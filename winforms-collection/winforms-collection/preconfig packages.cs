using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_organizer {
    public partial class preconfig_packages : Form {
        public preconfig_packages() {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged( object sender , EventArgs e ) {
            new package_form().Show();
            Hide();
        }
    }
}
