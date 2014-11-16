using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedFunctionalities;
namespace winforms_collection.popup_boxes {
    public partial class NotificationBar : Form {

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
            SharedAnimations.fadeIn( this, 250, null );

        }



        private void end() {
            SharedAnimations.fadeOut( this, 350, () => { Dispose(); } );
        }


        public static void showAtLocation( String text, Point pt ) {
            var not = new NotificationBar();
            not.LabelText = text;
            not.Show();
            not.SetDesktopLocation( pt.X, pt.Y );

        }

        private void pictureBox1_Click( object sender, EventArgs e ) {
            end();
        }


        private void label1_MouseLeave( object sender, EventArgs e ) {
            new SmartUITimer( this ) { continous = false, counter = 0, interval = 2000 }.start( () => { end(); } );
        }
    }
}
