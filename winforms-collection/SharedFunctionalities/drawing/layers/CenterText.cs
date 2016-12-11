using System.Drawing;

namespace SharedFunctionalities.drawing.layers {
    public class CenterText : TextBaseLayer {

        public CenterText() {
            this.StringFormat.LineAlignment = StringAlignment.Center;
            this.StringFormat.Alignment = StringAlignment.Center;
        }
    }
}
