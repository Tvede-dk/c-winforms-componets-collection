using System.Windows.Forms;

namespace SharedFunctionalities.forms {
    public class TransperantForm : Form {
        public TransperantForm() {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.Opaque, true);
        }
    }
}
