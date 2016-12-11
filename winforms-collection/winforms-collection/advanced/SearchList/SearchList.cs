using SharedFunctionalities;
using System;
using System.Collections.Generic;

namespace winforms_collection.advanced {
    public class SearchList : CustomControl {


        #region property isPopover
        private bool _isPopOver;


        public bool IsPopOver {
            get { return _isPopOver; }
            set { _isPopOver = value; }
        }
        #endregion


        public void AddStrings( IEnumerable<string> list ) {
            foreach ( var item in list ) {
                AddString( item );
            }
        }

        public void AddString( string str ) {

        }

        public void AddString( string str, string tooltip ) {

        }

        public void AddString( String str, string a, int aa ) {
            
        }




    }
}
