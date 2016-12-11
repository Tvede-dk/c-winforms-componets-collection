using System;

namespace winforms_collection.validator {
    public class NumberString : IValidatorType {
        public bool AllowDecimal { get; set; }
        public bool AllowInt { get; set; }


        private String _errorMsg;



        #region IValidatorType Members

        public bool Validate( string text ) {
            _errorMsg = "unknown error";
            if ( AllowDecimal ) {
                double d;
                int i;
                var sucess = Double.TryParse( text, out d );
                if ( !sucess ) {
                    if ( int.TryParse( text, out i ) ) {
                        return true;
                    } else {
                        _errorMsg = "Not a number";
                        return false;
                    }
                } else {
                    return true;
                }
            } else if ( AllowInt ) {
                int i;
                if ( int.TryParse( text, out i ) ) {
                    return true;
                } else {
                    _errorMsg = "Not a number";
                    return false;
                }
            } else {
                //TODO should throw something ??
                return false;
            }
        }

        public string GetErrorMessage() {
            return _errorMsg;
        }




        #endregion
    }
}
