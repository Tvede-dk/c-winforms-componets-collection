﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace SharedFunctionalities.drawing.layers.backgrounds {
    public class GridBackground : BaseDraw {


        #region property SpaceBetween
        private int _spaceBetween = 15 * 5;


        public int SpaceBetween {
            get { return _spaceBetween; }
            set { _spaceBetween = value; Invalidate(); }
        }
        #endregion



        #region property lineSize
        private int _lineSize = 1;


        public int LineSize {
            get { return _lineSize; }
            set { _lineSize = value; }
        }
        #endregion

        //public int translateX = 0;
        #region property LineColor
        private Color _lineColor = Color.LightSlateGray;


        public Color LineColor {
            get { return _lineColor; }
            set {
                _lineColor = value;
                Invalidate();
            }
        }
        #endregion



        ~GridBackground() {
            Invalidate();
        }

        public override void DoDraw(Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect) {
            var watch = new Stopwatch();
            watch.Start();
            Rectangle newRec = wholeComponent;
            var maxVal = Math.Max(wholeComponent.Width, wholeComponent.Height);
            //if ((maxVal / SpaceBetween) <= 16) {
            //    g.Clear(Color.White);
            //    drawGridOnGraphics(g, wholeComponent.Width, wholeComponent.Height);
            //} else {
                wholeComponent = DrawUsingBitblt(g, newRec);
            //}
            watch.Stop();
            if (watch.Elapsed.TotalMilliseconds > 1) {
                Console.WriteLine("time is:" + watch.Elapsed.TotalMilliseconds + "ms");
            }
        }

        private Rectangle DrawUsingBitblt(Graphics g, Rectangle wholeComponent) {
            Bitmap pattern = CreatePattern();
            pattern.BitbltRepeat(g, wholeComponent.Width, wholeComponent.Height);
            pattern.Dispose();
            return wholeComponent;
        }

        private Bitmap CreatePattern() {
            #region a far, close, and standard distance mode based on simple benchmarking.
            var preFactor = 10;
            var divisionFactor = 20;
            if (SpaceBetween > 150) {
                preFactor = 8;
            }
            if (SpaceBetween < 20) {
                preFactor = 20;
            }
            #endregion
            var totalSize = (preFactor * SpaceBetween) - ((SpaceBetween * SpaceBetween) / divisionFactor);
            totalSize = totalSize - (totalSize % SpaceBetween);
            totalSize = Math.Max(totalSize, SpaceBetween);
            var pattern = new Bitmap((int)((totalSize)), (int)((totalSize)), PixelFormat.Format32bppPArgb);
            using (var gg = Graphics.FromImage(pattern)) {

                var sw = new Stopwatch();
                sw.Start();
                gg.Clear(Color.White);
                DrawGridOnGraphics(gg, totalSize, totalSize);

                sw.Stop();
                Console.WriteLine("time for making pattern:" + sw.Elapsed.TotalMilliseconds);
            }
            return pattern;
        }

        private void DrawGridOnGraphics(Graphics gg, int width, int height) {

            var blackPen = new Pen(Color.Black, LineSize);
            for (var i = 0; i < Math.Ceiling((double)height / (double)SpaceBetween); i++) {
                gg.DrawLine(Pens.Black, 0, SpaceBetween * i, width, SpaceBetween * i);
            }
            for (var i = 0; i < Math.Ceiling((double)width / (double)SpaceBetween); i++) {
                gg.DrawLine(Pens.Black, SpaceBetween * i, 0, SpaceBetween * i, height);
            }
        }

        public override void ModifySize(ref Rectangle newSize) {

        }
        public override void Invalidate() {
            base.Invalidate();
        }
    }
}
