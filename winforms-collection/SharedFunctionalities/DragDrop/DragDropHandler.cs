﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedFunctionalities.DragDrop {
    /// <summary>
    /// Currently a filebased one.
    /// </summary>
    public class DragDropHandler {
        public bool GetFilename(out string filename, DragEventArgs e, Func<string, bool> validator) {
            bool ret = false;
            filename = String.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy) {
                Array data = ((IDataObject)e.Data).GetData("FileName") as Array;
                if (data != null) {
                    if ((data.Length == 1) && (data.GetValue(0) is String)) {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
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
        public Func<string, bool> validator { get; set; }
        /// <summary>
        /// string is the filename
        /// </summary>
        public Action<string> onDropValid { get; set; }

        public void registerToView(Control control) {
            control.DragEnter += Control_DragEnter;
            control.DragDrop += Control_DragDrop;
        }

        private void Control_DragDrop(object sender, DragEventArgs e) {
            string file;
            bool valid = GetFilename(out file, e, validator);
            if (valid) {
                onDropValid(file);
            }
        }

        private void Control_DragEnter(object sender, DragEventArgs e) {
            string file;
            bool valid = GetFilename(out file, e, validator);
            if (valid) {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}