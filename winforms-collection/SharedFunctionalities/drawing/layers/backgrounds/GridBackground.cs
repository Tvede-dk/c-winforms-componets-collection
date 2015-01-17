using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers.backgrounds {
    public class GridBackground : BaseDraw {


        #region property SpaceBetween
        private int _SpaceBetween = 15 * 5;


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

        public override void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            int totalSize = (12 * SpaceBetween) - ((SpaceBetween * SpaceBetween) / 14);
            totalSize = totalSize - (totalSize % SpaceBetween);
            totalSize = Math.Max( totalSize, SpaceBetween * 2 );
            Bitmap pattern = new Bitmap( (int)((totalSize)), (int)((totalSize)), PixelFormat.Format32bppPArgb );
            using (var gg = Graphics.FromImage( pattern )) {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Pen blackPen = new Pen( Color.Black, lineSize );
                for ( int i = 0; i < totalSize / SpaceBetween; i++ ) {
                    gg.DrawLine( Pens.Black, 0, SpaceBetween * i, pattern.Width, SpaceBetween * i );
                }
                for ( int i = 0; i < totalSize / SpaceBetween; i++ ) {
                    gg.DrawLine( Pens.Black, SpaceBetween * i, 0, SpaceBetween * i, pattern.Height );
                }

                sw.Stop();
                Console.WriteLine( "time for making pattern:" + sw.Elapsed.TotalMilliseconds );
            }
            pattern.bitbltRepeat( g, wholeComponent.Width, wholeComponent.Height );
        }

        public override void modifySize( ref Rectangle newSize ) {

        }
        public override void invalidate() {
            base.invalidate();
        }
    }
}
