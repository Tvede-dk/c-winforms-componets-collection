using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedFunctionalities {
    public class SharedStringUtils {

        public static String[] removeIndexFromArray( String[] array, int index ) {
            if ( array.Length > 20000 ) { //we could determin this at runtime, but that would be insane.. but arround this value. [tested on 4 ghz machine, so on lower end devices, this should be lower ?? ]
                return innerWorkings.fastMP_RemoveIndexFromArray( array, index );
            } else {
                return innerWorkings.simpleRemoveIndexFromArray( array, index );
            }
        }

        public static string[] insertValueIntoIndexInArray( string[] array, string value, int index ) {
            return innerWorkings.simpleInsertIndexIntoArray( array, value, index );
            //todo make a multithreaded edtion as well.
        }

        public static class innerWorkings {

            //TODO see if we cant actually make this work. this is a hack verison.
            public static string[] splitStringFast( string data, String newLineChar, StringSplitOptions options ) {
                int objs = data.Length / 4;
                var collecIndexs = new System.Collections.Concurrent.ConcurrentBag<int>();
                Parallel.For( 0, 4, ( int index ) => {
                    List<Int32> indexes = new List<Int32>();
                    for ( int i = index * objs; i < (index + 1) * objs; i++ ) {
                        bool found = true;

                        if ( newLineChar.Length == 2 ) {
                            found = (data[i] == newLineChar[0] && data[i + 1] == newLineChar[1]);
                        } else {
                            for ( int y = 0; y < newLineChar.Length; y++ ) {
                                if ( data[i] != newLineChar[y] ) {
                                    found = false;
                                    break;
                                }
                            }
                        }
                        if ( found ) {
                            collecIndexs.Add( i );
                        }
                    }

                } );
                int[] indexs = collecIndexs.ToArray();
                string[] result = new string[collecIndexs.Count];
                Parallel.For( 0, indexs.Count(), ( int indexInCollection ) => {
                    int dataIndex = indexs[indexInCollection];
                    String s = "";
                    while ( dataIndex >= 0 && data[dataIndex] != '\n' ) {
                        s = data[dataIndex] + s;
                        dataIndex--;
                    }
                    result[indexInCollection] = s;
                } );
                return result;
            }

            public static string[] simpleInsertIndexIntoArray( string[] array, string value, int index ) {
                string[] result = new string[array.Length + 1];
                for ( int i = 0; i < array.Length + 1; i++ ) {
                    if ( i != index ) {
                        if ( i > index ) {
                            result[i] = array[i - 1];
                        } else {
                            result[i] = array[i];
                        }
                    } else {
                        result[i] = value;
                    }
                }
                return result;
            }

            public static string[] simpleRemoveIndexFromArray( string[] array, int index ) {
                if ( array.Length == 1 ) {
                    return new string[0];
                }
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
                if ( array.Length == 1 ) {
                    return new string[0];
                }
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
