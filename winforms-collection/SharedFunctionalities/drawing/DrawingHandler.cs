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

        private List<IDrawMethod> layers = new List<IDrawMethod>();

        private bool allowCommCache = true;

        private Bitmap commCacheBitmap;

        private int backPartsDepth = 0;

        private long totalDraws = 0;
        private double totalTime = 0.0d;


        private void doDraw( Graphics g, Rectangle wholeComponent, Rectangle clipRect ) {
            var orgRectangle = new Rectangle( wholeComponent.X, wholeComponent.Y, wholeComponent.Width, wholeComponent.Height );
            int startDrawAt = useCommCache( g, orgRectangle );
            for ( int i = startDrawAt; i < layers.Count; i++ ) {
                var drawer = layers[i];
                if ( drawer.mayDraw() ) {
                    drawLayer( g, ref wholeComponent, ref clipRect, i, orgRectangle );
                }
            }
            if ( allowCommCache && (commCacheBitmap == null || startDrawAt == 0) ) {
                createCommCache( orgRectangle );
            }
        }

        public void draw( Graphics g, Rectangle wholeComponent, Rectangle clipRect ) {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            doDraw( g, wholeComponent, clipRect );
            sw.Stop();
            totalDraws++;
            totalTime += sw.Elapsed.TotalMilliseconds;
            Console.WriteLine( "time:{0}, average:{1}", sw.Elapsed.TotalMilliseconds, (totalTime / (double)totalDraws) );
        }


        private int useCommCache( Graphics g, Rectangle orgRectangle ) {
            int startDrawAt = 0; //skip unnessary parts.
            if ( allowCommCache && commCacheBitmap != null && backPartsDepth > 0 && validateCommCache() ) { //can we use it.
                g.DrawImage( commCacheBitmap, orgRectangle );
                startDrawAt = backPartsDepth;//use this instead.
            } else {
                invalidate(); //make sure we dont use any resources unless nessary.
            }
            return startDrawAt;
        }

        private void createCommCache( Rectangle orgComponent ) {
            commCacheBitmap = new Bitmap( orgComponent.Width, orgComponent.Height, PixelFormat.Format32bppPArgb );
            using (var g = Graphics.FromImage( commCacheBitmap )) {
                for ( backPartsDepth = 0; backPartsDepth < layers.Count; backPartsDepth++ ) {
                    if ( haveChanged( backPartsDepth ) ) {
                        break;
                    }
                    drawLayer( g, ref orgComponent, ref orgComponent, backPartsDepth, orgComponent );
                }
            }

        }

        private bool haveChanged( int index ) {
            return (!layers[index].mayDraw()) || layers[index].isCacheInvalid() || (!layers[index].mayCacheLayer());
        }

        private bool validateCommCache() {
            for ( int i = 0; i < backPartsDepth; i++ ) {
                if ( haveChanged( i ) ) {
                    invalidate();
                    return false;
                }
            }
            return true;
        }

        private void drawLayer( Graphics g, ref Rectangle wholeComponent, ref Rectangle clipRect, int i, Rectangle orgRect ) {
            layers[i].draw( g, ref wholeComponent, ref clipRect );
        }

        public void invalidate() {
            if ( commCacheBitmap != null ) {
                commCacheBitmap.Dispose();
            }
            commCacheBitmap = null;
        }

        public void addLayer( IDrawMethod method ) {
            layers.Add( method );
        }
        public void insertLayer( IDrawMethod method, int index ) {
            index = Math.Max( index, 0 );
            if ( index >= layers.Count ) {
                addLayer( method );
            } else {
                layers.Insert( index, method );
            }
        }

        ~DrawingHandler() {
            invalidate();
        }

    }
}
