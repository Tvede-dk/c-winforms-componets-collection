using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SharedFunctionalities.keyboard {
    public class KeyHook {
        #region DLL imports
        /// <summary>
        /// Sets the windows hook, do the desired event, one of hInstance or threadId must be non-null
        /// </summary>
        /// <param name="idHook">The id of the event you want to hook</param>
        /// <param name="callback">The callback.</param>
        /// <param name="hInstance">The handle you want to attach the event to, can be null</param>
        /// <param name="threadId">The thread you want to attach the event to, can be null</param>
        /// <returns>a handle to the desired hook</returns>
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        /// Unhooks the windows hook.
        /// </summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        /// Calls the next hook.
        /// </summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wparam.</param>
        /// <param name="lParam">The lparam.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref KeyboardHookStruct lParam);

        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="lpFileName">Name of the library</param>
        /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);


        private const int VkShift = 0x10;
        private const int VkControl = 0x11;
        private const int VkMenu = 0x12;
        private const int VkCapital = 0x14;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);
        #endregion


        #region Constant, Structure and Delegate Definitions
        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int KeyboardHookProc(int code, int wParam, ref KeyboardHookStruct lParam);

        public struct KeyboardHookStruct {
            public int VkCode;
            public int ScanCode;
            public int Flags;
            public int Time;
            public int DwExtraInfo;
        }

        const int WhKeyboardLl = 13;
        const int WmKeydown = 0x100;
        const int WmKeyup = 0x101;
        const int WmSyskeydown = 0x104;
        const int WmSyskeyup = 0x105;
        #endregion

        #region Instance Variables

        private readonly Dictionary<Keys, CallbackHandler> _keyToAction = new Dictionary<Keys, CallbackHandler>();

        private readonly KeyboardHookProc _callback;

        /// <summary>
        /// Handle to the hook, need this to unhook and call the next hook
        /// </summary>
        private IntPtr _hhook = IntPtr.Zero;
        #endregion

        public void AddHook(Keys k, Action onKey) {
            AddHook(k, onKey, false);
        }

        public void AddHook(Keys k, Action<KeyState> onKey) {
            _keyToAction.Add(k, new CallbackHandler(onKey));
        }

        public void AddHook(Keys k, Action onKey, bool onlyCallOneTime) {
            _keyToAction.Add(k, new CallbackHandler(onKey, onlyCallOneTime));
        }



        private void ReHook() {
            Unhook();
            Hook();
        }



        #region Constructors and Destructors



        public KeyHook() {
            _callback = new KeyboardHookProc(HookProc);
        }

        ~KeyHook() {
            Unhook();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Installs the global hook
        /// </summary>
        public void Hook() {
            IntPtr hInstance = LoadLibrary("User32");
            _hhook = SetWindowsHookEx(WhKeyboardLl, _callback, hInstance, 0);
        }

        /// <summary>
        /// Uninstalls the global hook
        /// </summary>
        public void Unhook() {
            UnhookWindowsHookEx(_hhook);
        }

        /// <summary>
        /// The callback for the keyboard hook
        /// </summary>
        /// <param name="code">The hook code, if it isn't >= 0, the function shouldn't do anyting</param>
        /// <param name="wParam">The event type</param>
        /// <param name="lParam">The keyhook event information</param>
        /// <returns></returns>
        public int HookProc(int code, int wParam, ref KeyboardHookStruct lParam) {
            if (code >= 0) {
                var key = (Keys)lParam.VkCode;
                key = HandleModifiers(key);
                if (_keyToAction.ContainsKey(key)) {
                    var kea = new KeyEventArgs(key);
                    if ((wParam == WmKeydown || wParam == WmSyskeydown)) {

                        OnKeyDown(this, kea);
                    } else if ((wParam == WmKeyup || wParam == WmSyskeyup)) {
                        OnKeyUp(this, kea);
                    }
                    if (kea.Handled) {
                        return 1;
                    }
                }
            }
            return CallNextHookEx(_hhook, code, wParam, ref lParam);
        }

        private Keys HandleModifiers(Keys key) {
            //if ((NativeMethods.GetKeyState(VK_CAPITAL) & 0x0001) != 0) {
            //}

            if ((GetKeyState(VkShift) & 0x8000) != 0) {
                //SHIFT is pressed
                key |= Keys.Shift;
            }
            if ((GetKeyState(VkControl) & 0x8000) != 0) {
                //CONTROL is pressed
                key |= Keys.Control;
            }
            if ((GetKeyState(VkMenu) & 0x8000) != 0) {
                //ALT is pressed
                key |= Keys.Alt;
            }
            return key;
        }
        #endregion

        private void OnKeyDown(object sender, KeyEventArgs key) {
            if (_keyToAction.ContainsKey(key.KeyData)) {

                _keyToAction[key.KeyData].Handle(KeyState.KeyDown);
            }
            key.Handled = true;
        }
        private void OnKeyUp(object sender, KeyEventArgs key) {
            if (_keyToAction.ContainsKey(key.KeyData)) {
                _keyToAction[key.KeyData].Handle(KeyState.KeyUp);
            }
            key.Handled = true;
        }

    }
    public enum KeyState {
        KeyDown = 0,
        KeyUp = 1
    }

    public struct CallbackHandler {
        public Action ToRun { get; set; }
        /// <summary>
        /// the expanded version where we want the keystate.
        /// </summary>
        public Action<KeyState> OnKeyPressedWithState;
        private readonly bool _onlyCallOneTime;

        public CallbackHandler(Action onKey) : this() {
            this.ToRun = onKey;
        }

        public CallbackHandler(Action<KeyState> onKey) : this() {
            this.OnKeyPressedWithState = onKey;
        }

        public CallbackHandler(Action onKey, bool onlyCallOneTime) : this() {
            this.ToRun = onKey;
            this._onlyCallOneTime = onlyCallOneTime;
        }

        public void Handle(KeyState state) {
            if (OnKeyPressedWithState == null) {
                if (!_onlyCallOneTime || _onlyCallOneTime && state == KeyState.KeyDown) {
                    ToRun();
                }
            } else {
                OnKeyPressedWithState(state);
            }
        }
    }

}
