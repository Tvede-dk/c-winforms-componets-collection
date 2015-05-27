using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_organizer.interfaces {
    interface InstallRepo {

        List<Installable> getProducts();

        String getProductDownloadUrl(Installable ins);

        String getProductImageUrl( Installable ins );

        bool needBrowserSupport(Installable ins);  

    }
}
