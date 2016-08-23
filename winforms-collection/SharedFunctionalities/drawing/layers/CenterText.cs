using System.Drawing;

namespace SharedFunctionalities.drawing.layers {
    public class CenterText : TextBaseLayer {

        public CenterText() {
            this.stringFormat.LineAlignment = StringAlignment.Center;
            this.stringFormat.Alignment = StringAlignment.Center;
        }
    }
}
