using System.Drawing;

namespace SharedFunctionalities.drawing.layers {
    public class BorderLayer : BaseDraw {

        private Pen borderPen;

        #region property BorderColor
        private Color _BorderColor = Color.Black;

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
            invalidate();
        }

        #region property BorderSize
        private int _BorderSize = 1;

        ~BorderLayer() {
            if ( borderPen != null ) {
                borderPen.Dispose();
            }
        }



        public int BorderSize {
            get { return _BorderSize; }
            set { _BorderSize = value; updateBorderPen( BorderColor ); }
        }
        #endregion


        public override void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            if ( borderPen == null ) {
                borderPen = new Pen( BorderColor, (float)BorderSize );
            }
            Rectangle rec = wholeComponent.CalculateBorder( BorderSize );
            g.DrawRectangle( borderPen, rec );
            wholeComponent = wholeComponent.CalculateInsideBorder( BorderSize );
        }


        public override void modifySize( ref Rectangle newSize ) {
            newSize = newSize.CalculateInsideBorder( BorderSize );
        }
    }
}
