using System.Drawing;

namespace SharedFunctionalities.drawing.layers {
    public class BorderLayer : BaseDraw {

        private Pen _borderPen;

        #region property BorderColor
        private Color _borderColor = Color.Black;

        public Color BorderColor {
            get { return _borderColor; }
            set {
                _borderColor = value;
                UpdateBorderPen( value );
            }
        }
        #endregion
        private void UpdateBorderPen( Color value ) {
            if ( _borderPen != null ) { _borderPen.Dispose(); }
            _borderPen = new Pen( value, BorderSize );
            Invalidate();
        }

        #region property BorderSize
        private int _borderSize = 1;

        ~BorderLayer() {
            if ( _borderPen != null ) {
                _borderPen.Dispose();
            }
        }



        public int BorderSize {
            get { return _borderSize; }
            set { _borderSize = value; UpdateBorderPen( BorderColor ); }
        }
        #endregion


        public override void DoDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            if ( _borderPen == null ) {
                _borderPen = new Pen( BorderColor, (float)BorderSize );
            }
            Rectangle rec = wholeComponent.CalculateBorder( BorderSize );
            g.DrawRectangle( _borderPen, rec );
            wholeComponent = wholeComponent.CalculateInsideBorder( BorderSize );
        }


        public override void ModifySize( ref Rectangle newSize ) {
            newSize = newSize.CalculateInsideBorder( BorderSize );
        }
    }
}
