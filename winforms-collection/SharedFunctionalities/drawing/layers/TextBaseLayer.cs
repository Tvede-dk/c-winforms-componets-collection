using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace SharedFunctionalities.drawing.layers {
    public class TextBaseLayer : BaseDraw {



        #region property displayBrush
        private Brush _displayBrush;


        public Brush DisplayBrush {
            get { return _displayBrush; }
            set {
                if (_displayBrush != null) {
                    _displayBrush.Dispose();
                }
                _displayBrush = value;
                Invalidate();
            }
        }
        #endregion


        #region property stringFormat
        private StringFormat _stringFormat;


        public StringFormat StringFormat {
            get { return _stringFormat; }
            set { _stringFormat = value; }
        }
        #endregion


        #region property Text
        private string _text;


        public string Text {
            get { return _text; }
            set {
                _text = value;
                Invalidate();
            }
        }
        #endregion


        #region property Font
        private Font _font;


        public Font Font {
            get { return _font; }
            set {
                _font = value;
                Invalidate();
            }
        }
        #endregion




        public TextBaseLayer() {
            StringFormat = StringFormat.GenericDefault;
            DisplayBrush = new SolidBrush(Color.Black);
        }

        public override void DoDraw(Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect) {
            //the following is NESSARY for transperant backgrounds, which we always want..
            //TextRender is slower (on my machine), this seems odd, but then again, windows 10 <3
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.DrawString(Text, Font, DisplayBrush, wholeComponent, StringFormat);
        }


        public override bool MayDraw() {
            return Text != null && Text.Length > 0 && Font != null;
        }

        public override bool WillFillRectangleOut() {
            return false;
        }

        public override void ModifySize(ref Rectangle newSize) {
            return;
        }
    }
}
