using SharedFunctionalities.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.powerAssist {
    public partial class PowerAssistBox : TransperantForm {

        private Color prevBackColor;


        #region property LockSingleLine
        private bool _LockSingleLine = true;


        public bool LockSingleLine {
            get { return _LockSingleLine; }
            set { _LockSingleLine = value; }
        }
        #endregion


        #region property Margin
        private int _Margin;


        public new int Margin {
            get { return _Margin; }
            set { _Margin = value; }
        }
        #endregion


        #region property WhereToDisplayAt
        private PowerAssister.DisplayToControl _WhereToDisplayAt = PowerAssister.DisplayToControl.RIGHT;


        public PowerAssister.DisplayToControl WhereToDisplayAt {
            get { return _WhereToDisplayAt; }
            set { _WhereToDisplayAt = value; }
        }
        #endregion


        public PowerAssistBox() {
            InitializeComponent();
            this.Opacity = 0.98;
        }

        public PowerAssistBox(string text) : this() {
            setText(text);
        }

        public void setText(string v) {
            label1.Text = v;
        }

        public void highlight() {
            prevBackColor = BackColor;
            BackColor = Color.FromArgb(200, 0, 251, 204);
        }
        public void removeHighlight() {
            if (prevBackColor != null) {
                BackColor = prevBackColor;
            }
        }

        private void PowerAssistBox_Resize(object sender, EventArgs e) {
            if (LockSingleLine) {
                this.Height = 15;
            }

            Width = label1.PreferredWidth;

        }

        
    }
}
