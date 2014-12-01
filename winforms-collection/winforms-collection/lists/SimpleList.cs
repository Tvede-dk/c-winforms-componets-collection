using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection {
    public partial class SimpleList : UserControl {



        #region property onAdd
        private Func<object> _onAdd;

         
        public Func<object> onAdd {
            get { return _onAdd; }
            set { _onAdd = value; }
        }
        #endregion


        #region property onDelete
        private Func<object> _onDelete;


        public Func<object> onDelete {
            get { return _onDelete; }
            set { _onDelete = value; }
        }
        #endregion


        #region property onEdit
        private Func<object> _onEdit;


        public Func<object> onEdit {
            get { return _onEdit; }
            set { _onEdit = value; }
        }
        #endregion

        #region property title
        private String _title;


        public String title {
            get { return _title; }
            set { _title = value; }
        }
        #endregion

        public SimpleList() {
            InitializeComponent();

        }
       
    }
}
