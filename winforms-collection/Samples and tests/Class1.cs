/*
BitBlt.cs
*/
namespace MyNamespace {
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    class MyForm : Form {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt
        (
        System.IntPtr a,
        int b,
        int c,
        int d,
        int e,
        System.IntPtr f,
        int g,
        int h,
        int i
        );
        const int A_BIG_NUMBER = 50000;
        Bitmap bitmap;
        Stopwatch stopwatch;
        float first;
        private winforms_collection.navigation.CollapsableSplitContainer collapsableSplitContainer1;
        float second;
        public MyForm() {
            bitmap = new Bitmap( 55, 55 );
            Graphics a = Graphics.FromImage( bitmap );
            a.Clear( BackColor );
            a.FillEllipse( Brushes.Red, 21, 21, 34, 34 );
            a.Dispose();
        }
        void FirstAlternative( Graphics a ) {
            for ( int i = 0; i < A_BIG_NUMBER; i++ ) {
                a.DrawImage( bitmap, 0, 0 );
            }
        }
        void SecondAlternative( Graphics a ) {
            System.IntPtr intptr = a.GetHdc();
            for ( int i = 0; i < A_BIG_NUMBER; i++ ) {
                BitBlt( intptr, 55, 0, 55, 55, intptr, 0, 0, 13369376 );
            }
            a.ReleaseHdc( intptr );
        }
        override protected void OnPaint( PaintEventArgs a ) {
            a.Graphics.DrawString
            (
            "Wait...",
            new Font( "Times", 12 ),
            new SolidBrush( Color.Black ),
            new Point( 5, 90 )
            );

            stopwatch = Stopwatch.StartNew();
            FirstAlternative( a.Graphics );
            stopwatch.Stop();
            first = stopwatch.ElapsedMilliseconds / 1000f;

            stopwatch = Stopwatch.StartNew();
            SecondAlternative( a.Graphics );
            stopwatch.Stop();
            second = stopwatch.ElapsedMilliseconds / 1000f;

            a.Graphics.DrawString
            (
            "Finished",
            new Font( "Times", 12 ),
            new SolidBrush( Color.Black ),
            new Point( 5, 120 )
            );

            a.Graphics.DrawString
            (
            "First Alternative\nusing DrawImage() took " + first + " seconds",
            new Font( "Times", 12 ),
            new SolidBrush( Color.Black ),
            new Point( 5, 150 )
            );

            a.Graphics.DrawString
            (
            "Second Alternative\nusing BitBlt() took " + second + " seconds",
            new Font( "Times", 12 ),
            new SolidBrush( Color.Black ),
            new Point( 5, 200 )
            );

        }

        private void InitializeComponent() {
            this.collapsableSplitContainer1 = new winforms_collection.navigation.CollapsableSplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.collapsableSplitContainer1)).BeginInit();
            this.collapsableSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // collapsableSplitContainer1
            // 
            this.collapsableSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collapsableSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.collapsableSplitContainer1.Name = "collapsableSplitContainer1";
            this.collapsableSplitContainer1.Size = new System.Drawing.Size(284, 261);
            this.collapsableSplitContainer1.SplitterDistance = 48;
            this.collapsableSplitContainer1.TabIndex = 0;
            // 
            // MyForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.collapsableSplitContainer1);
            this.Name = "MyForm";
            ((System.ComponentModel.ISupportInitialize)(this.collapsableSplitContainer1)).EndInit();
            this.collapsableSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        //[System.STAThread]
        //static void Main() {
        //    Application.Run( new MyForm() );
        //}
    }
}
