using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace winforms_collection.advanced {
    public partial class Dropdown : ComboBox {

        private dropdown.DropdownOverlay dropdownOverlay = new dropdown.DropdownOverlay() { ShowIcon = false, ShowInTaskbar = false };

        #region property fastShortcut
        private bool _fastShortcut;

        [EditorBrowsable]
        public bool fastShortcut {
            get { return _fastShortcut; }
            set { _fastShortcut = value; }
        }
        #endregion


        #region property fastShortcutModifiers
        private Keys _fastShortcutModifiers;

        [EditorBrowsable]
        public Keys fastShortcutModifiers {
            get { return _fastShortcutModifiers; }
            set { _fastShortcutModifiers = value; }
        }
        #endregion


        protected override void OnKeyDown( KeyEventArgs e ) {
            base.OnKeyDown( e );
            if ( e.Modifiers == fastShortcutModifiers ) {
                if ( isNumber( e ) ) {
                    //fast select
                    int index = getNumber( e );
                    if ( index >= 0 && index < Items.Count ) {
                        SelectedIndex = index;
                        DroppedDown = false;
                    }
                }
            }
        }

        private bool isNumber( KeyEventArgs e ) {
            if ( (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) ) {
                return true;
            } else {
                return false;
            }
        }



        private int getNumber( KeyEventArgs e ) {
            int keyVal = (int)e.KeyValue;
            int value = -1;
            if ( e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 ) {
                value = (int)e.KeyValue - (int)Keys.D0;
            } else if ( e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 ) {
                value = (int)e.KeyValue - (int)Keys.NumPad0;
            }
            return value - 1;
        }

        public Dropdown() {
            InitializeComponent();
        }

        protected override void OnDropDownClosed( EventArgs e ) {
            base.OnDropDownClosed( e );
            dropdownOverlay.Hide();
        }

        protected override void OnDropDown( EventArgs e ) {
            base.OnDropDown( e );
            if ( _fastShortcut && DropDownStyle != ComboBoxStyle.Simple && (dropdownOverlay.Visible == false) ) {
                //display all the fast shortcuts. 
                Point pt = this.PointToScreen( Point.Empty );
                pt.X -= dropdownOverlay.Size.Width;
                dropdownOverlay.Location = pt;
                //dropdownOverlay.Show();
                //dropdownOverlay.BringToFront();
                //this.Focus();
                dropdownOverlay.ShowInactiveTopmost();
            }
        }

    }
}
