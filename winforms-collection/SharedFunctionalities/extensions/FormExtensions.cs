using SharedFunctionalities;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>
/// contains extensions for forms. and wrappers.
/// </summary>
public static class FormExtensions {
    #region show topmost form without focus


    private const int SwShownoactivate = 4;
    private const int HwndTopmost = -1;
    private const uint SwpNoactivate = 0x0010;
    const UInt32 SwpNosize = 0x0001;
    const UInt32 SwpNomove = 0x0002;
    const UInt32 SwpShowwindow = 0x0040;

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    static extern bool SetWindowPos(
         IntPtr hWnd,             // Window handle
         int hWndInsertAfter,  // Placement-order handle
         int x,                // Horizontal position
         int y,                // Vertical position
         int cx,               // Width
         int cy,               // Height
         uint uFlags);         // Window positioning flags

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static void ShowInactiveTopmost<T>(this T frm) where T : Form {
        ShowWindow(frm.Handle, SwShownoactivate);
        SetWindowPos(frm.Handle, HwndTopmost,
        frm.Left, frm.Top, frm.Width, frm.Height,
        SwpNoactivate);
    }
    #endregion

    public static void SetTopMost<T>(this T f) where T : Form {
        SetWindowPos(f.Handle, HwndTopmost, 0, 0, 0, 0, SwpNomove | SwpNosize | SwpShowwindow);

    }

    #region animations

    public static void FadeIn<T>(this T frm, int displayTimeInMs, Action after, float maxVal = 1f, float startVal = 0f) where T : Form {
        SharedAnimations.FadeIn(frm, displayTimeInMs, after, maxVal, startVal);
    }
    public static void FadeOut<T>(this T frm, int displayTimeInMs, Action after, float minVal = 1f, float startVal = 0f) where T : Form {
        SharedAnimations.FadeOut(frm, displayTimeInMs, after, minVal, startVal);
    }

    public static void HandleRemoteInvoke(this Control con, Action code) {
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

