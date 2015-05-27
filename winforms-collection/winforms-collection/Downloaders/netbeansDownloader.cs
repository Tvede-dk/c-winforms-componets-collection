using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows_organizer.interfaces;

namespace Windows_organizer.Downloaders {
    class netbeansDownloader : InstallRepo {

        private static readonly Installable netbeans = new Installable( "NetBeans IDE lets you quickly and easily develop Java desktop, mobile, and web applications, as well as HTML5 applications with HTML, JavaScript, and CSS. The IDE also provides a great set of tools for PHP and C/C++ developers. It is free and open source and has a large community of users and developers around the world." , "netbeans" , 
            new List<string>() , false );
        private static readonly List<Installable> products = new List<Installable> { netbeans };

        public List<Installable> getProducts() {
            return products;
        }

        public string getProductDownloadUrl( Installable ins ) {
            if ( Equals( ins , netbeans ) ) {
                return "https://netbeans.org/downloads/start.html?platform=windows&lang=en&option=all";
            }
            return "";
        }

        public string getProductImageUrl( Installable ins ) {
            if ( Equals( ins , netbeans ) ) {
                return "https://netbeans.org/images_www/v7/design/logo_netbeans_red.png";
            }
            return "";
        }

        public bool needBrowserSupport(Installable ins)
        {
            return false;
        }
    }
}
