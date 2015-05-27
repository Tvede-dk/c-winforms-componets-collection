using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Windows_organizer.Interfaces;


namespace Windows_organizer.interfaces {
    [Serializable]
    public class Installable {


        #region equals

        protected bool Equals( Installable other ) {
            return string.Equals( ProductName , other.ProductName );
        }

        public override int GetHashCode() {
            return (ProductName != null ? ProductName.GetHashCode() : 0);
        }

        public override bool Equals( object obj ) {
            if ( ReferenceEquals( null , obj ) ) return false;
            if ( ReferenceEquals( this , obj ) ) return true;
            return obj.GetType() == this.GetType() && Equals( (Installable)obj );
        }

        #endregion

        public Installable( string desc , string productName , List<string> categories , bool supportSettingSync ) {
            this.Description = desc;
            this.ProductName = productName;
            this.Categories = categories;
            this.SupportSettingSync = supportSettingSync;
        }

        public Installable( string desc , string productName , List<string> categories ,bool supportSettingSync , string programUrl , string programImageUrl ) {
            this.Description = desc;
            this.ProductName = productName;
            this.Categories = categories;
            this.SupportSettingSync = supportSettingSync;
            this.downloadHtmlUrl = programUrl;
            this.imageUrl = programImageUrl;
            this.requireBrowser = true;
        }

        public Installable(string desc, string productName, List<string> categories, bool supportSettingSync, string programUrl, string programImageUrl, bool requireBrowser, string silentSwitch) {
            this.Description = desc;
            this.ProductName = productName;
            this.Categories = categories;
            this.SupportSettingSync = supportSettingSync;
            this.downloadHtmlUrl = programUrl;
            this.imageUrl = programImageUrl;
            this.requireBrowser = requireBrowser;
            this.SilentSwitchCommandLine = silentSwitch;
        }

        public String Description { get; private set; }

        public String ProductName { get; private set; }

        public List<string> Categories { get; private set; }

        public List<Installable> dependencies{ get; private set; }

        public String SilentSwitchCommandLine { get; private set; }

        public bool SupportSettingSync { get; private set; }

        public SettingsHandler settingsHandler { get; private set; }

        public String downloadHtmlUrl { get; private set; }

        public String imageUrl { get; private set; }

        public bool requireBrowser { get; private set; }

        //possible fields:

        //vender

        //64 bit ?

        //require internet ? [is full installer ?? ]

        // cookie 





    }
}
