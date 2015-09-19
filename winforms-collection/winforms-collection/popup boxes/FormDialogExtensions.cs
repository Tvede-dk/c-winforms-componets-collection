using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class FormDialogExtensionss {

    public static void ShowDialog<T>(this T form, Action<T> onSucess, Action<T> onFailed) where T : Form {
        DialogCommenCode.HandleResult(form, form.ShowDialog(), onSucess, onFailed);
    }

    public static void ShowDialog<T>(this T form, Form parrent, Action<T> onSucess, Action<T> onFailed) where T : Form {
        DialogCommenCode.HandleResult(form, form.ShowDialog(parrent), onSucess, onFailed);
    }

    public static void ShowDialog<T>(this T form, Form parrent, Action<T> onSucess) where T : Form {
        DialogCommenCode.HandleResult(form, form.ShowDialog(parrent), onSucess, null);
    }
    public static void ShowDialog<T>(this T form, Action<T> onSucess) where T : Form {
        DialogCommenCode.HandleResult(form, form.ShowDialog(), onSucess, null);
    }

}


public static class CommenDialogExt {
    public static void ShowCommenDialog<T>(this T form, Action<T> onSucess, Action<T> onFailed) where T : CommonDialog {
        DialogCommenCode.HandleResult(form, form.ShowDialog(), onSucess, onFailed);
    }

    public static void ShowCommenDialog<T>(this T form, Form parrent, Action<T> onSucess, Action<T> onFailed) where T : CommonDialog {
        DialogCommenCode.HandleResult(form, form.ShowDialog(parrent), onSucess, onFailed);
    }

    public static void ShowCommenDialog<T>(this T form, Form parrent, Action<T> onSucess) where T : CommonDialog {
        DialogCommenCode.HandleResult(form, form.ShowDialog(parrent), onSucess, null);
    }
    public static void ShowCommenDialog<T>(this T form, Action<T> onSucess) where T : CommonDialog {
        DialogCommenCode.HandleResult(form, form.ShowDialog(), onSucess, null);
    }
}

static class DialogCommenCode {
    public static void HandleResult<T>(T dialog, DialogResult result, Action<T> success, Action<T> error) where T : IDisposable{
        if (result == DialogResult.OK || result == DialogResult.Yes) {
            if (success != null) {
                success?.Invoke(dialog);
            }
        } else {
            if (error != null) {
                error?.Invoke(dialog);
            }
        }
        dialog.Dispose();
    }
}
