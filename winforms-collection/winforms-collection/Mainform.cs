using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows_organizer.Downloaders;
using Windows_organizer.interfaces;
using Windows_organizer.server;
using Windows_organizer.Views;

namespace Windows_organizer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click( object sender , EventArgs e ) {
            Form f = new preconfig_packages();
            f.Show();
            Hide();
        }

        private void button4_Click( object sender , EventArgs e ) {
            new StartmenuTest().Show();
            Hide();
        }

        private void button3_Click( object sender , EventArgs e ) {
            /*var g = new GoogleDownlaoder();
            new package_form(g.getProducts()).Show();
            Hide();*/
            
        }

        private void button5_Click( object sender , EventArgs e ) {
            var me = new MainEdit();
            me.Show();
            
        }

        private void button6_Click( object sender , EventArgs e )
        {
            var list =apiconnector.GetInstallables();
            new package_form(list).Show();
        }
    }
}