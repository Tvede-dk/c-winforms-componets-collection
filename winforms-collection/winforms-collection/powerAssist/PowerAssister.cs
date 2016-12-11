using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace winforms_collection.powerAssist {
    public class PowerAssister {

        public enum DisplayToControl {
            Left, Right, Top, Bottom
        }


        #region property Margin
        private int _margin;


        public int Margin {
            get { return _margin; }
            set { _margin = value; }
        }
        #endregion

        private List<PowerAssistBox> ShownBoxes {
            get {
                return _controlToShownBox.Values.ToList();
            }

        }


        private bool _haveShown = false;


        private readonly Dictionary<Control, PowerAssistBox> _controlToShownBox = new Dictionary<Control, PowerAssistBox>(4);

        private readonly Dictionary<Keys, Control> _focusDict = new Dictionary<Keys, Control>(4);


        public void Show() {
            if (_haveShown) {
                return;
            }
            _haveShown = true;
            foreach (var item in _controlToShownBox.ToList()) {
                //configure.
                Show(item.Key, item.Value);
                item.Value.ShowInactiveTopmost();
                SharedFunctionalities.SharedAnimations.FadeIn(item.Value, 150, null);
            }
        }

        public void Hide() {
            if (_haveShown == false) {
                return;
            }
            _haveShown = false;

            foreach (var item in ShownBoxes) {
                item.Hide();
            }
        }

        public void Highlight(Control control) {
            if (_controlToShownBox.ContainsKey(control)) {
                _controlToShownBox[control].Highlight();
            }
        }
        public void RemoveHightligt(Control control) {
            if (_controlToShownBox.ContainsKey(control)) {
                _controlToShownBox[control].RemoveHighlight();
            }
        }



        private void Show(Control testControl, PowerAssistBox form) {
            testControl.Move += (sender, e) => {
                ShowTo(testControl, form);
            };

            var pform = testControl.FindForm();
            if (pform != null) {
                pform.Move += (sender, e) => {
                    ShowTo(testControl, form);
                };
                pform.Resize += (sender, e) => {
                    ShowTo(testControl, form);
                };
            }


            ShowTo(testControl, form);
        }

        private void ShowTo(Control testControl, PowerAssistBox form) {
            var margin = form.Margin;
            var inner = testControl.PointToScreen(Point.Empty);
            var where = form.WhereToDisplayAt;
            switch (where) {
                case DisplayToControl.Left:
                    inner.X -= (form.Width + margin);
                    break;
                case DisplayToControl.Right:
                    inner.X += (testControl.Width + margin);
                    break;
                case DisplayToControl.Top:
                    inner.Y -= (form.Height + margin);
                    break;
                case DisplayToControl.Bottom:
                    inner.Y += (testControl.Height + margin);
                    break;
                default:
                    break;
            }
            form.Location = inner;
        }

        public void Clear() {
            _controlToShownBox.Clear();
            _focusDict.Clear();
            _haveShown = false;
        }

        public void AddControl(Control control, string text, Keys shortcutKey = Keys.None, DisplayToControl where = PowerAssister.DisplayToControl.Right, int margin = 5) {
            var form = new PowerAssistBox(text);
            form.WhereToDisplayAt = where;
            form.Margin = margin;
            form.InnerShortcut = shortcutKey;
            _controlToShownBox.Add(control, form);
            _focusDict.Add(shortcutKey, control);
        }

        public void registerForKeyEventsHandler(KeyEventHandler keyDown, KeyEventHandler keyUp) {
            foreach (var item in _controlToShownBox) {
                item.Key.KeyDown += keyDown;
                item.Key.KeyUp += keyUp;
            }
        }

        public void RegisterForKeysWithPreviewKey(Form form = null, Keys previewKey = Keys.Menu) {
            foreach (var item in _controlToShownBox) {
                item.Key.KeyDown += (object obj, KeyEventArgs e) => {
                    if (previewKey == e.KeyCode) {
                        if (form != null) {
                            form.ActiveControl = null;
                        }
                        e.Handled = true;
                        Show();
                    }
                    if (HandleOnKeyPress(e.KeyData)) {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                    }
                    
                };
                item.Key.KeyUp += (object obj, KeyEventArgs e) => {
                    if (e.KeyCode.HasFlag(previewKey)) {
                        if (form != null) {
                            form.ActiveControl = null;
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Hide();
                    }
                };
            }
            if (form != null) {
                form.KeyPreview = true;
                form.KeyUp += (object sender, KeyEventArgs e) => {
                    if (e.KeyCode.HasFlag(previewKey)) {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Hide();
                    }
                };
                form.Leave += (sender, theEvent) => { Hide(); };
                form.Deactivate += (sender, theEvent) => { Hide(); };
            }
        }


        private void TrySetFocus(Control control) {
            var asComboBox = control as ComboBox;
            if (asComboBox != null) {
                asComboBox.DroppedDown = true;
                asComboBox.Focus();
            } else {
                control.Focus();
            }
        }

        public bool IsVisable() {
            return _haveShown;
        }

        public void ToogleVisable() {
            if (IsVisable()) {
                Hide();
            } else {
                Show();
            }
        }

        public bool HandleOnKeyPress(Keys keyData) {
            if (_focusDict.ContainsKey(keyData)) {
                TrySetFocus(_focusDict[keyData]);
                Hide();
                return true;
            }
            return false;
        }
    }
}
