using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers {
    public class BorderLayer : IDrawMethod {

        private bool haveChangedSinceLastDraw = true;

        private Pen borderPen;

        #region property BorderColor
        private Color _BorderColor;

        public Color BorderColor {
            get { return _BorderColor; }
            set {
                _BorderColor = value;
                updateBorderPen( value );
            }
        }
        #endregion
        private void updateBorderPen( Color value ) {
            if ( borderPen != null ) { borderPen.Dispose(); }
            borderPen = new Pen( value, BorderSize );
            haveChangedSinceLastDraw = true;
        }

        #region property BorderSize
        private int _BorderSize;

        ~BorderLayer() {
            if ( borderPen != null ) {
                borderPen.Dispose();
            }
        }

        public int BorderSize {
            get { return _BorderSize; }
            set { _BorderSize = value; haveChangedSinceLastDraw = true; updateBorderPen( BorderColor ); }
        }
        #endregion


        public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            if ( borderPen == null ) {
                borderPen = new Pen( Brushes.Black, (float)BorderSize );
            }
            Rectangle rec = wholeComponent.InnerPart( BorderSize / 2, BorderSize / 2, BorderSize, BorderSize );
            g.DrawRectangle( borderPen, rec );
            //set the rest as the inside.
            wholeComponent = new Rectangle( BorderSize, BorderSize, wholeComponent.Width - BorderSize, wholeComponent.Height - BorderSize );
            haveChangedSinceLastDraw = false;
        }

        public bool isCacheInvalid() {
            return haveChangedSinceLastDraw;
        }

        public bool isTransperant() {
            return true;
        }

        public bool mayCacheLayer() {
            return true;
        }

        public bool mayDraw() {
            return BorderSize > 0;
        }

        public bool willFillRectangleOut() {
            return false;
        }
    }
}
