using System.Drawing;
using System.Drawing.Drawing2D;

namespace SharedFunctionalities.drawing.layers.backgrounds {
    public class TriangularShapeBackground : BaseDraw {


        #region property startColor
        private Color _startColor = Color.FromArgb( 255, 230, 240, 163 );


        public Color StartColor {
            get { return _startColor; }
            set {
                _startColor = value;
                Invalidate();
            }
        }
        #endregion


        #region property endColor
        private Color _endColor = Color.FromArgb( 255, 210, 230, 56 );


        public Color EndColor {
            get { return _endColor; }
            set {
                _endColor = value;
                Invalidate();
            }
        }
        #endregion



        public override void DoDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            using (var toUse = new LinearGradientBrush( wholeComponent, StartColor, EndColor, 90f, true )) {
                toUse.SetBlendTriangularShape( 0.5f, 1.0f );
                g.FillRectangle( toUse, wholeComponent );
            }
        }

        public override void ModifySize( ref Rectangle newSize ) {
            return;
        }
    }
}
