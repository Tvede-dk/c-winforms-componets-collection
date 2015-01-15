using SharedFunctionalities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.advanced {
    public class SearchList : CustomControl {


        #region property isPopover
        private bool _isPopOver;


        public bool IsPopOver {
            get { return _isPopOver; }
            set { _isPopOver = value; }
        }
        #endregion


        public void addStrings( IEnumerable<string> list ) {
            foreach ( var item in list ) {
                addString( item );
            }
        }

        public void addString( string str ) {

        }

        public void addString( string str, string tooltip ) {

        }

        public void addString( String str, string a, int aa ) {
            
        }




    }
}
