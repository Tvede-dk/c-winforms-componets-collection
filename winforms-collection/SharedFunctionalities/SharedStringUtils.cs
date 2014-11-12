using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities {
    public class SharedStringUtils {

        public static String[] removeIndexFromArray( String[] array, int index ) {
            if ( array.Length > 20000 ) { //we could determin this at runtime, but that would be insane.. but arround this value. 
                return innerWorkings.fastMP_RemoveIndexFromArray( array, index );
            } else {
                return innerWorkings.simpleRemoveIndexFromArray( array, index );
            }

        }

        public static class innerWorkings {

            public static string[] simpleRemoveIndexFromArray( string[] array, int index ) {
                string[] result = new string[array.Length - 1];
                for ( int i = 0; i < array.Length; i++ ) {
                    if ( i != index ) {
                        if ( i > index ) {
                            result[i - 1] = array[i];
                        } else {
                            result[i] = array[i];
                        }
                    }
                }
                return result;
            }

            public static string[] fastMP_RemoveIndexFromArray( string[] array, int index ) {
                string[] result = new string[array.Length - 1];
                Parallel.For( 0, array.Length, ( int currentIndex ) => {
                    if ( currentIndex != index ) {
                        if ( currentIndex > index ) {
                            result[currentIndex - 1] = array[currentIndex];
                        } else {
                            result[currentIndex] = array[currentIndex];
                        }
                    }
                } );
                return result;
            }
        }





    }
}
