using SharedFunctionalities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// contains extensions for forms. and wrappers.
/// </summary>
public static class FormExtensions {
    #region show topmost form without focus


    private const int SW_SHOWNOACTIVATE = 4;
    private const int HWND_TOPMOST = -1;
    private const uint SWP_NOACTIVATE = 0x0010;
    const UInt32 SWP_NOSIZE = 0x0001;
    const UInt32 SWP_NOMOVE = 0x0002;
    const UInt32 SWP_SHOWWINDOW = 0x0040;

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    static extern bool SetWindowPos(
         IntPtr hWnd,             // Window handle
         int hWndInsertAfter,  // Placement-order handle
         int X,                // Horizontal position
         int Y,                // Vertical position
         int cx,               // Width
         int cy,               // Height
         uint uFlags);         // Window positioning flags

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static void ShowInactiveTopmost<T>(this T frm) where T : Form {
        ShowWindow(frm.Handle, SW_SHOWNOACTIVATE);
        SetWindowPos(frm.Handle, HWND_TOPMOST,
        frm.Left, frm.Top, frm.Width, frm.Height,
        SWP_NOACTIVATE);
    }
    #endregion

    public static void setTopMost<T>(this T f) where T : Form {
        SetWindowPos(f.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);

    }

    #region animations

    public static void FadeIn<T>(this T frm, int displayTimeInMs, Action after, float maxVal = 1f, float startVal = 0f) where T : Form {
        SharedAnimations.fadeIn(frm, displayTimeInMs, after, maxVal, startVal);
    }
    public static void FadeOut<T>(this T frm, int displayTimeInMs, Action after, float minVal = 1f, float startVal = 0f) where T : Form {
        SharedAnimations.fadeOut(frm, displayTimeInMs, after, minVal, startVal);
    }

    public static void handleRemoteInvoke(this Control con, Action code) {
        if (con.InvokeRequired) {
            con.BeginInvoke(code);
        } else {
            code.Invoke();
        }
    }

    #endregion


    #region Dialogboxes


    public static void PerformIfOkay(this DialogResult result, Action onOk, DialogResult okCondition = DialogResult.OK) {
        if (result == okCondition) {
            onOk();
        }
    }

    public static void DoInvokeIfRequired(this Control control, Action callback) {
        if (control.InvokeRequired) {
            control.BeginInvoke(new MethodInvoker(callback));
        } else {
            callback();
        }
    }


    #endregion

}

