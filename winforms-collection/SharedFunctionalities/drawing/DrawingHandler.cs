using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace SharedFunctionalities.drawing {
    public class DrawingHandler {

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, UIntPtr count);

        private List<IDrawMethod> layers = new List<IDrawMethod>();

        private bool allowCommCache = true;


        //eventually make it work after 50 draws without invalidate ?
        private Bitmap commCacheBitmap;

        private int backPartsDepth = 0;

        private long totalDraws = 0;
        private double totalTime = 0.0d;

        private int notInvalidatedFor = 0;

        private void doDraw(Graphics g, Rectangle wholeComponent, Rectangle clipRect) {

            var orgRectangle = new Rectangle(wholeComponent.X, wholeComponent.Y, wholeComponent.Width, wholeComponent.Height);
            var startDrawAt = useCommCache(g, orgRectangle, false);

            if (allowCommCache && (commCacheBitmap == null || startDrawAt == 0)) {
                //make cache
                createCommCache(orgRectangle);
                startDrawAt = useCommCache(g, orgRectangle, true);
            }
            for (var i = startDrawAt; i < layers.Count; i++) {
                var drawer = layers[i];
                if (drawer.mayDraw()) {
                    var sw = new Stopwatch();
                    sw.Start();
                    drawLayer(g, ref wholeComponent, ref clipRect, i, orgRectangle);
                    sw.Stop();
                    //Console.WriteLine( "\tLayer {0}:{1}", i, sw.Elapsed.TotalMilliseconds );
                }
            }

            notInvalidatedFor++;
        }

        public void disableCommCache() {

            allowCommCache = false;
            invalidate();
        }

        public void draw(Graphics g, Rectangle wholeComponent, Rectangle clipRect) {
            var sw = new Stopwatch();
            sw.Start();
            doDraw(g, wholeComponent, clipRect);
            sw.Stop();
            totalDraws++;
            totalTime += sw.Elapsed.TotalMilliseconds;
            if (sw.Elapsed.TotalMilliseconds > 16) {
                Console.WriteLine("time:{0}, average:{1}", sw.Elapsed.TotalMilliseconds, (totalTime / (double)totalDraws));
            }

        }


        private int useCommCache(Graphics g, Rectangle orgRectangle, bool mayInvalidate) {
            if (notInvalidatedFor > 10) {
                var startDrawAt = 0; //skip unnessary parts.
                if (allowCommCache && commCacheBitmap != null && backPartsDepth > 0 && validateCommCache()) {
                    g.DrawImage(commCacheBitmap, orgRectangle);
                    startDrawAt = backPartsDepth;//use this instead.
                } else {
                    if (mayInvalidate) {
                        invalidate(); //make sure we dont use any resources unless nessary.
                    }
                }
                return startDrawAt;
            } else {
                return 0;
            }
        }


        private void createCommCache(Rectangle orgComponent) {
            if (notInvalidatedFor > 10) {
                //g.DrawImage( commCacheBitmap, orgRectangle );
                commCacheBitmap = new Bitmap(orgComponent.Width, orgComponent.Height, PixelFormat.Format32bppPArgb);
                using (var g = Graphics.FromImage(commCacheBitmap)) {
                    for (backPartsDepth = 0; backPartsDepth < layers.Count; backPartsDepth++) {
                        if (haveChanged(backPartsDepth)) {
                            break;
                        }
                        drawLayer(g, ref orgComponent, ref orgComponent, backPartsDepth, orgComponent);
                    }
                }
            }
        }

        private bool haveChanged(int index) {
            return (!layers[index].mayDraw()) || layers[index].isCacheInvalid() || (!layers[index].mayCacheLayer());
        }

        private bool validateCommCache() {
            for (var i = 0; i < backPartsDepth; i++) {
                if (haveChanged(i)) {
                    invalidate();
                    return false;
                }
            }
            return true;
        }

        private void drawLayer(Graphics g, ref Rectangle wholeComponent, ref Rectangle clipRect, int i, Rectangle orgRect) {
            layers[i].draw(g, ref wholeComponent, ref clipRect);
        }

        public void invalidate() {
            if (commCacheBitmap != null) {
                commCacheBitmap.Dispose();
            }
            notInvalidatedFor = 0;
            commCacheBitmap = null;
        }

        public void addLayer(IDrawMethod method) {
            layers.Add(method);
        }

        public void removeLayer(int index) {
            index = Math.Max(index, 0);
            layers.RemoveAt(index);
            invalidate();
        }

        public void moveLayer(int fromLayer, int toLayer) {
            IDrawMethod temp = layers[fromLayer];
            removeLayer(fromLayer);
            insertLayer(temp, toLayer);
        }
        public void insertLayer(IDrawMethod method, int index) {
            index = Math.Max(index, 0);
            if (index >= layers.Count) {
                addLayer(method);
            } else {
                layers.Insert(index, method);
            }
        }

        ~DrawingHandler() {
            invalidate();
        }

    }
}
