using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonSenseCSharp.datastructures;

namespace winforms_collection {
    public partial class SimpleList : UserControl {
        private ListViewItem _selectedObject;

        private readonly List<object> _data = new List<object>();

        #region event handlers

        private Object _lastAdded;

        public void SetData<T>(IEnumerable<T> dataLst) {
            Clear();
            AddDataCollection(dataLst);
        }

        public void AddData<T>(T dataObj) {
            _data.Add(dataObj);
            _lastAdded = dataObj;
            if (_onRenderCallback != null) {
                var lvi = _onRenderCallback?.Invoke((T)dataObj);
                simpleListControl1.Items.Add(lvi);
            }

        }

        public void AddDataCollection<T>(IEnumerable<T> dataLst) {
            foreach (var item in dataLst) {
                AddData(item);
            }
        }


        public void RemoveAt(int index) {
            simpleListControl1.Items.RemoveAt(index);
            _data.RemoveAt(index);
        }

        public void CallEdit<T>(T obj, int v) {
            CallEdit((object)obj, v);
        }
        private void CallEdit(object obj, int v) {
            if (_onEditCallback != null) {
                _onEditCallback?.Invoke(obj, v);
            }
        }

        public NonNullList<T> GetData<T>() {
            return new NonNullList<T>(_data.Cast<T>());
        }

        private Action<object, int> _onSelectionChangedCallback;

        public void OnSelection<T>(Action<T, int> callback) {
            _onSelectionChangedCallback = (object obj, int index) => { callback?.Invoke((T)obj, index); };
            simpleListControl1.ItemSelectionChanged += OnChanged;

        }

        private void OnChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e) {
            if (_onSelectionChangedCallback != null) {
                _onSelectionChangedCallback?.Invoke(_data[e.ItemIndex], e.ItemIndex);
            }

        }


        public void EditObject(object newObj, int index) {
            var addedInstead = false;
            if (_data.Count == 0 || index < 0 || index > _data.Count) {
                _data.Add(newObj);
                _lastAdded = newObj;
                addedInstead = true;
                index = _data.Count - 1;
            } else {
                _data[index] = newObj;
            }

            if (_onRenderCallback != null && newObj != null) {
                var lvi = _onRenderCallback?.Invoke(newObj);
                if (addedInstead) {
                    simpleListControl1.Items.Insert(index, lvi);
                } else {
                    simpleListControl1.Items[index] = lvi;
                }

            }

        }

        public void setHandlers<T>(Action onAddHandler, Action<T, int> onEditHandler, Action<T, int> removeHandler, Func<T, ListViewItem> renderFunction) {
            OnAdd(onAddHandler);
            OnEdit(onEditHandler);
            OnRemove(removeHandler);
            OnRender(renderFunction);
        }

        private void OnAdd(Action onAddHandler) {
            _onAddCallback = onAddHandler;
        }

        public void OnAddClick() {
            _onAddCallback?.Invoke();
        }

        private Action _onAddCallback = null;

        public void OnEdit<T>(Action<T, int> onEditHandler) {
            _onEditCallback = ((object parm, int val) => { onEditHandler?.Invoke((T)parm, val); });
        }

        private Action<object, int> _onEditCallback = null;

        public void OnRemove<T>(Action<T, int> onRemoveHandler) {
            this._onRemoveHandler = ((object obj, int val) => { onRemoveHandler?.Invoke((T)obj, val); });
        }

        private Action<object, int> _onRemoveHandler = null;

        #endregion


        public void OnRender<T>(Func<T, ListViewItem> onRender) {
            _onRenderCallback = new Func<object, ListViewItem>((object obj) => { return onRender?.Invoke((T)obj); });//wrap it around.
        }

        private Func<object, ListViewItem> _onRenderCallback = (object obj) =>
        {
            return new ListViewItem(obj.ToString());
        };


        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            if (Width < 280) { //approx 
                button1.Text = "";
                button1.ImageAlign = ContentAlignment.MiddleCenter;
                button3.Text = "";
                button3.ImageAlign = ContentAlignment.MiddleCenter;
                button4.Text = "";
                button4.ImageAlign = ContentAlignment.MiddleCenter;
            } else {
                button1.Text = "Add";
                button1.ImageAlign = ContentAlignment.MiddleLeft;
                button3.Text = "Remove";
                button3.ImageAlign = ContentAlignment.MiddleLeft;
                button4.Text = "Edit";
                button4.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        #region property title
        private String _title;


        public String Title {
            get { return _title; }
            set {
                _title = value;
                label1.Text = value;
            }
        }
        #endregion

        public SimpleList() {
            OnAdd(() => { CallEdit(null, -1); });
            OnRemove((object obj, int index) => { RemoveAt(index); });
            InitializeComponent();
        }

        /// <summary>
        /// Clears the data in this list. 
        /// </summary>
        public void Clear() {
            _data.Clear();
            _selectedObject = null;
            simpleListControl1.Clear();
        }

        [EditorBrowsable]
        [DefaultValue(View.List)]
        [Description("The way the listview gets rendered")]
        public View View {
            get {
                return simpleListControl1.View;
            }
            set {
                simpleListControl1.View = value;
            }
        }

        #region events / click handlers internal 
        private void simpleListControl1_SelectedIndexChanged(object sender, EventArgs e) {
            if (simpleListControl1.SelectedItems.Count == 1) {
                _selectedObject = simpleListControl1.SelectedItems[0];
            } else {
                _selectedObject = null;
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (_onRemoveHandler != null && _selectedObject != null) {
                var index = simpleListControl1.SelectedIndices[0];
                _onRemoveHandler?.Invoke(_data[index], index);

            }
        }

        private void button4_Click(object sender, EventArgs e) {
            if (_onEditCallback != null && _selectedObject != null) {
                var index = simpleListControl1.SelectedIndices[0];
                _onEditCallback?.Invoke(_data[index], index);
            }
        }



        private void button1_Click(object sender, EventArgs e) {
            if (_onAddCallback != null) {
                _onAddCallback?.Invoke();
            }
        }
        #endregion


        public T GetLastAdded<T>() {
            return (T)_lastAdded;
        }

        private void simpleListControl1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Delete && simpleListControl1.SelectedItems.Count == 1) {
                var items = simpleListControl1.SelectedIndices;
                _onRemoveHandler?.Invoke(_data[items[0]], items[0]);
            }

        }

        private void simpleListControl1_DoubleClick(object sender, EventArgs e) {
            if (simpleListControl1.SelectedItems.Count == 1) {
                var items = simpleListControl1.SelectedIndices;
                _onEditCallback?.Invoke(_data[items[0]], items[0]);
            }
        }
    }
}
