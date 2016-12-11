using System.Collections.Generic;
using System.Drawing;

namespace SharedFunctionalities.drawing.layers {
    public class DrawingGroup : BaseDraw {


        private readonly List<IDrawMethod> _composition = new List<IDrawMethod>();

        public DrawingGroup AddMethod( IDrawMethod method ) {
            _composition.Add( method );
            return this;
        }

        public override void DoDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            DrawAll( g, ref wholeComponent, ref clippingRect );
        }

        private void DrawAll( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            foreach ( var item in _composition ) {
                item.Draw( g, ref wholeComponent, ref clippingRect );
            }
        }


        public override void ModifySize( ref Rectangle newSize ) {
            foreach ( var item in _composition ) {
                item.ModifySize( ref newSize );
            }
        }
    }
}
