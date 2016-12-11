using System.Drawing;

namespace SharedFunctionalities.drawing.layers {
    /// <summary>
    /// combines most of the required stuff to implement the iDrawMethod.
    /// </summary>
    public abstract class BaseDraw : IDrawMethod {
        private bool _haveChangedSinceDraw = true;
        public void Draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            _haveChangedSinceDraw = false;
            DoDraw( g, ref wholeComponent, ref clippingRect );
        }

        public abstract void DoDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect );


        public bool IsCacheInvalid() {
            return _haveChangedSinceDraw;
        }

        public virtual bool IsTransperant() {
            return true;
        }

        public virtual bool MayCacheLayer() {
            return true;
        }

        public virtual bool MayDraw() {
            return true;
        }

        public virtual bool WillFillRectangleOut() {
            return true;
        }

        public virtual void Invalidate() {
            _haveChangedSinceDraw = true;
        }

        public abstract void ModifySize( ref Rectangle newSize );
    }
}
