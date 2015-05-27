using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows_organizer.interfaces;

namespace Windows_organizer.Downloaders {
    /// <summary>
    /// A single instance of all the various download managers 
    /// (composite pattern and singleton).
    /// </summary>
    class GlobalDownloadProvider : Windows_organizer.interfaces.InstallRepo {

        #region singleton pattern

        private static GlobalDownloadProvider instance;



        private GlobalDownloadProvider() {
            mozillaManager = new MozillaDownloader();
            nbdownloader = new netbeansDownloader();
            ChromeDownloader = new GoogleDownlaoder();
            //TODO make it for the sourceforgemanager
        }

        public static GlobalDownloadProvider getInstance() {
            if ( instance == null ) {
                instance = new GlobalDownloadProvider();
            }
            return instance;
        }

        #endregion


        public netbeansDownloader nbdownloader { get; private set; }
        public MozillaDownloader mozillaManager { get; private set; }
        public MozillaDownloader sourceforgeManager { get; private set; }
        public GoogleDownlaoder ChromeDownloader { get; private set; }


        public List<Installable> getProducts() {
            var res = mozillaManager.getProducts();
            res.AddRange( ChromeDownloader.getProducts() );
            res.AddRange( nbdownloader.getProducts() );
            return res;
        }

        public string getProductDownloadUrl( Installable ins ) {
            if ( mozillaManager.getProducts().Contains( ins ) ) {
                return mozillaManager.getProductDownloadUrl( ins );
            } else if ( nbdownloader.getProducts().Contains( ins ) ) {
                return nbdownloader.getProductDownloadUrl( ins );
            } else if ( ChromeDownloader.getProducts().Contains( ins ) ) {
                return ChromeDownloader.getProductDownloadUrl( ins );
            }
            return "";
        }

        public string getProductImageUrl( Installable ins ) {
            if ( mozillaManager.getProducts().Contains( ins ) ) {
                return mozillaManager.getProductImageUrl( ins );
            } else if ( nbdownloader.getProducts().Contains( ins ) ) {
                return nbdownloader.getProductImageUrl( ins );
            } else if ( ChromeDownloader.getProducts().Contains( ins ) ) {
                return ChromeDownloader.getProductImageUrl( ins );
            }
            return "";
        }

        public bool needBrowserSupport( Installable ins )
        {
            if ( mozillaManager.getProducts().Contains( ins ) ) {
                return mozillaManager.needBrowserSupport( ins );
            } else if ( nbdownloader.getProducts().Contains( ins ) ) {
                return nbdownloader.needBrowserSupport( ins );
            } else if ( ChromeDownloader.getProducts().Contains( ins ) ) {
                return ChromeDownloader.needBrowserSupport( ins );
            }
            return false;
        }
    }
}
