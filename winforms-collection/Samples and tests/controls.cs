using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samples_and_tests {
    public partial class controls : Form {
        public controls() {
            InitializeComponent();
                
            simpleList1.onAdd( new Func<string>( () => { return "swag"; } ) );
            simpleList1.onEdit( new Func<string, string>( ( string str ) => { return str + "- yolo"; } ) );
            simpleList1.onRemove<string>( new Action<string>( ( string st ) => { MessageBox.Show( "Test of " + st ); } ) );
        }
        
    }
}
