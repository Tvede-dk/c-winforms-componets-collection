using System;
using System.Drawing;
using System.Windows.Forms;
using SharedFunctionalities;
namespace winforms_collection.popup_boxes {
    public partial class NotificationBar : Form {
        private const int fastFadeTimeInMs = 250;
        private const int fastFadeoutInMs = 350;
        private const int TimeToDimissAfterMouseOverInMs = 2000;
        #region property text
        public string LabelText {
            get { return this.label1.Text; }
            set {
                this.label1.Text = value;
            }
        }
        #endregion


        public NotificationBar() {
            InitializeComponent();
            TransparencyKey = BackColor;
            this.label1.Text = LabelText;
            SharedAnimations.fadeIn( this, fastFadeTimeInMs, null );

        }



        private void end() {

            SharedAnimations.fadeOut( this, fastFadeoutInMs, () => { Dispose(); } );
        }


        public static void showAtLocation( String text, Point pt ) {
            var not = new NotificationBar();
            not.LabelText = text;
            not.Show();
            //these values are from the graphical image  part of the background. The label is however a very good estimate for these values.
            not.SetDesktopLocation( pt.X - 15, pt.Y - 22 );


        }

        private void pictureBox1_Click( object sender, EventArgs e ) {
            end();
        }


        private void label1_MouseLeave( object sender, EventArgs e ) {
            new SmartUITimer( this ) { repeate = false, counter = 0, interval = TimeToDimissAfterMouseOverInMs }.start( () => { end(); } );
        }
    }
}
