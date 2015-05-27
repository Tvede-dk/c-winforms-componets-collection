using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows_organizer.interfaces;

namespace Windows_organizer.Downloaders {
    class GoogleDownlaoder : InstallRepo {
        public List<Installable> getProducts() {
            var lst = new List<Installable> { new Installable( "Google Chrome er en browser, som kombinerer et minimalt design med avanceret teknologi for at gøre det hurtigere, nemmere og sikrere at bruge nettet." , "chrome" , new List<string>() , false ) };
            return lst;
        }

        public string getProductDownloadUrl( Installable ins ) {
            return "https://www.google.com/intl/en/chrome/browser/thankyou.html";
        }

        public string getProductImageUrl( Installable ins ) {
            return "http://www.google.dk/intl/en/chrome/assets/common/images/chrome_logo_2x.png";
        }

        public bool needBrowserSupport(Installable ins)
        {
            return true;
        }

    }
}
