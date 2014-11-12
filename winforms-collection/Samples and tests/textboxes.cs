using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedFunctionalities;
using System.Diagnostics;

namespace Samples_and_tests {
    public partial class textboxes : Form {
        public textboxes() {
            InitializeComponent();
        }

        private void button1_Click( object sender, EventArgs e ) {

            bool isSingleFasterThanMultithreaded = true;
            string[] array;

            int size = 20000;
            while ( isSingleFasterThanMultithreaded ) {
                array = new string[size + 1];
                double timeSingle = profile( 1, () => { SharedStringUtils.innerWorkings.simpleRemoveIndexFromArray( array, array.Length / 2 ); } );
                double timeMulti = profile( 1, () => { SharedStringUtils.innerWorkings.fastMP_RemoveIndexFromArray( array, array.Length / 2 ); } );
                size += 5;
                isSingleFasterThanMultithreaded = timeMulti >= timeSingle;
            }
            MessageBox.Show( "value was: " + size );
        }

        public static double profile( int iterations, Action func ) {
            // warm up jit.
            func();

            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers(); //lets make sure..
            GC.Collect();

            watch.Start();
            for ( int i = 0; i < iterations; i++ ) {
                func();
            }
            watch.Stop();
            return watch.Elapsed.TotalMilliseconds;
        }
    }
}
