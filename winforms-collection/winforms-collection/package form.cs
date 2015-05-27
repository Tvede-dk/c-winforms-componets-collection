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
using Windows_organizer.Models;

namespace Windows_organizer {
    public partial class package_form : Form {
        private bool showAll = true;
        private List<Installable> products = new List<Installable>();
        private readonly GlobalDownloadProvider gd = GlobalDownloadProvider.getInstance();

        public package_form( IEnumerable<Installable> toList ) {
            InitializeComponent();
            displayProducts( toList );
        }

        public package_form() {
            InitializeComponent();

            if ( showAll ) {
                fetchAll();
            } else {
                //TODO make me.
            }


        }

        private void displayProducts( IEnumerable<Installable> inss ) {
            var imgList = new ImageList();
            imgList.ColorDepth = ColorDepth.Depth24Bit;
            imgList.ImageSize = new Size( 64 , 64 );
            var index = 0;
            foreach ( var prod in inss ) {
                var item = new ListViewItem( prod.ProductName ) { Checked = false };
                if ( prod.imageUrl != null ) {
                    imgList.Images.Add( LoadImage( prod.imageUrl ) );
                }
                item.ImageIndex = index;
                this.listView1.Items.Add( item );
                index++;
                products.Add( prod );
            }
            this.listView1.SmallImageList = imgList;
            this.listView1.LargeImageList = imgList;
        }

        private void fetchAll() {
          //  displayProducts( gd.getProducts() );
        }

        private void button1_Click( object sender , EventArgs e ) {
            var listToInstall = new List<Installable>();
            foreach ( ListViewItem item in listView1.CheckedItems ) {
                listToInstall.Add( products.ElementAt( item.Index ) );
            }
            new installer( listToInstall ).Show();
            Hide();

        }

        private static Image LoadImage( string url ) {
            try {
                var request = System.Net.WebRequest.Create( url );
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                if ( responseStream == null ) return null;
                var bmp = new Bitmap( responseStream );
                responseStream.Dispose();
                return bmp;
            } catch ( Exception e ) {
                return null;
            }

        }
    }
}
