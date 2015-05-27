using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows_organizer.server;
using Newtonsoft.Json;
namespace Windows_organizer.Models {


    public class Installable {
        public int id { get; set; }
        public String ProductName { get; set; }
        public String Vendor { get; set; }
        public String SilentSwitchCommandLine { get; set; }
        public String downloadHtmlUrl { get; set; }
        public String imageUrl { get; set; }
        [JsonConverter( typeof( BoolConverter ) )]
        public bool requireBrowser { get; set; }
        public String Description { get; set; }
        [JsonConverter( typeof( BoolConverter ) )]
        public bool SupportSettingSync { get; set; }
        [JsonConverter( typeof( BoolConverter ) )]
        public bool is64Bit { get; set; }
        [JsonConverter( typeof( BoolConverter ) )]
        public bool isFullInstaller { get; set; }
        public String DownloadCookie { get; set; }

        public static Installable parseFromJson( String json ) {
            return JsonConvert.DeserializeObject<Installable>( json );
        }

        public static List<Installable> parseFromJsonToList( String json ) {
            return JsonConvert.DeserializeObject<List<Installable>>( json );
        }
    }
}
