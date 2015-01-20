using SharedFunctionalities.drawing;
using SharedFunctionalities.drawing.layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedFunctionalities {
    public class CustomControl : Control {



        #region property DrawHandler
        private readonly DrawingHandler _DrawHandler = new DrawingHandler();


        public DrawingHandler DrawHandler {
            get { return _DrawHandler; }
        }
        #endregion


        [EditorBrowsable]
        public Color BorderColor {
            get { return borderLayer.BorderColor; }
            set {
                borderLayer.BorderColor = value;
            }
        }



        #region property FlashBorderOnMouseOver
        private bool _FlashBorderOnMouseOver;


        public bool FlashBorderOnMouseOver {
            get { return _FlashBorderOnMouseOver; }
            set { _FlashBorderOnMouseOver = value; }
        }
        #endregion


        #region property FlashBorderColorStart
        private Color _FlashBorderColorStart = Color.FromArgb( 255, 9, 74, 178 );


        public Color FlashBorderColorStart {
            get { return _FlashBorderColorStart; }
            set { _FlashBorderColorStart = value; }
        }
        #endregion



        #region property BorderSize

        [EditorBrowsable]
        public int BorderSize {
            get { return borderLayer.BorderSize; }
            set { borderLayer.BorderSize = value; }
        }
        #endregion

        private bool isMouseInside = false;

        private BorderLayer borderLayer = new BorderLayer();

        //to consider :
        // provide an automatic cleanup of brushes ? 
        // provide a simple interface and painters ?

        public CustomControl() {
            SetStyle( ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.CacheText | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true );
            DrawHandler.addLayer( borderLayer );

        }
        protected override void OnPaint( PaintEventArgs e ) {
            //result = new Bitmap( Width, Height, PixelFormat.Format32bppPArgb );
            //using (var g = Graphics.FromImage( result )) {
            //    testThis( g, e );
            //}
            //Console.WriteLine( "in OnPaint" );
            //DrawHandler.draw( e.Graphics, ClientRectangle, e.ClipRectangle );
            //testThis( e );

            DrawHandler.drawAsync( this.CreateGraphics(), ClientRectangle, e.ClipRectangle, this );
        }

        //private async void testThis(Graphics g, PaintEventArgs e ) {
        //    Console.WriteLine( "in testThis" );
        //    await Task.Run( () => { DrawHandler.drawAsync( g, ClientRectangle, e.ClipRectangle ); } );
        //    this.BeginInvoke( (MethodInvoker)(() => { result.bitbltRepeat( e.Graphics, result.Width, result.Height ); }) );
        //    //e.Graphics.DrawImage( result, 0, 0 );
        //}


        protected override void OnMouseEnter( EventArgs e ) {
            base.OnMouseEnter( e );
            isMouseInside = true;
            flashBorder();
        }

        private void flashBorder() {
            if ( _FlashBorderOnMouseOver ) {
                Color startCol = BorderColor;
                if ( _FlashBorderColorStart != Color.Transparent ) {
                    startCol = FlashBorderColorStart;
                }
                Color oldColor = this.BorderColor;
                SharedAnimations.cycleColorLighting( ( Color col ) => {
                    this.BorderColor = col;
                    if ( !IsMouseInside() ) {
                        this.BorderColor = oldColor;
                    }
                    Invalidate( ClientRectangle.CalculateBorder( BorderSize ) );
                    return IsMouseInside();
                }, 1.0f, 0.0f, 0.05f, startCol, this );
            }
        }

        protected override void OnMouseLeave( EventArgs e ) {
            base.OnMouseLeave( e );
            isMouseInside = false;
        }

        public bool IsMouseInside() {
            return isMouseInside;
        }
        protected override void OnResize( EventArgs e ) {
            base.OnResize( e );
            DrawHandler.invalidate();
        }

    }
}
