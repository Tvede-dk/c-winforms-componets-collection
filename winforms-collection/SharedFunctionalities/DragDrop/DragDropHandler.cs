using System;
using System.IO;
using System.Windows.Forms;

namespace SharedFunctionalities.DragDrop {
    /// <summary>
    /// Currently a filebased one.
    /// </summary>
    public class DragDropHandler {
        public bool GetFilename(out string filename, DragEventArgs e, Func<string, bool> validator) {
            var ret = false;
            filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy) {
                var data = ((IDataObject)e.Data).GetData("FileNameW") as Array;
                if (data != null) {
                    if ((data.Length == 1) && (data.GetValue(0) is String)) {
                        filename = ((string[])data)[0];
                        var ext = Path.GetExtension(filename).ToLower();
                        if (validator(ext)) {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// param is the extension, returns true if valid.
        /// </summary>
        public Func<string, bool> Validator { get; set; }
        /// <summary>
        /// string is the filename
        /// </summary>
        public Action<string> OnDropValid { get; set; }

        public void RegisterToView(Control control) {
            control.DragEnter += Control_DragEnter;
            control.DragDrop += Control_DragDrop;
        }

        private void Control_DragDrop(object sender, DragEventArgs e) {
            string file;
            var valid = GetFilename(out file, e, Validator);
            if (valid) {
                OnDropValid(file);
            }
        }

        private void Control_DragEnter(object sender, DragEventArgs e) {
            string file;
            var valid = GetFilename(out file, e, Validator);
            if (valid) {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
