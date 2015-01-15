using SharedFunctionalities.drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.drawParts {
    public class testDraw : Control {

        private DrawingHandler drawer = new DrawingHandler();



        public testDraw() {
            SetStyle( ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.CacheText | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true );
            //drawer.addLayer( new backgroundDrawer() );
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            //drawer.addLayer( new TextDrawer( Text, Font, Brushes.Black, format ) );
        }



        protected override void OnPaint( PaintEventArgs e ) {

            //drawer.draw( e.Graphics, ClientRectangle, e.ClipRectangle );

        }

        protected override void OnResize( EventArgs e ) {
            base.OnResize( e );
            drawer.invalidate();
        }



    }

}
