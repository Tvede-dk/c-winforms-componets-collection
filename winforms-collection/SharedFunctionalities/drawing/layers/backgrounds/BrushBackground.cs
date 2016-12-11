using System.Drawing;

namespace SharedFunctionalities.drawing.layers.backgrounds {
    public class BrushBackground : BaseDraw {

        #region property Background
        private Brush _background = Brushes.Black;

        ~BrushBackground() {
            if ( _background != null ) {
                _background.Dispose();
            }
        }

        public Brush Background {
            get { return _background; }
            set {
                _background = value;
                Invalidate();
            }
        }
        #endregion

        public override void DoDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            g.FillRectangle( Background, wholeComponent );
        }

        public override void ModifySize( ref Rectangle newSize ) {
            return;
        }
    }
}
