using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers.backgrounds {
    public class BrushBackground : BaseDraw {

        #region property Background
        private Brush _Background = Brushes.Black;

        ~BrushBackground() {
            if ( _Background != null ) {
                _Background.Dispose();
            }
        }

        public Brush Background {
            get { return _Background; }
            set {
                _Background = value;
                invalidate();
            }
        }
        #endregion

        public override void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            g.FillRectangle( Background, wholeComponent );
        }

        public override void modifySize( ref Rectangle newSize ) {
            return;
        }
    }
}
