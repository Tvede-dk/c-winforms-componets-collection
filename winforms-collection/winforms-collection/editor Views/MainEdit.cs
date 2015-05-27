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

namespace Windows_organizer.Views {
    public partial class MainEdit : Form {

        private readonly SimpleHtmlDownloader data = new SimpleHtmlDownloader();

        public MainEdit() {
            InitializeComponent();

        }

        private void button1_Click( object sender , EventArgs e ) {
            var editor = new EditApp();
            var dr = editor.ShowDialog();
            if ( dr == DialogResult.OK ) {
                data.addInstallable( editor.getInstallable() );
                displayData();
            }

        }

        private void button2_Click( object sender , EventArgs e ) {
            if ( listBox1.SelectedIndex != -1 ) {
                this.data.RemoveAtIndex(listBox1.SelectedIndex);
                displayData();
            }
        }


        private void button3_Click( object sender , EventArgs e ) {
            var result = saveFileDialog1.ShowDialog();
            if ( result == DialogResult.OK ) {
                data.saveToFile( saveFileDialog1.FileName );
            }
        }

        private void button4_Click( object sender , EventArgs e ) {

            var result = openFileDialog1.ShowDialog();
            if ( result == DialogResult.OK ) {
                data.loadFromFile( openFileDialog1.FileName );
                displayData();
            }
        }

        private void displayData() {
            listBox1.Items.Clear();
            foreach (var prog in data.getProducts())
            {
                listBox1.Items.Add(prog.ProductName);
            }
        }

        private void button5_Click( object sender , EventArgs e )
        {
            var lst = data.getProducts();
            var newLst = new List<Models.Installable>();

            //TODO remove this, it is only tempoary conversion
            foreach ( var item in lst ) {
                newLst.Add( new Models.Installable { downloadHtmlUrl = item.downloadHtmlUrl, Description = item.Description, imageUrl = item.imageUrl, requireBrowser =true, SilentSwitchCommandLine = item.SilentSwitchCommandLine } );
            }

            var installer = new package_form( newLst );
            installer.Show();
        }

        private void button6_Click( object sender , EventArgs e ) {
            if ( listBox1.SelectedIndex != -1 ) {
                var editor = new EditApp(data.getProductAt(listBox1.SelectedIndex));
                var dr = editor.ShowDialog();
                if ( dr == DialogResult.OK ) {
                    data.addInstallable( editor.getInstallable() );
                    displayData();
                }


                displayData();
            }
        }
    }
}
