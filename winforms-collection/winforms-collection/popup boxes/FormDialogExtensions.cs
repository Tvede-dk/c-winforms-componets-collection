using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class FormDialogExtensionss
{

    public static void ShowDialog<T>(this T form, Action<T> onSucess, Action<T> onFailed) where T : Form
    {
        var result = form.ShowDialog();
        if (result == DialogResult.OK || result == DialogResult.Yes)
        {
            onSucess(form);
        }
        else
        {
            onFailed(form);
        }
    }

    public static void ShowDialog<T>(this T form, Form parrent, Action<T> onSucess, Action<T> onFailed) where T : Form
    {
        var result = form.ShowDialog(parrent);
        if (result == DialogResult.OK || result == DialogResult.Yes)
        {
            onSucess(form);
        }
        else
        {
            onFailed(form);
        }
    }

    public static void ShowDialog<T>(this T form, Form parrent, Action<T> onSucess) where T : Form
    {
        var result = form.ShowDialog(parrent);
        if (result == DialogResult.OK || result == DialogResult.Yes)
        {
            onSucess(form);
        }
    }
    public static void ShowDialog<T>(this T form, Action<T> onSucess) where T : Form
    {
        var result = form.ShowDialog();
        if (result == DialogResult.OK || result == DialogResult.Yes)
        {
            onSucess(form);
        }
    }
}
