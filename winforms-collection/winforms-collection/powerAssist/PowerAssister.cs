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

        private Dictionary<Control, PowerAssistBox> controlToShownBox = new Dictionary<Control, PowerAssistBox>();

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
        }

        public void registerForKeyEventsHandler(KeyEventHandler keyDown, KeyEventHandler keyUp) {
            foreach (var item in controlToShownBox) {
                item.Key.KeyDown += keyDown;
                item.Key.KeyUp += keyUp;
            }
        }

        public void registerForKeysWithPreviewKey(Keys previewKey = Keys.Menu) {

            Dictionary<Keys, Control> focusDict = new Dictionary<Keys, Control>(controlToShownBox.Count);

            foreach (var item in controlToShownBox) {
                focusDict.Add(item.Value.InnerShortcut, item.Key);
                item.Key.KeyDown += (object obj, KeyEventArgs e) => {
                    if (previewKey == e.KeyCode) {
                        e.Handled = true;
                        show();
                    }

                    if (focusDict.ContainsKey(e.KeyData)) {
                        trySetFocus(focusDict[e.KeyData]);
                        hide();
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
    }
}
