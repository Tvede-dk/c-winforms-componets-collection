using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers {
    public class TextBaseLayer : BaseDraw {



        #region property displayBrush
        private Brush _displayBrush;


        public Brush displayBrush {
            get { return _displayBrush; }
            set {
                if ( _displayBrush != null ) {
                    _displayBrush.Dispose();
                }
                _displayBrush = value;
                invalidate();
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
                invalidate();
            }
        }
        #endregion


        #region property Font
        private Font _Font;


        public Font font {
            get { return _Font; }
            set {
                _Font = value;
                invalidate();
            }
        }
        #endregion




        public TextBaseLayer() {
            stringFormat = StringFormat.GenericDefault;
            displayBrush = Brushes.Black;
        }

        public override void doDraw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
            //the following is NESSARY for transperant backgrounds, which we always want..
            //TextRender is slower (on my machine), this seems odd, but then again, windows 10 <3
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.DrawString( Text, font, displayBrush, wholeComponent, stringFormat );
        }


        public override bool mayDraw() {
            return Text != null && Text.Length > 0 && font != null;
        }

        public override bool willFillRectangleOut() {
            return false;
        }

        public override void modifySize( ref Rectangle newSize ) {
            return;
        }
    }
}
