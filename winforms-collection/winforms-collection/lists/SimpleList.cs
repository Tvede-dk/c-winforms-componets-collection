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

        private ListViewItem selectedObject;


        private List<object> data = new List<object>();

        #region event handlers


        public void onAdd<T>( Func<T> onAddHandler ) {
            onAddCallback = new Func<object>( () => { return (object)onAddHandler(); } );//wrap it around.
        }

        private Func<object> onAddCallback = null;

        public void onEdit<T>( Func<T, T> onEditHandler ) {
            onEditCallback = new Func<object, object>( ( object obj ) => { return (object)onEditHandler( (T)obj ); } );//wrap it around.
        }

        private Func<object, object> onEditCallback = null;

        public void onRemove<T>( Action<T> onRemoveHandler ) {
            this.onRemoveHandler = new Action<object>( ( object obj ) => { onRemoveHandler( (T)obj ); } );
        }

        private Action<object> onRemoveHandler = null;

        #endregion


        public void onRender<T>( Func<T, ListViewItem> onRender ) {
            onRenderCallback = new Func<object, ListViewItem>( ( object obj ) => { return onRender( (T)obj ); } );//wrap it around.
        }

        private Func<object, ListViewItem> onRenderCallback = null;

        #region property title
        private String _title;


        public String title {
            get { return _title; }
            set {
                _title = value;
                label1.Text = value;
            }
        }
        #endregion

        public SimpleList() {
            onRenderCallback = new Func<object, ListViewItem>( ( object obj ) => { return new ListViewItem( obj.ToString() ); } );
            InitializeComponent();

        }

        private void simpleListControl1_SelectedIndexChanged( object sender, EventArgs e ) {
            if ( simpleListControl1.SelectedItems.Count == 1 ) {
                selectedObject = simpleListControl1.SelectedItems[0];
            } else {
                selectedObject = null;
            }
        }

        private void button3_Click( object sender, EventArgs e ) {
            if ( onRemoveHandler != null && selectedObject != null ) {
                int index = simpleListControl1.SelectedIndices[0];
                onRemoveHandler( data[index] );
                data.RemoveAt( index );
                simpleListControl1.Items.RemoveAt( index );
            }
        }

        private void button4_Click( object sender, EventArgs e ) {
            if ( onEditCallback != null && selectedObject != null ) {
                int index = simpleListControl1.SelectedIndices[0];
                var result = onEditCallback( data[index] );
                if ( result != null ) {
                    data[index] = result;
                    //simpleListControl1.Items.RemoveAt( index );
                    //simpleListControl1.Items.Insert( index, onRenderCallback( result ) );
                    simpleListControl1.Items[index] = onRenderCallback( result );
                }
            }
        }

        private void button1_Click( object sender, EventArgs e ) {
            if ( onAddCallback != null ) {
                var result = onAddCallback();
                if ( result != null ) {
                    simpleListControl1.Items.Add( onRenderCallback( result ) );
                    data.Add( result );
                }
            }
        }
    }
}
