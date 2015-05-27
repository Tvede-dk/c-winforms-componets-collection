using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Windows_organizer.Downloaders;
using System.IO;
using Windows_organizer.Models;

namespace Windows_organizer {
    public partial class installer : Form {
        private SourceForgeDownloader sfd;
        private bool mayInstall = true;

        private int index = 0;
        private readonly ProductDownloader pd;

        private GlobalDownloadProvider gd;
        private readonly List<Installable> listToInstall;



        public installer( List<Installable> listToInstall ) {
            InitializeComponent();
            gd = GlobalDownloadProvider.getInstance();
            pd = new ProductDownloader();
            this.listToInstall = listToInstall;
            progressBar1.Maximum = listToInstall.Count;
            progressBar2.Maximum = 2;
            UpdateProgramsLeft(0);
        }

        private void UpdateProgramsLeft( int programsDone ) {
            label7.Text = programsDone + " / " + listToInstall.Count + " programs";
        }

        protected override void OnLoad( EventArgs e ) {
            base.OnLoad( e );
            if ( listToInstall != null && listToInstall.Count > 0 ) {
                pd.downloadComplete += pd_downloadComplete;
                pd.installationCompleted += pd_installationCompleted;
                displayProduct(  listToInstall.First() );
                pd.downloadProduct(listToInstall.First() );
            }
        }


        void pd_downloadComplete( object sender , EventArgs e ) {
            pd.installProduct( listToInstall.ElementAt( index ) );
            progressBar2.Value++;
        }

        void pd_installationCompleted( object sender , EventArgs e ) {
            pd.removeInstaller( listToInstall.ElementAt( index ) );
            progressBar1.Value++;
            UpdateProgramsLeft(progressBar1.Value);
            progressBar2.Value = 0;
            index += 1;
            if ( index < listToInstall.Count ) {
                displayProduct(  listToInstall.ElementAt( index ) );
                pd.downloadProduct( listToInstall.ElementAt( index ) );
            } else {
                progressBar2.Value = 0;
                MessageBox.Show( "All installlations are done" );
            }

        }

        private void displayProduct( Installable ins ) {
            try
            {
                displayImage(ins.imageUrl);
                label4.Text = ins.ProductName;
            }
            catch (Exception e)
            {
                
            }
        }
        private void displayImage( String s ) {
            pictureBox1.ImageLocation = s;
        }


        private void comboBox1_SelectedIndexChanged( object sender , EventArgs e ) {

        }

        private void installer_FormClosing( object sender , FormClosingEventArgs e ) {
            Application.Exit();
        }

        private void button1_Click( object sender , EventArgs e ) {
            mayInstall = false;
            label6.Text = "Cancelling";
        }
    }
}
