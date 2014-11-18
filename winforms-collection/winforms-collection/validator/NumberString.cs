using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_collection.validator {
    public class NumberString : IValidatorType {
        public bool allowDecimal { get; set; }
        public bool allowInt { get; set; }


        private String errorMsg;



        #region IValidatorType Members

        public bool Validate( string text ) {
            errorMsg = "unknown error";
            if ( allowDecimal ) {
                double d;
                int i;
                bool sucess = Double.TryParse( text, out d );
                if ( !sucess ) {
                    if ( int.TryParse( text, out i ) ) {
                        return true;
                    } else {
                        errorMsg = "Not a number";
                        return false;
                    }
                } else {
                    return true;
                }
            } else if ( allowInt ) {
                int i;
                if ( int.TryParse( text, out i ) ) {
                    return true;
                } else {
                    errorMsg = "Not a number";
                    return false;
                }
            } else {
                //TODO should throw something ??
                return false;
            }
        }

        public string getErrorMessage() {
            return errorMsg;
        }




        #endregion
    }
}
