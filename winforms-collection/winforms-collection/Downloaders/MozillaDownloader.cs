using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Http;
using Windows_organizer.interfaces;
using System.Threading.Tasks;


namespace Windows_organizer.Downloaders {
    class MozillaDownloader : InstallRepo {

        private const String baseUrl = "https://download.mozilla.org/?product=";
        private const String endUrl = "&os=win&lang=en-US";

        #region InstallRepo Members

        public List<Installable> getProducts() {
            var products = new List<Installable>();
            var categegory = new List<string>();

            products.Add( new Installable( "A fast, moden webbrowser" ,"firefox",  categegory , false ) );
            products.Add( new Installable( "A fast, moden email client" , "thunderbird" ,  categegory ,  false ) );
            return products;
        }

        public string getProductDownloadUrl( Installable ins ) {
            var url = baseUrl;
            switch ( ins.ProductName.ToLower() ) {
                case "firefox":
                    url += "firefox-stub";
                    break;

                case "thunderbird":
                    url += "thunderbird-24.5.0"; //apprently no simple way .. ?? 
                    break;
            }
            url += endUrl;
            return url;
        }

        public string getProductImageUrl( Installable ins ) {
            var url = "";
            switch ( ins.ProductName.ToLower() ) {
                case "firefox":
                    url = "https://mozorg.cdn.mozilla.net/media/img/styleguide/identity/firefox/usage-logo.png?2013-06";
                    break;

                case "thunderbird":
                    url = "https://mozorg.cdn.mozilla.net/media/img/styleguide/identity/thunderbird/usage-logo.png?2013-06";
                    break;
            }
            return url;
        }

        public bool needBrowserSupport(Installable ins)
        {
            return false;
        }

        #endregion
    }
}
