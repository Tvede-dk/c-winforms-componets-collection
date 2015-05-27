using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Windows_organizer.Models;
using Newtonsoft.Json;

namespace Windows_organizer.server {
     public  class apiconnector {


         public static IEnumerable<Models.Installable> GetInstallables()
         {
             var url = "http://84.238.65.151/winorg/api/getInstallable.php";
             var req = WebRequest.Create(url);
             var webres = req.GetResponse();
             var stream = webres.GetResponseStream();
             var reader = new StreamReader( stream );
             var text = reader.ReadToEnd();
             var installerJson = baseRequestResonse < Models.Installable>.parseFromJson( text ).data;
             return installerJson;
         }

    }

    class baseRequestResonse<T>
    {
        public bool sucess { get; set; }
        public T[] data { get; set; }

        public static baseRequestResonse<T> parseFromJson( String json ) {
            return JsonConvert.DeserializeObject<baseRequestResonse<T>>( json );
        }

        public static List<baseRequestResonse<T>> parseFromJsonToList( String json ) {
            return JsonConvert.DeserializeObject<List<baseRequestResonse<T>>>( json );
        }
    }

}
