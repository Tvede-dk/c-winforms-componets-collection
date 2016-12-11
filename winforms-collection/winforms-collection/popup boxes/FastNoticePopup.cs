using System;
using System.Drawing;
using System.Windows.Forms;
using SharedFunctionalities;

namespace winforms_collection {
    public partial class FastNoticePopup : Form {
        private readonly int _displayTime;

        public FastNoticePopup(String text, int displayTime, Bitmap bmp, Form parrent = null) {
            InitializeComponent();
            if (parrent == null) {
                parrent = this;
            }
            this.StartPosition = FormStartPosition.Manual;
            HandleLocation(parrent);
            this.label1.Text = text;
            this.pictureBox1.Image = bmp;
            this._displayTime = displayTime;
            this.Opacity = 0f;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.FadeIn(_displayTime / 4, AfterFadeIn);
        }

        private void HandleLocation(Form parrent) {
            parrent.HandleRemoteInvoke(() => {
                var parrentScreen = Screen.FromControl(parrent);
                this.HandleRemoteInvoke(() => {

                    Rectangle screen = parrentScreen.Bounds;
                    Rectangle work = parrentScreen.WorkingArea;
                    try {
                        this.Top = (work.Bottom - (this.Height * 2) - 25) - 50;
                        this.Left = work.Right - (screen.Width / 2) - (this.Width / 2);
                        var nTaskBarHeight = screen.Bottom - parrentScreen.WorkingArea.Bottom;
                    } catch (Exception e) {

                    }
                });
            });
        }



        public void AfterFadeIn() {
            new SmartTimer { Repeate = false, Counter = 1, Interval = _displayTime }.Start(AfterWait); //wait some time.
        }

        public void AfterWait() {
            this.FadeOut(_displayTime / 4, AfterFadeOut);
        }

        public void AfterFadeOut() {
            this.DialogResult = DialogResult.OK;
            Dispose();
        }

        public static void Show(string text, Bitmap bmp) {
        }


        public static void ShowInfo(string text, Form parret,int startTimeInMs = 500) {
            //IN ORder to use the showtopmostinactivce, we would have to schedual the timers on the parent form..  SO this is a "WIP" / TODO.
            new FastNoticePopup(text, startTimeInMs + (text.Length / 6 * 100), Properties.Resources._1416174868_info, parret).Show();
        }

        public static void ShowSucess(string text, Form parret) {
            new FastNoticePopup(text, 1200 + (text.Length / 6 * 100), Properties.Resources._1416175596_ok, parret).Show();
        }
        public static void ShowWarning(string text, Form parret) {
            new FastNoticePopup(text, 1200 + (text.Length / 6 * 100), Properties.Resources._1416175496_alert, parret).Show();
        }
        public static void ShowError(string text, Form parret) {
            new FastNoticePopup(text, 1200 + (text.Length / 6 * 100), Properties.Resources._1416175115_delete, parret).Show();
        }
    }
}
