using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers.backgrounds {
    public class GridBackground : BaseDraw {


        #region property SpaceBetween
        private int _SpaceBetween = 15*5;


        public int SpaceBetween {
            get { return _SpaceBetween; }
            set { _SpaceBetween = value; invalidate(); }
        }
        #endregion



        #region property lineSize
        private int _lineSize = 1;


        public int lineSize {
            get { return _lineSize; }
            set { _lineSize = value; }
        }
        #endregion


        #region property LineColor
        private Color _LineColor = Color.LightSlateGray;


        public Color LineColor {
            get { return _LineColor; }
            set {
                _LineColor = value;
                invalidate();
            }
        }
        #endregion



        ~GridBackground() {
            invalidate();
        }

        private TextureBrush cachedBrush = null;

        public override void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            handleCachedBrush();
            //g.ScaleTransform( 5, 5, System.Drawing.Drawing2D.MatrixOrder.Append );
            g.FillRectangle( cachedBrush, wholeComponent );
            
        }

        private void handleCachedBrush() {
            if ( cachedBrush == null ) {
                Bitmap pattern = new Bitmap( (int)((SpaceBetween)), (int)((SpaceBetween)), PixelFormat.Format32bppPArgb );

                using (var gg = Graphics.FromImage( pattern )) {
                    Pen blackPen = new Pen( Color.Black, lineSize );
                    Rectangle rec = new Rectangle( 0, 0, pattern.Width, pattern.Height );
                    gg.DrawRectangle( blackPen, rec );
                }

                cachedBrush = new TextureBrush( pattern, System.Drawing.Drawing2D.WrapMode.Tile, new Rectangle( 0, 0, pattern.Width, pattern.Height ) );
            }
        }

        public override void modifySize( ref Rectangle newSize ) {

        }
        public override void invalidate() {
            base.invalidate();
            cachedBrush = null;
        }
    }
}
