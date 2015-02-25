using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection
{
    public partial class SimpleList : UserControl
    {

        private ListViewItem selectedObject;


        private List<object> data = new List<object>();

        #region event handlers


        public void addData<T>(T dataObj)
        {
            data.Add(dataObj);
            if (onRenderCallback != null)
            {
                ListViewItem lvi = onRenderCallback((T)dataObj);
                simpleListControl1.Items.Add(lvi);
            }

        }

        public void removeAt(int index)
        {
            simpleListControl1.Items.RemoveAt(index);
            data.RemoveAt(index);
        }

        public void callEdit<T>(T obj, int v)
        {
            callEdit((object)obj, v);
        }
        private void callEdit(object obj, int v)
        {
            if (onEditCallback != null)
            {
                onEditCallback(obj, v);
            }
        }


        public void editObject(object newObj, int index)
        {
            bool addedInstead = false;
            if (data.Count == 0 || index < 0 || index > data.Count)
            {
                data.Add(newObj);
                addedInstead = true;
                index = data.Count - 1;
            }
            else
            {
                data[index] = newObj;
            }

            if (onRenderCallback != null)
            {
                ListViewItem lvi = onRenderCallback(newObj);
                if (addedInstead)
                {
                    simpleListControl1.Items.Insert(index, lvi);
                }
                else
                {
                    simpleListControl1.Items[index] = lvi;
                }

            }

        }

        public void setHandlers<T>(Action onAddHandler, Action<T, int> onEditHandler, Action<T, int> onRemoveHandler, Func<T, ListViewItem> renderFunction)
        {
            onAdd(onAddHandler);
            onEdit(onEditHandler);
            onRemove(onRemoveHandler);
            onRender(renderFunction);
        }

        private void onAdd(Action onAddHandler)
        {
            onAddCallback = onAddHandler;
        }

        private Action onAddCallback = null;

        public void onEdit<T>(Action<T, int> onEditHandler)
        {
            onEditCallback = ((object parm, int val) => { onEditHandler((T)parm, val); });
        }

        private Action<object, int> onEditCallback = null;

        public void onRemove<T>(Action<T, int> onRemoveHandler)
        {
            this.onRemoveHandler = ((object obj, int val) => { onRemoveHandler((T)obj, val); });
        }

        private Action<object, int> onRemoveHandler = null;

        #endregion


        public void onRender<T>(Func<T, ListViewItem> onRender)
        {
            onRenderCallback = new Func<object, ListViewItem>((object obj) => { return onRender((T)obj); });//wrap it around.
        }

        private Func<object, ListViewItem> onRenderCallback = (object obj) =>
        {
            return new ListViewItem(obj.ToString());
        };

        #region property title
        private String _title;


        public String title
        {
            get { return _title; }
            set
            {
                _title = value;
                label1.Text = value;
            }
        }
        #endregion

        public SimpleList()
        {
            onAdd(() => { callEdit(null, -1); });
            onRemove((object obj, int index) => { removeAt(index); });
            InitializeComponent();
        }

        [EditorBrowsable]
        [DefaultValue(View.List)]
        [Description("The way the listview gets rendered")]
        public View View
        {
            get
            {
                return simpleListControl1.View;
            }
            set
            {
                simpleListControl1.View = value;
            }
        }

        private void simpleListControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (simpleListControl1.SelectedItems.Count == 1)
            {
                selectedObject = simpleListControl1.SelectedItems[0];
            }
            else
            {
                selectedObject = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (onRemoveHandler != null && selectedObject != null)
            {
                int index = simpleListControl1.SelectedIndices[0];
                onRemoveHandler(data[index], index);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (onEditCallback != null && selectedObject != null)
            {
                int index = simpleListControl1.SelectedIndices[0];
                onEditCallback(data[index], index);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (onAddCallback != null)
            {
                onAddCallback();
            }
        }
    }
}
