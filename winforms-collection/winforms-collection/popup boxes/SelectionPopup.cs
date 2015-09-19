using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.popup_boxes {
    public partial class SelectionPopup<T> : Form {
        private readonly Func<T, string> modelToString;


        #region property ResultSelection
        private HashSet<T> _ResultSelection;


        public HashSet<T> ResultSelection {
            get { return _ResultSelection; }
            set { _ResultSelection = value; }
        }
        #endregion

        private readonly List<T> realData;

        public SelectionPopup(List<T> data, Func<T, string> modelToString, bool multiSelect = false, bool showChecboxes = false) {
            InitializeComponent();
            ResultSelection = new HashSet<T>();
            simpleListControl1.MultiSelect = multiSelect;
            simpleListControl1.CheckBoxes = showChecboxes;
            this.realData = data;
            this.modelToString = modelToString;
            setupList();
        }

        private void setupList() {
            simpleListControl1.BeginUpdate();
            foreach (var item in realData) {
                simpleListControl1.Items.Add(modelToString?.Invoke(item));
            }
            simpleListControl1.EndUpdate();
            simpleListControl1.Update();
        }

        private void styleableButton1_Click(object sender, EventArgs e) {

            if (simpleListControl1.SelectedItems.Count == 0 && simpleListControl1.CheckedIndices.Count == 0) {
                return;
            }

            if (simpleListControl1.MultiSelect) {
                foreach (var item in simpleListControl1.SelectedIndices) {
                    var first = realData[(int)item];
                    ResultSelection.Add(first);
                }
                if (simpleListControl1.CheckBoxes) {
                    foreach (var item in simpleListControl1.CheckedIndices) {
                        var first = realData[(int)item];
                        ResultSelection.Add(first);
                    }
                }
            } else {
                var firstIndex = simpleListControl1.SelectedIndices[0];
                var first = realData[firstIndex];
                ResultSelection.Add(first);
            }

            DialogResult = DialogResult.OK;
        }

        private void styleableButton2_Click(object sender, EventArgs e) {
            ResultSelection = null;
            DialogResult = DialogResult.No;
        }
    }
}
