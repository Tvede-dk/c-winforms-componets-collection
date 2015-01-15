using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers {
    public class BrushBackground : IDrawMethod {



        #region property Background
        private Brush _Background;

        ~BrushBackground() {
            if ( _Background != null ) {
                _Background.Dispose();
            }
        }

        public Brush Background {
            get { return _Background; }
            set {
                _Background = value;
                haveChangedSinceDraw = true;
            }
        }
        #endregion


        private bool haveChangedSinceDraw = true;
        public virtual void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            g.FillRectangle( Background, wholeComponent );
            haveChangedSinceDraw = false;
        }

        public bool isCacheInvalid() {
            return haveChangedSinceDraw;
        }

        public bool isTransperant() {
            return true;
        }

        public bool mayCacheLayer() {
            return true;
        }

        public bool mayDraw() {
            return true;
        }

        public bool willFillRectangleOut() {
            return true;
        }
    }
}
