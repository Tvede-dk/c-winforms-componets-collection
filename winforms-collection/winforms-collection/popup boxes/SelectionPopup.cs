using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace winforms_collection.popup_boxes {
    public partial class SelectionPopup<T> : Form {
        private readonly Func<T, string> _modelToString;


        #region property ResultSelection
        private HashSet<T> _resultSelection;


        public HashSet<T> ResultSelection {
            get { return _resultSelection; }
            set { _resultSelection = value; }
        }
        #endregion

        private readonly List<T> _realData;

        public SelectionPopup(List<T> data, Func<T, string> modelToString, bool multiSelect = false, bool showChecboxes = false) {
            InitializeComponent();
            ResultSelection = new HashSet<T>();
            simpleListControl1.MultiSelect = multiSelect;
            simpleListControl1.CheckBoxes = showChecboxes;
            this._realData = data;
            this._modelToString = modelToString;
            SetupList();
        }

        private void SetupList() {
            simpleListControl1.BeginUpdate();
            foreach (var item in _realData) {
                simpleListControl1.Items.Add(_modelToString?.Invoke(item));
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
                    var first = _realData[(int)item];
                    ResultSelection.Add(first);
                }
                if (simpleListControl1.CheckBoxes) {
                    foreach (var item in simpleListControl1.CheckedIndices) {
                        var first = _realData[(int)item];
                        ResultSelection.Add(first);
                    }
                }
            } else {
                var firstIndex = simpleListControl1.SelectedIndices[0];
                var first = _realData[firstIndex];
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
