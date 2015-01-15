using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers {
    public class TextBaseLayer : IDrawMethod {



        #region property displayBrush
        private Brush _displayBrush;


        public Brush displayBrush {
            get { return _displayBrush; }
            set {
                if ( _displayBrush != null ) {
                    _displayBrush.Dispose();
                }
                _displayBrush = value;
                haveChangedSinceLastDraw = true;
            }
        }
        #endregion


        #region property stringFormat
        private StringFormat _stringFormat;


        public StringFormat stringFormat {
            get { return _stringFormat; }
            set { _stringFormat = value; }
        }
        #endregion


        #region property Text
        private string _Text;


        public string Text {
            get { return _Text; }
            set {
                _Text = value;
                haveChangedSinceLastDraw = true;
            }
        }
        #endregion


        #region property Font
        private Font _Font;


        public Font font {
            get { return _Font; }
            set {
                _Font = value;
                haveChangedSinceLastDraw = true;
            }
        }
        #endregion


        private bool haveChangedSinceLastDraw = true;


        public TextBaseLayer() {
            stringFormat = StringFormat.GenericDefault;
            displayBrush = Brushes.Black;
        }

        public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            //the following is NESSARY for transperant backgrounds, which we always want..
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            // 
            g.DrawString( Text, font, displayBrush, wholeComponent, stringFormat );
            haveChangedSinceLastDraw = false;

        }

        public bool isCacheInvalid() {
            return haveChangedSinceLastDraw;
        }

        public bool isTransperant() {
            return true;
        }

        public bool mayCacheLayer() {
            return true;
        }

        public bool mayDraw() {
            return Text != null && Text.Length > 0 && font != null;
        }

        public bool willFillRectangleOut() {
            return false;
        }

        public void modifySize( ref Rectangle newSize ) {
            return;
        }
    }
}
