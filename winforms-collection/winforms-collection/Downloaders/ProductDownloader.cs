using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows_organizer.Models;

namespace Windows_organizer.Downloaders {
    class ProductDownloader {

        #region events


        public delegate void DownloadCompleted( object sender , EventArgs e );

        public event DownloadCompleted downloadComplete;

        public delegate void DownloadFailed( object sender , int errorcode );

        public event DownloadFailed downloadFailed;


        public delegate void InstallationCompleted( object sender , EventArgs e );

        public event InstallationCompleted installationCompleted;



        #endregion

        private readonly SimpleHTMLExtractor simpleHtml = new SimpleHTMLExtractor();


        public void downloadProduct(Installable toInstall ) {
            try
            {
                var downloadUrl = toInstall.downloadHtmlUrl;
                if (toInstall.requireBrowser)
                {
                    DownloadThoughWebClient(downloadUrl, toInstall);
                }
                else
                {
                    DownloadSimple(downloadUrl, toInstall);
                }
            }
            catch (Exception e)
            {
                downloadFailed(this, 0);
            }
        }

        private async void DownloadThoughWebClient( String downloadUrl , Installable toInstall ) {
            var uri = await simpleHtml.DownloadAndWait( downloadUrl );
            DownloadSimple( uri.OriginalString , toInstall );
        }

        private async void DownloadSimple( String downloadUrl , Installable toInstall ) {
            try {
                using ( var client = new HttpClient() ) {
                    client.DefaultRequestHeaders.TryAddWithoutValidation( "User-Agent" , "Windows organizer/5.0 (Windows NT 6.3; Win64; x64) " );
                    client.BaseAddress = new Uri( downloadUrl );
                    var response = await client.GetAsync( "" );
                    if ( response.IsSuccessStatusCode ) {
                        writeFile( response , toInstall );
                    } else if ( downloadFailed != null ) {
                        downloadFailed( this , (int)response.StatusCode );
                    }
                }
            } catch ( Exception e ) {
                Console.WriteLine( "Exception thrown" );
            }
        }

        private async void writeFile( HttpResponseMessage response , Installable toInstall ) {
            var arr = await response.Content.ReadAsByteArrayAsync();
            File.WriteAllBytes( toInstall.ProductName + ".exe" , arr );
            if ( downloadComplete != null ) {
                var args = new EventArgs();
                downloadComplete( this , new EventArgs() );
            }
        }

        public void installProduct( Installable toInstall ) {
            var switchStr = "";
            if (!String.IsNullOrEmpty(toInstall.SilentSwitchCommandLine))
            {
                switchStr = toInstall.SilentSwitchCommandLine;
            }
            var installer = new Process
            {
                StartInfo = new ProcessStartInfo( toInstall.ProductName + ".exe" , switchStr ) { UseShellExecute = true }
            };
            installer.Start();
            installer.WaitForExit();
            if ( installationCompleted != null ) {
                installationCompleted( this , new EventArgs() );
            }
        }

        public void removeInstaller( Installable product ) {
           // File.Delete( product.ProductName + ".exe" );
        }
    }
}
