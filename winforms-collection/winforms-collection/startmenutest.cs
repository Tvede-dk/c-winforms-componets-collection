using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows_organizer.interfaces;

namespace Windows_organizer {
    public partial class StartmenuTest : Form {
        public StartmenuTest() {
            InitializeComponent();
        }

        private void textBox1_KeyDown( object sender , KeyEventArgs e ) {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1 != null && textBox1.Text == "wamp")
                {
                    var dialogResult = MessageBox.Show( "The program \"wamp\" is not installed,  but located in a repository,would you like to download and install it ?" , "Not installed program" , MessageBoxButtons.YesNo );
                    if ( dialogResult == DialogResult.Yes )
                    {
                        var lst = new List<Installable>();
                        lst.Add(new Installable("","wampserver", new List<string>(),false ));
                     //   new installer(lst).Show();
                    }
                }
            }
        }
    }
}
