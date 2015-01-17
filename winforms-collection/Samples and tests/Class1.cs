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
        //[System.STAThread]
        //static void Main() {
        //    Application.Run( new MyForm() );
        //}
    }
}
