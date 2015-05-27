using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CefSharp.WinForms;
using CefSharp;

namespace Windows_organizer.Downloaders {
    class SimpleHTMLExtractor {



        public delegate void OnDownloadUrl( Uri url );

        //   public event OnDownloadUrl onDownloadUrl;

        private readonly SemaphoreSlim signal = new SemaphoreSlim( 0, 1 );

        private Uri uriResult;

        public async Task<Uri> DownloadAndWait( String uri ) {
            uriResult = null;
           
            ChromiumWebBrowser browser = new ChromiumWebBrowser( "");
            browser.CreateControl();
            browser.Hide();
            var dlHandler = new DownloadHandler( signal );
            browser.DownloadHandler = dlHandler;
            browser.Load( uri );
            await signal.WaitAsync();
            
            uriResult = new Uri( dlHandler.url);

            return uriResult;
        }

        //private void WebCore_Download( object sender, DownloadEventArgs e ) {
        //    try {
        //        uriResult = e.Url;

        //    } finally {
        //        signal.Release();
        //    }

        //}
        private class DownloadHandler : IDownloadHandler {

            public string url;

            private SemaphoreSlim sem;
            public DownloadHandler( SemaphoreSlim sem ) {
                this.sem = sem;
            }

            #region IDownloadHandler Members

            public bool OnBeforeDownload( DownloadItem downloadItem, out string downloadPath, out bool showDialog ) {
                Console.WriteLine( "test" );
                downloadPath = "";
                showDialog = false;
                url = downloadItem.Url;
                sem.Release();
                return false; //cancel the download
            }

            public bool OnDownloadUpdated( DownloadItem downloadItem ) {
                return false;//just contiune....
            }


            #endregion
        }

    }


}
