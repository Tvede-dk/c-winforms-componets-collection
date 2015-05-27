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

namespace Windows_organizer.Views {
    public partial class EditApp : Form {
        private Installable result;

        public EditApp() {
            InitializeComponent();
        }

        public EditApp( Installable ins ) {
            InitializeComponent();
            textBox3.Text = ins.ProductName;
            textBox4.Text = ins.Description;
            textBox7.Text = ins.SilentSwitchCommandLine;
            textBox5.Text = ins.downloadHtmlUrl;
            textBox6.Text = ins.imageUrl;
            checkBox1.Checked = ins.requireBrowser;
            if ( ins.dependencies != null ) {
                foreach ( var depend in ins.dependencies ) {

                }
            }
            if ( ins.Categories != null ) {
                foreach ( var cat in ins.Categories ) {

                }
            }
        }

        private void button3_Click( object sender , EventArgs e ) {
            var categorylist = new List<String>();
            foreach ( String str in checkedListBox1.CheckedItems ) {
                categorylist.Add( str );
            }
            result = new Installable( textBox4.Text , textBox3.Text , categorylist , false , textBox5.Text , textBox6.Text , !checkBox1.Checked , textBox7.Text );
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Dispose();
        }

        private void button4_Click( object sender , EventArgs e ) {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Dispose();
        }

        public Installable getInstallable() {
            return result;
        }

        private void textBox6_Leave( object sender , EventArgs e ) {
            pictureBox1.ImageLocation = textBox6.Text;
        }

        private void button7_Click( object sender , EventArgs e ) {
            var isFolder = MessageBox.Show( "Is it a folder  ? " , "Settings type" , MessageBoxButtons.YesNo );
            if ( isFolder == DialogResult.Yes ) {

            } else {

            }
        }
    }
}
