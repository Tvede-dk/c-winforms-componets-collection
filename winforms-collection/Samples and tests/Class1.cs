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
        const int ABigNumber = 50000;
        readonly Bitmap _bitmap;
        Stopwatch _stopwatch;
        float _first;
        private winforms_collection.navigation.CollapsableSplitContainer _collapsableSplitContainer1;
        float _second;
        public MyForm() {
            _bitmap = new Bitmap( 55, 55 );
            var a = Graphics.FromImage( _bitmap );
            a.Clear( BackColor );
            a.FillEllipse( Brushes.Red, 21, 21, 34, 34 );
            a.Dispose();
        }
        void FirstAlternative( Graphics a ) {
            for ( var i = 0; i < ABigNumber; i++ ) {
                a.DrawImage( _bitmap, 0, 0 );
            }
        }
        void SecondAlternative( Graphics a ) {
            System.IntPtr intptr = a.GetHdc();
            for ( var i = 0; i < ABigNumber; i++ ) {
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

            _stopwatch = Stopwatch.StartNew();
            FirstAlternative( a.Graphics );
            _stopwatch.Stop();
            _first = _stopwatch.ElapsedMilliseconds / 1000f;

            _stopwatch = Stopwatch.StartNew();
            SecondAlternative( a.Graphics );
            _stopwatch.Stop();
            _second = _stopwatch.ElapsedMilliseconds / 1000f;

            a.Graphics.DrawString
            (
            "Finished",
            new Font( "Times", 12 ),
            new SolidBrush( Color.Black ),
            new Point( 5, 120 )
            );

            a.Graphics.DrawString
            (
            "First Alternative\nusing DrawImage() took " + _first + " seconds",
            new Font( "Times", 12 ),
            new SolidBrush( Color.Black ),
            new Point( 5, 150 )
            );

            a.Graphics.DrawString
            (
            "Second Alternative\nusing BitBlt() took " + _second + " seconds",
            new Font( "Times", 12 ),
            new SolidBrush( Color.Black ),
            new Point( 5, 200 )
            );

        }

        private void InitializeComponent() {
            this._collapsableSplitContainer1 = new winforms_collection.navigation.CollapsableSplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this._collapsableSplitContainer1)).BeginInit();
            this._collapsableSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // collapsableSplitContainer1
            // 
            this._collapsableSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._collapsableSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this._collapsableSplitContainer1.Name = "_collapsableSplitContainer1";
            this._collapsableSplitContainer1.Size = new System.Drawing.Size(284, 261);
            this._collapsableSplitContainer1.SplitterDistance = 48;
            this._collapsableSplitContainer1.TabIndex = 0;
            // 
            // MyForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this._collapsableSplitContainer1);
            this.Name = "MyForm";
            ((System.ComponentModel.ISupportInitialize)(this._collapsableSplitContainer1)).EndInit();
            this._collapsableSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        //[System.STAThread]
        //static void Main() {
        //    Application.Run( new MyForm() );
        //}
    }
}
