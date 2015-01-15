using SharedFunctionalities.drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.drawParts {
    public class testDraw : Control {

        private DrawingHandler drawer = new DrawingHandler();



        public testDraw() {
            SetStyle( ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.CacheText | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true );
            drawer.addLayer( new backgroundDrawer() );
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            drawer.addLayer( new TextDrawer( Text, Font, Brushes.Black, format ) );
        }



        protected override void OnPaint( PaintEventArgs e ) {

            //drawer.draw( e.Graphics, ClientRectangle, e.ClipRectangle );

        }

        protected override void OnResize( EventArgs e ) {
            base.OnResize( e );
            drawer.invalidate();
        }


        private class TextDrawer : IDrawMethod {

            private StringFormat format;
            private Font font;
            private Brush brushToUse;
            private String text;
            public TextDrawer( String text, Font fnt, Brush brush, StringFormat drawFormat ) {
                this.text = text;
                this.format = drawFormat;
                brushToUse = brush;
                font = fnt;
            }

            ~TextDrawer() {
                brushToUse.Dispose();
            }

            public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                g.DrawString( " asd ", font, Brushes.Bisque, clippingRect, format );
            }

            public bool isCacheInvalid() {
                return false;
            }

            public bool isTransperant() {
                return true;
            }

            public bool mayCacheLayer() {
                return true;
            }

            public bool willFillRectangleOut() {
                return false;
            }

            public bool mayDraw() {
                return true;
            }
        }


        private class backgroundDrawer : IDrawMethod {
            public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                using (LinearGradientBrush lgb = new LinearGradientBrush( wholeComponent, Color.AliceBlue, Color.Crimson, 0.45f )) {
                    lgb.SetSigmaBellShape( 0.5f );
                    g.FillRectangle( lgb, wholeComponent );
                }
            }

            public bool isCacheInvalid() {
                return false;
            }

            public bool isTransperant() {
                return false;
            }

            public bool mayCacheLayer() {
                return true;
            }

            public bool mayDraw() {
                return true;
            }

            public bool willFillRectangleOut() {
                return true;
            }
        }
    }

}
