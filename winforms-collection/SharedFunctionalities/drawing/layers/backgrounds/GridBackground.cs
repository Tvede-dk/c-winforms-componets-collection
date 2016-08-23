using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

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

        //public int translateX = 0;
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

        public override void doDraw(Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect) {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Rectangle newRec = wholeComponent;
            int maxVal = Math.Max(wholeComponent.Width, wholeComponent.Height);
            //if ((maxVal / SpaceBetween) <= 16) {
            //    g.Clear(Color.White);
            //    drawGridOnGraphics(g, wholeComponent.Width, wholeComponent.Height);
            //} else {
                wholeComponent = drawUsingBitblt(g, newRec);
            //}
            watch.Stop();
            if (watch.Elapsed.TotalMilliseconds > 1) {
                Console.WriteLine("time is:" + watch.Elapsed.TotalMilliseconds + "ms");
            }
        }

        private Rectangle drawUsingBitblt(Graphics g, Rectangle wholeComponent) {
            Bitmap pattern = createPattern();
            pattern.bitbltRepeat(g, wholeComponent.Width, wholeComponent.Height);
            pattern.Dispose();
            return wholeComponent;
        }

        private Bitmap createPattern() {
            #region a far, close, and standard distance mode based on simple benchmarking.
            int preFactor = 10;
            int divisionFactor = 20;
            if (SpaceBetween > 150) {
                preFactor = 8;
            }
            if (SpaceBetween < 20) {
                preFactor = 20;
            }
            #endregion
            int totalSize = (preFactor * SpaceBetween) - ((SpaceBetween * SpaceBetween) / divisionFactor);
            totalSize = totalSize - (totalSize % SpaceBetween);
            totalSize = Math.Max(totalSize, SpaceBetween);
            Bitmap pattern = new Bitmap((int)((totalSize)), (int)((totalSize)), PixelFormat.Format32bppPArgb);
            using (var gg = Graphics.FromImage(pattern)) {

                Stopwatch sw = new Stopwatch();
                sw.Start();
                gg.Clear(Color.White);
                drawGridOnGraphics(gg, totalSize, totalSize);

                sw.Stop();
                Console.WriteLine("time for making pattern:" + sw.Elapsed.TotalMilliseconds);
            }
            return pattern;
        }

        private void drawGridOnGraphics(Graphics gg, int width, int height) {

            Pen blackPen = new Pen(Color.Black, lineSize);
            for (int i = 0; i < Math.Ceiling((double)height / (double)SpaceBetween); i++) {
                gg.DrawLine(Pens.Black, 0, SpaceBetween * i, width, SpaceBetween * i);
            }
            for (int i = 0; i < Math.Ceiling((double)width / (double)SpaceBetween); i++) {
                gg.DrawLine(Pens.Black, SpaceBetween * i, 0, SpaceBetween * i, height);
            }
        }

        public override void modifySize(ref Rectangle newSize) {

        }
        public override void invalidate() {
            base.invalidate();
        }
    }
}
