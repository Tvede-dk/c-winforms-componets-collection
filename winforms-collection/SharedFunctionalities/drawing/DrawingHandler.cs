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

        private readonly List<IDrawMethod> _layers = new List<IDrawMethod>();

        private bool _allowCommCache = true;


        //eventually make it work after 50 draws without invalidate ?
        private Bitmap _commCacheBitmap;

        private int _backPartsDepth = 0;

        private long _totalDraws = 0;
        private double _totalTime = 0.0d;

        private int _notInvalidatedFor = 0;

        private void DoDraw(Graphics g, Rectangle wholeComponent, Rectangle clipRect) {

            var orgRectangle = new Rectangle(wholeComponent.X, wholeComponent.Y, wholeComponent.Width, wholeComponent.Height);
            var startDrawAt = UseCommCache(g, orgRectangle, false);

            if (_allowCommCache && (_commCacheBitmap == null || startDrawAt == 0)) {
                //make cache
                CreateCommCache(orgRectangle);
                startDrawAt = UseCommCache(g, orgRectangle, true);
            }
            for (var i = startDrawAt; i < _layers.Count; i++) {
                var drawer = _layers[i];
                if (drawer.MayDraw()) {
                    var sw = new Stopwatch();
                    sw.Start();
                    DrawLayer(g, ref wholeComponent, ref clipRect, i, orgRectangle);
                    sw.Stop();
                    //Console.WriteLine( "\tLayer {0}:{1}", i, sw.Elapsed.TotalMilliseconds );
                }
            }

            _notInvalidatedFor++;
        }

        public void DisableCommCache() {

            _allowCommCache = false;
            Invalidate();
        }

        public void Draw(Graphics g, Rectangle wholeComponent, Rectangle clipRect) {
            var sw = new Stopwatch();
            sw.Start();
            DoDraw(g, wholeComponent, clipRect);
            sw.Stop();
            _totalDraws++;
            _totalTime += sw.Elapsed.TotalMilliseconds;
            if (sw.Elapsed.TotalMilliseconds > 16) {
                Console.WriteLine("time:{0}, average:{1}", sw.Elapsed.TotalMilliseconds, (_totalTime / (double)_totalDraws));
            }

        }


        private int UseCommCache(Graphics g, Rectangle orgRectangle, bool mayInvalidate) {
            if (_notInvalidatedFor > 10) {
                var startDrawAt = 0; //skip unnessary parts.
                if (_allowCommCache && _commCacheBitmap != null && _backPartsDepth > 0 && ValidateCommCache()) {
                    g.DrawImage(_commCacheBitmap, orgRectangle);
                    startDrawAt = _backPartsDepth;//use this instead.
                } else {
                    if (mayInvalidate) {
                        Invalidate(); //make sure we dont use any resources unless nessary.
                    }
                }
                return startDrawAt;
            } else {
                return 0;
            }
        }


        private void CreateCommCache(Rectangle orgComponent) {
            if (_notInvalidatedFor > 10) {
                //g.DrawImage( commCacheBitmap, orgRectangle );
                _commCacheBitmap = new Bitmap(orgComponent.Width, orgComponent.Height, PixelFormat.Format32bppPArgb);
                using (var g = Graphics.FromImage(_commCacheBitmap)) {
                    for (_backPartsDepth = 0; _backPartsDepth < _layers.Count; _backPartsDepth++) {
                        if (HaveChanged(_backPartsDepth)) {
                            break;
                        }
                        DrawLayer(g, ref orgComponent, ref orgComponent, _backPartsDepth, orgComponent);
                    }
                }
            }
        }

        private bool HaveChanged(int index) {
            return (!_layers[index].MayDraw()) || _layers[index].IsCacheInvalid() || (!_layers[index].MayCacheLayer());
        }

        private bool ValidateCommCache() {
            for (var i = 0; i < _backPartsDepth; i++) {
                if (HaveChanged(i)) {
                    Invalidate();
                    return false;
                }
            }
            return true;
        }

        private void DrawLayer(Graphics g, ref Rectangle wholeComponent, ref Rectangle clipRect, int i, Rectangle orgRect) {
            _layers[i].Draw(g, ref wholeComponent, ref clipRect);
        }

        public void Invalidate() {
            if (_commCacheBitmap != null) {
                _commCacheBitmap.Dispose();
            }
            _notInvalidatedFor = 0;
            _commCacheBitmap = null;
        }

        public void AddLayer(IDrawMethod method) {
            _layers.Add(method);
        }

        public void RemoveLayer(int index) {
            index = Math.Max(index, 0);
            _layers.RemoveAt(index);
            Invalidate();
        }

        public void MoveLayer(int fromLayer, int toLayer) {
            IDrawMethod temp = _layers[fromLayer];
            RemoveLayer(fromLayer);
            InsertLayer(temp, toLayer);
        }
        public void InsertLayer(IDrawMethod method, int index) {
            index = Math.Max(index, 0);
            if (index >= _layers.Count) {
                AddLayer(method);
            } else {
                _layers.Insert(index, method);
            }
        }

        ~DrawingHandler() {
            Invalidate();
        }

    }
}
