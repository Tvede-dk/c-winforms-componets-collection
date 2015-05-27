using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Windows_organizer.interfaces;


namespace Windows_organizer  {
    class SourceForgeDownloader : InstallRepo {

        private String projectName = "";

        private const String baseUrl = "http://sourceforge.net/projects/";
        private const String downloadLinkPart = "/files/latest/download?nowrap";

        private const String iconLocationStart = "http://sourceforge.net/p/";
        private const String iconLocationEnd = "/icon";


        public String getProductImage() {
            return iconLocationStart + projectName + iconLocationEnd;
        }


        public async Task<HttpContent> getDownloadLink() {
            HttpContent content = null;
            try {
                using ( var client = new HttpClient() ) {
                    client.DefaultRequestHeaders.TryAddWithoutValidation( "User-Agent" , "downloader/5.0 (Windows NT 6.3; Win64; x64) " );
                    client.BaseAddress = new Uri( baseUrl );
                    HttpResponseMessage response = await client.GetAsync( projectName + downloadLinkPart );
                    if ( response.IsSuccessStatusCode ) {
                        content = response.Content;
                    } else {
                        // System.Windows.Forms.MessageBox.Show( "Failed to download" );
                        Console.WriteLine( "failed to download" );
                    }
                }

            } catch ( Exception e ) {
                Console.WriteLine( "Exception thrown" );
                return null;
            }
            return content;
        }


        public void setProductName( string p ) {
            this.projectName = p;
        }

        public List<Installable> getProducts()
        {
            return null;
            
        }

        public string getProductDownloadUrl(Installable ins)
        {
            return "";
        }

        public string getProductImageUrl(Installable ins)
        {
            return "";
        }

        public bool needBrowserSupport(Installable ins)
        {
            return true;
        }
    }
}
