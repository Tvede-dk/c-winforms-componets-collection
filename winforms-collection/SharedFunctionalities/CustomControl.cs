using SharedFunctionalities.drawing;
using SharedFunctionalities.drawing.layers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharedFunctionalities {
    public class CustomControl : Control {



        #region property DrawHandler
        private readonly DrawingHandler _drawHandler = new DrawingHandler();


        public DrawingHandler DrawHandler {
            get { return _drawHandler; }
        }
        #endregion


        [EditorBrowsable]
        public Color BorderColor {
            get { return _borderLayer.BorderColor; }
            set {
                _borderLayer.BorderColor = value;
            }
        }



        #region property FlashBorderOnMouseOver
        private bool _flashBorderOnMouseOver;


        public bool FlashBorderOnMouseOver {
            get { return _flashBorderOnMouseOver; }
            set { _flashBorderOnMouseOver = value; }
        }
        #endregion


        #region property FlashBorderColorStart
        private Color _flashBorderColorStart = Color.FromArgb( 255, 9, 74, 178 );


        public Color FlashBorderColorStart {
            get { return _flashBorderColorStart; }
            set { _flashBorderColorStart = value; }
        }
        #endregion



        #region property BorderSize

        [EditorBrowsable]
        public int BorderSize {
            get { return _borderLayer.BorderSize; }
            set { _borderLayer.BorderSize = value; }
        }
        #endregion

        private bool _isMouseInside = false;

        private readonly BorderLayer _borderLayer = new BorderLayer();

        //to consider :
        // provide an automatic cleanup of brushes ? 
        // provide a simple interface and painters ?

        public CustomControl() {
            SetStyle( ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.CacheText | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true );
            DrawHandler.AddLayer( _borderLayer );

        }
        protected override void OnPaint( PaintEventArgs e ) {
            //result = new Bitmap( Width, Height, PixelFormat.Format32bppPArgb );
            //using (var g = Graphics.FromImage( result )) {
            //    testThis( g, e );
            //}
            //Console.WriteLine( "in OnPaint" );
            DrawHandler.Draw( e.Graphics, ClientRectangle, e.ClipRectangle );
            //testThis( e );

            //DrawHandler.drawAsync( this.CreateGraphics(), ClientRectangle, e.ClipRectangle, this ); // this is kinda slow (so far).

        }


        protected override void OnMouseEnter( EventArgs e ) {
            base.OnMouseEnter( e );
            _isMouseInside = true;
            FlashBorder();
        }

        private void FlashBorder() {
            if ( _flashBorderOnMouseOver ) {
                Color startCol = BorderColor;
                if ( _flashBorderColorStart != Color.Transparent ) {
                    startCol = FlashBorderColorStart;
                }
                Color oldColor = this.BorderColor;
                SharedAnimations.CycleColorLighting( ( Color col ) => {
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
            _isMouseInside = false;
        }

        public bool IsMouseInside() {
            return _isMouseInside;
        }
        protected override void OnResize( EventArgs e ) {
            base.OnResize( e );
            DrawHandler.Invalidate();
        }

    }
}
