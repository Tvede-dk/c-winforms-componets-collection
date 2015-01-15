using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers {
    public class DrawingGroup : BaseDraw {


        private List<IDrawMethod> composition = new List<IDrawMethod>();

        public DrawingGroup addMethod( IDrawMethod method ) {
            composition.Add( method );
            return this;
        }

        public override void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            drawAll( g, ref wholeComponent, ref clippingRect );
        }

        private void drawAll( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            foreach ( var item in composition ) {
                item.draw( g, ref wholeComponent, ref clippingRect );
            }
        }


        public override void modifySize( ref Rectangle newSize ) {
            foreach ( var item in composition ) {
                item.modifySize( ref newSize );
            }
        }
    }
}
