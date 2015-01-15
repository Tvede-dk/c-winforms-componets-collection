using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities.drawing.layers {
    public class CenterText : TextBaseLayer {

        public CenterText() {
            this.stringFormat.LineAlignment = StringAlignment.Center;
            this.stringFormat.Alignment = StringAlignment.Center;
        }
    }
}
