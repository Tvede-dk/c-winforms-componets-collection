using SharedFunctionalities.drawing;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace winforms_collection.drawParts {
    public class TestDraw : Control {

        private readonly DrawingHandler _drawer = new DrawingHandler();



        public TestDraw() {
            SetStyle( ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.CacheText | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true );
            //drawer.addLayer( new backgroundDrawer() );
            var format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            //drawer.addLayer( new TextDrawer( Text, Font, Brushes.Black, format ) );
        }



        protected override void OnPaint( PaintEventArgs e ) {

            //drawer.draw( e.Graphics, ClientRectangle, e.ClipRectangle );

        }

        protected override void OnResize( EventArgs e ) {
            base.OnResize( e );
            _drawer.Invalidate();
        }



    }

}
