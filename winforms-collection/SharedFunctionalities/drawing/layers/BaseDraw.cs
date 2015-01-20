using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers {
    /// <summary>
    /// combines most of the required stuff to implement the iDrawMethod.
    /// </summary>
    public abstract class BaseDraw : IDrawMethod {
        private bool haveChangedSinceDraw = true;
        public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            haveChangedSinceDraw = false;
            doDraw( g, ref wholeComponent, ref clippingRect );
        }

        public abstract void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect );


        public bool isCacheInvalid() {
            return haveChangedSinceDraw;
        }

        public virtual bool isTransperant() {
            return true;
        }

        public virtual bool mayCacheLayer() {
            return true;
        }

        public virtual bool mayDraw() {
            return true;
        }

        public virtual bool willFillRectangleOut() {
            return true;
        }

        public virtual void invalidate() {
            haveChangedSinceDraw = true;
        }

        public abstract void modifySize( ref Rectangle newSize );
    }
}
