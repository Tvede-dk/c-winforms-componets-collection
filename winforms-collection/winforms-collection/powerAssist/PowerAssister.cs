using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.powerAssist {
    public class PowerAssister {

        public enum DisplayToControl {
            LEFT, RIGHT, TOP, BOTTOM
        }


        #region property Margin
        private int _Margin;


        public int Margin {
            get { return _Margin; }
            set { _Margin = value; }
        }
        #endregion

        private List<PowerAssistBox> shownBoxes {
            get {
                return controlToShownBox.Values.ToList();
            }

        }


        private bool haveShown = false;


        private Dictionary<Control, PowerAssistBox> controlToShownBox = new Dictionary<Control, PowerAssistBox>(4);

        private Dictionary<Keys, Control> focusDict = new Dictionary<Keys, Control>(4);


        public void show() {
            if (haveShown) {
                return;
            }
            haveShown = true;
            foreach (var item in controlToShownBox.ToList()) {
                //configure.
                show(item.Key, item.Value);
                item.Value.ShowInactiveTopmost();
                SharedFunctionalities.SharedAnimations.fadeIn(item.Value, 150, null);
            }
        }

        public void hide() {
            if (haveShown == false) {
                return;
            }
            haveShown = false;

            foreach (var item in shownBoxes) {
                item.Hide();
            }
        }

        public void highlight(Control control) {
            if (controlToShownBox.ContainsKey(control)) {
                controlToShownBox[control].highlight();
            }
        }
        public void removeHightligt(Control control) {
            if (controlToShownBox.ContainsKey(control)) {
                controlToShownBox[control].removeHighlight();
            }
        }



        private void show(Control testControl, PowerAssistBox form) {
            testControl.Move += (sender, e) => {
                showTo(testControl, form);
            };
            testControl.FindForm().Move += (sender, e) => {
                showTo(testControl, form);
            };
            testControl.FindForm().Resize += (sender, e) => {
                showTo(testControl, form);
            };
            showTo(testControl, form);
        }

        private void showTo(Control testControl, PowerAssistBox form) {
            int margin = form.Margin;
            var inner = testControl.PointToScreen(Point.Empty);
            var where = form.WhereToDisplayAt;
            switch (where) {
                case DisplayToControl.LEFT:
                    inner.X -= (form.Width + margin);
                    break;
                case DisplayToControl.RIGHT:
                    inner.X += (testControl.Width + margin);
                    break;
                case DisplayToControl.TOP:
                    inner.Y -= (form.Height + margin);
                    break;
                case DisplayToControl.BOTTOM:
                    inner.Y += (testControl.Height + margin);
                    break;
                default:
                    break;
            }
            form.Location = inner;
        }

        public void AddControl(Control control, string text, Keys shortcutKey = Keys.None, DisplayToControl where = PowerAssister.DisplayToControl.RIGHT, int margin = 5) {
            var form = new PowerAssistBox(text);
            form.WhereToDisplayAt = where;
            form.Margin = margin;
            form.InnerShortcut = shortcutKey;
            controlToShownBox.Add(control, form);
            focusDict.Add(shortcutKey, control);
        }

        public void registerForKeyEventsHandler(KeyEventHandler keyDown, KeyEventHandler keyUp) {
            foreach (var item in controlToShownBox) {
                item.Key.KeyDown += keyDown;
                item.Key.KeyUp += keyUp;
            }
        }

        public void registerForKeysWithPreviewKey(Form form = null, Keys previewKey = Keys.Menu) {
            foreach (var item in controlToShownBox) {
                item.Key.KeyDown += (object obj, KeyEventArgs e) => {
                    if (previewKey == e.KeyCode) {
                        e.Handled = true;
                        show();
                    }
                    if (handleOnKeyPress(e.KeyData)) {
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                    }

                };
                item.Key.KeyUp += (object obj, KeyEventArgs e) => {
                    if (e.KeyCode.HasFlag(previewKey)) {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        hide();
                    }
                };
            }
            if (form != null) {
                form.KeyPreview = true;
                form.KeyUp += (object sender, KeyEventArgs e) => {
                    if (e.KeyCode.HasFlag(previewKey)) {
                        hide();
                    }
                };
                form.Leave += (sender, theEvent) => { hide(); };
                form.Deactivate += (sender, theEvent) => { hide(); };
            }
        }


        private void trySetFocus(Control control) {
            var asComboBox = control as ComboBox;
            if (asComboBox != null) {
                asComboBox.DroppedDown = true;
                asComboBox.Focus();
            } else {
                control.Focus();
            }
        }

        public bool isVisable() {
            return haveShown;
        }

        public void toogleVisable() {
            if (isVisable()) {
                hide();
            } else {
                show();
            }
        }

        public bool handleOnKeyPress(Keys keyData) {
            if (focusDict.ContainsKey(keyData)) {
                trySetFocus(focusDict[keyData]);
                hide();
                return true;
            }
            return false;
        }


        public void Clear() {
            focusDict.Clear();
            controlToShownBox.Clear();
        }
    }
}
