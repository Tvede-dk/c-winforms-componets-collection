using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Windows_organizer.Downloaders {
    [Serializable]
    [Table( Name = "downloadable" )]
    class DownloadableObjs
    {
        [Key]
        public int id { get; private set; }
        public String name { get; private set; }
        public String companyName { get; private set; }
        public String silentInstallerSwitch { get; private set; }
        public String description { get; private set; }
        public String logourl { get; private set; }
        public String programUrl { get; private set; }
        public bool requireJSSupport { get; private set; }
        public List<string> Categories { get; private set; }
        public int SizeInMb { get; private set; }
        public bool SupportSettingSync { get; private set; }

    }
}
