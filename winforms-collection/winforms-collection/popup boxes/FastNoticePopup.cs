using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;
using winforms_collection.popup_boxes;
using SharedFunctionalities;

namespace winforms_collection {
    public partial class FastNoticePopup : Form {
        private int displayTime;

        public FastNoticePopup( String text, int displayTime, Bitmap bmp, Form parrent = null ) {
            InitializeComponent();
            if ( parrent == null ) {
                parrent = this;
            }
            this.StartPosition = FormStartPosition.Manual;
            handleLocation( parrent );
            this.label1.Text = text;
            this.pictureBox1.Image = bmp;
            this.displayTime = displayTime;
            this.Opacity = 0f;
        }
        protected override void OnLoad( EventArgs e ) {
            base.OnLoad( e );
            this.FadeIn( displayTime / 4, afterFadeIn );
        }

        private void handleLocation( Form parrent ) {
            //parrent.handleRemoteInvoke( () => {

            this.handleRemoteInvoke( () => {
                Screen thisScreen = Screen.FromControl( parrent );
                Rectangle screen = thisScreen.Bounds;
                Rectangle work = thisScreen.WorkingArea;
                this.Top = (work.Bottom - (this.Height * 2) - 25) - 50;
                this.Left = work.Right - (screen.Width / 2) - (this.Width / 2);
                int nTaskBarHeight = screen.Bottom - thisScreen.WorkingArea.Bottom;
            } );
            //} );
        }



        public void afterFadeIn() {
            new SmartTimer { repeate = false, counter = 1, interval = displayTime }.start( afterWait ); //wait some time.
        }

        public void afterWait() {
            this.FadeOut( displayTime / 4, afterFadeOut );
        }

        public void afterFadeOut() {
            this.DialogResult = DialogResult.OK;
            Dispose();
        }

        public static void Show( string text, Bitmap bmp ) {
        }


        public static void ShowInfo( string text, Form parret ) {
            //IN ORder to use the showtopmostinactivce, we would have to schedual the timers on the parent form..  SO this is a "WIP" / TODO.
            new FastNoticePopup( text, 1200 + (text.Length / 6 * 100), Properties.Resources._1416174868_info, parret ).Show();
        }

        public static void ShowSucess( string text, Form parret ) {
            new FastNoticePopup( text, 1200 + (text.Length / 6 * 100), Properties.Resources._1416175596_ok, parret ).Show();
        }
        public static void ShowWarning( string text, Form parret ) {
            new FastNoticePopup( text, 1200 + (text.Length / 6 * 100), Properties.Resources._1416175496_alert, parret ).Show();
        }
        public static void ShowError( string text, Form parret ) {
            new FastNoticePopup( text, 1200 + (text.Length / 6 * 100), Properties.Resources._1416175115_delete, parret ).Show();
        }
    }
}
