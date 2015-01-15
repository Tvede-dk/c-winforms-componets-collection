using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing {
    public class DrawingHandler {

        private bool mayCache = true;

        private int startIndex = 0;

        private List<IDrawMethod> layers = new List<IDrawMethod>();

        private List<Bitmap> caches = new List<Bitmap>();


        private bool allowCommCache = true;

        private Bitmap commCacheBitmap;

        private int backPartsDepth = 0;



        private long totalDraws = 0;
        private double totalTime = 0.0d;

        public DrawingHandler() {

        }

        public void draw( Graphics g, Rectangle wholeComponent, Rectangle clipRect ) {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            validateStartIndex(); //make sure we can use startindex as expected.
            int startDrawAt = startIndex;
            if ( allowCommCache && commCacheBitmap != null && validateCommCache() && backPartsDepth > 0 ) {
                g.DrawImage( commCacheBitmap, clipRect.X, clipRect.Y, clipRect, GraphicsUnit.Pixel );
                startDrawAt = backPartsDepth;
            }
            for ( int i = startDrawAt; i < layers.Count; i++ ) {
                var drawer = layers[i];
                if ( !drawer.mayDraw() ) {
                    continue;
                }
                drawLayer( g, ref wholeComponent, ref clipRect, i, drawer );
                if ( !drawer.isTransperant() && drawer.willFillRectangleOut() ) { //at this point we know that everthing below this is ACTUALLY never going to get drawn.
                    //so dont draw that stuff.
                    startIndex = i;
                }

            }

            if ( allowCommCache && commCacheBitmap == null ) {
                createCommCache( wholeComponent );
            }
            sw.Stop();
            totalDraws++;
            totalTime += sw.Elapsed.TotalMilliseconds;
            Console.WriteLine( "time:{0}, average:{1}", sw.Elapsed.TotalMilliseconds, (totalTime / (double)totalDraws) );
        }

        private void createCommCache( Rectangle wholeComponent ) {
            commCacheBitmap = new Bitmap( wholeComponent.Width, wholeComponent.Height, PixelFormat.Format32bppPArgb );
            using (var g = Graphics.FromImage( commCacheBitmap )) {
                var point = new Point( 0, 0 );
                int i = 0;
                for ( i = startIndex; i < layers.Count; i++ ) {
                    if ( (!layers[i].mayDraw()) || layers[i].isCacheInvalid() || (!layers[i].mayCacheLayer()) ) {
                        break;
                    }
                    g.DrawImage( caches[i], point );
                }
                backPartsDepth = i;
                if ( backPartsDepth == 0 ) {
                    commCacheBitmap = null;
                }
            }
        }

        private bool validateCommCache() {
            for ( int i = 0; i < backPartsDepth; i++ ) {
                if ( layers[i].isCacheInvalid() && layers[i].mayDraw() || (layers[i].mayCacheLayer() == false) ) {
                    commCacheBitmap = null;
                    return false;
                }
            }
            return true;
        }

        private void drawLayer( Graphics g, ref Rectangle wholeComponent, ref Rectangle clipRect, int i, IDrawMethod drawer ) {
            #region get cache
            Bitmap cache = null;
            if ( caches != null && caches.Count > i ) {
                cache = caches[i];
            }
            #endregion
            if ( cache != null && (!drawer.isCacheInvalid()) ) { //do we have a cache and is the cache valid ? 
                g.DrawImage( cache, clipRect.X, clipRect.Y, clipRect, GraphicsUnit.Pixel );
                //g.DrawImage( cache, wholeComponent );
            } else {
                #region no cache found
                if ( mayCache && drawer.mayCacheLayer() ) {
                    caches[i] = new Bitmap( wholeComponent.Width, wholeComponent.Height, PixelFormat.Format32bppPArgb );

                    using (var gg = Graphics.FromImage( caches[i] )) {
                        //g.Clear( Color.Transparent );
                        drawer.draw( gg, ref wholeComponent, ref wholeComponent );
                    }
                    //caches[i].MakeTransparent( Color.Transparent );
                    //g.DrawImage( caches[i], wholeComponent );
                    g.DrawImage( caches[i], clipRect.X, clipRect.Y, clipRect, GraphicsUnit.Pixel );
                    //then create cache.
                } else {
                    //no caching allowed.
                    drawer.draw( g, ref wholeComponent, ref clipRect );
                }
                #endregion
            }
        }

        public void invalidate() {
            for ( int i = 0; i < layers.Count && i < caches.Count; i++ ) {
                caches[i] = null;
            }
            commCacheBitmap = null;
        }

        private void validateStartIndex() {
            if ( startIndex >= 0 && startIndex < layers.Count ) {
                if ( layers[startIndex].isCacheInvalid() || !layers[startIndex].mayCacheLayer()
                    || !layers[startIndex].willFillRectangleOut() || layers[startIndex].isTransperant() || !layers[startIndex].mayDraw() ) { //our assumtions holds ?
                    startIndex = 0;
                }
            } else {
                startIndex = 0;
            }
        }

        public void deactiveCache() {
            mayCache = false;
            caches = null;
        }

        public void activateCache() {
            if ( mayCache ) {
                return;
            } else {
                mayCache = true;
            }
        }

        public void addLayer( IDrawMethod method ) {
            layers.Add( method );
            caches.Add( null );
        }
        public void insertLayer( IDrawMethod method, int index ) {
            index = Math.Max( index, 0 );
            if ( index >= layers.Count ) {
                addLayer( method );
            } else {
                layers.Insert( index, method );
                if ( startIndex >= index ) { //if we would not use it, then propperly thats an error, or we have changed something radically.
                    startIndex = 0;
                }
            }
        }


    }
}
