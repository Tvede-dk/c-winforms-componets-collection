using System;
using System.Windows.Forms;

namespace SharedFunctionalities.forms {
    /// <summary>
    /// a form that simply doesnt respond to mouse or keyboard events (pass though).
    /// </summary>
    public class BasePassthough : TransperantForm {


        protected override void WndProc( ref Message m ) {
            const int WM_NCHITTEST = 0x0084;
            const int WM_TIMER = 0x0113;
            const int HTTRANSPARENT = (-1);
            switch ( m.Msg ) {
                case WM_NCHITTEST:
                    m.Result = (IntPtr)HTTRANSPARENT;
                    break;
                case WM_TIMER:
                default:
                    base.WndProc( ref m );
                    break;
            }
            //if ( m.Msg == WM_TIMER ) {
            //    base.WndProc( ref m );
            //}
            //if ( m.Msg == WM_NCHITTEST ) {
            //    m.Result = (IntPtr)HTTRANSPARENT;
            //} else {
            //    base.WndProc( ref m );
            //}
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }
    }
}
