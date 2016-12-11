using SharedFunctionalities.forms;
using System;
using System.Drawing;

namespace winforms_collection.powerAssist {
    public partial class PowerAssistBox : TransperantForm {

        private Color _prevBackColor;


        #region property LockSingleLine
        private bool _lockSingleLine = true;


        public bool LockSingleLine {
            get { return _lockSingleLine; }
            set { _lockSingleLine = value; }
        }
        #endregion


        #region property Margin
        private int _margin;


        public new int Margin {
            get { return _margin; }
            set { _margin = value; }
        }
        #endregion


        #region property WhereToDisplayAt
        private PowerAssister.DisplayToControl _whereToDisplayAt = PowerAssister.DisplayToControl.Right;


        public PowerAssister.DisplayToControl WhereToDisplayAt {
            get { return _whereToDisplayAt; }
            set { _whereToDisplayAt = value; }
        }
        #endregion


        public PowerAssistBox() {
            InitializeComponent();
            this.Opacity = 0.98;
        }

        public PowerAssistBox(string text) : this() {
            SetText(text);
        }

        public void SetText(string v) {
            label1.Text = v;
        }

        public void Highlight() {
            _prevBackColor = BackColor;
            BackColor = Color.FromArgb(200, 0, 251, 204);
        }
        public void RemoveHighlight() {
            if (_prevBackColor != null) {
                BackColor = _prevBackColor;
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
