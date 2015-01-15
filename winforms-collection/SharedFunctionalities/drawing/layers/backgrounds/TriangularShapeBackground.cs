using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers.backgrounds {
    public class TriangularShapeBackground : BaseDraw {


        #region property startColor
        private Color _startColor = Color.FromArgb( 255, 230, 240, 163 );


        public Color startColor {
            get { return _startColor; }
            set {
                _startColor = value;
                invalidate();
            }
        }
        #endregion


        #region property endColor
        private Color _endColor = Color.FromArgb( 255, 210, 230, 56 );


        public Color endColor {
            get { return _endColor; }
            set {
                _endColor = value;
                invalidate();
            }
        }
        #endregion



        public override void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            using (LinearGradientBrush toUse = new LinearGradientBrush( wholeComponent, startColor, endColor, 90f, true )) {
                toUse.SetBlendTriangularShape( 0.5f, 1.0f );
                g.FillRectangle( toUse, wholeComponent );
            }
        }

        public override void modifySize( ref Rectangle newSize ) {
            return;
        }
    }
}
