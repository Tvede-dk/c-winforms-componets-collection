using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedFunctionalities {
    public abstract class CustomControl : Control {


        private Pen borderPen;

        #region property BorderColor
        private Color _BorderColor;

        [EditorBrowsable]
        public Color BorderColor {
            get { return _BorderColor; }
            set {
                _BorderColor = value;
                updateBorderPen( value );
            }
        }

        private void updateBorderPen( Color value ) {
            if ( borderPen != null ) { borderPen.Dispose(); }
            borderPen = new Pen( value, BorderSize );
            Invalidate();
        }
        #endregion



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
        private int _BorderSize;

        [EditorBrowsable]
        public int BorderSize {
            get { return _BorderSize; }
            set {
                _BorderSize = value;
                updateBorderPen( BorderColor );
            }
        }
        #endregion

        private bool isMouseInside = false;


        ~CustomControl() {
            borderPen.Dispose();
        }

        //to consider :
        // provide an automatic cleanup of brushes ? 
        // provide a simple interface and painters ?

        public CustomControl() {
            SetStyle( ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.CacheText | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true );
        }

        protected override void OnPaint( PaintEventArgs e ) {
            base.OnPaint( e );
            drawBorder( e );

        }

        protected void drawBorder( PaintEventArgs e ) {
            if ( BorderSize > 0 ) {
                if ( borderPen == null ) {
                    borderPen = new Pen( Brushes.Black, (float)BorderSize );
                }
                e.Graphics.DrawRectangle( borderPen, ClientRectangle.InnerPart( BorderSize / 2, BorderSize / 2, BorderSize, BorderSize ) );
            }
        }

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

    }
}
