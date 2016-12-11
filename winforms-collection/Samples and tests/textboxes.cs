using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using winforms_collection;
namespace Samples_and_tests {
    public partial class Textboxes : Form {
        public Textboxes() {
            InitializeComponent();
            var t = new Timer();
            t.Interval = 500;
            t.Enabled = true;
            t.Start();
            t.Tick += T_Tick;
            new Controls().Show();
        }

        private void button1_Click( object sender, EventArgs e ) {

            //bool isSingleFasterThanMultithreaded = true;
            //string[] array;

            //int size = 20000;
            //while ( isSingleFasterThanMultithreaded ) {
            //    array = new string[size + 1];
            //    double timeSingle = profile( 1, () => { SharedStringUtils.innerWorkings.simpleRemoveIndexFromArray( array, array.Length / 2 ); } );
            //    double timeMulti = profile( 1, () => { SharedStringUtils.innerWorkings.fastMP_RemoveIndexFromArray( array, array.Length / 2 ); } );
            //    size += 5;
            //    isSingleFasterThanMultithreaded = timeMulti >= timeSingle;
            //}
            //MessageBox.Show( "value was: " + size );


        }

        private void T_Tick( object sender, EventArgs e ) {
            progressbar1.ProgressInProcent++;
        }

        public static double Profile( int iterations, Action func ) {
            // warm up jit.
            func();

            var watch = new Stopwatch();

            // clean up
            GC.Collect();
            GC.WaitForPendingFinalizers(); //lets make sure..
            GC.Collect();

            watch.Start();
            for ( var i = 0; i < iterations; i++ ) {
                func();
            }
            watch.Stop();
            return watch.Elapsed.TotalMilliseconds;
        }

        private void button2_Click( object sender, EventArgs e ) {
            string[] arr1 = sTextbox3.Lines;
            string[] arr2 = sTextbox4.Lines;
            string[] res = arr1.Union( arr2 ).ToArray();
            IEnumerable<string> mergedDistinctList = new HashSet<string>( arr1 ).Union( arr2 );
            Array.Sort( res );
            sTextbox5.Lines = mergedDistinctList.ToArray();
        }

        private void button3_Click( object sender, EventArgs e ) {

            string[] lines = sTextbox6.Lines;
            var indexInWordList = (int)numericUpDown1.Value;
            string[] result = new string[lines.Length];
            char[] split = new char[] { ' ', ',' };
            Parallel.For( 0, lines.Length,
                ( int index ) => {
                    var words = lines[index].Split( split );
                    if ( indexInWordList < words.Length ) {
                        result[index] = words[indexInWordList];
                    }
                } );
            sTextbox7.Lines = result;
        }

        private void button4_Click( object sender, EventArgs e ) {

            var mult = Profile( 1, () => {
                SharedFunctionalities.SharedStringUtils.InnerWorkings.SplitStringFast( winforms_collection.Properties.Resources.names, Environment.NewLine, StringSplitOptions.None );
            } );

            var single = Profile( 1, () => {
                winforms_collection.Properties.Resources.names.Split( new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries );
            } );
            MessageBox.Show( "mult" + mult + ", single" + single );
        }

        private void textboxes_Load( object sender, EventArgs e ) {

        }

        private void button5_Click( object sender, EventArgs e ) {
            var pop = new FastNoticePopup( "", 2000, null );
            pop.ShowDialog();
        }

        private void button6_Click( object sender, EventArgs e ) {
            FastNoticePopup.ShowInfo( "test", this );

        }

        private void button7_Click( object sender, EventArgs e ) {
            FastNoticePopup.ShowWarning( "test", this );
        }

        private void button8_Click( object sender, EventArgs e ) {
            FastNoticePopup.ShowError( "test", this );
        }

        private void button9_Click( object sender, EventArgs e ) {
            FastNoticePopup.ShowSucess( "test", this );
        }

        private void button10_Click( object sender, EventArgs e ) {
            winforms_collection.popup_boxes.NotificationBar.ShowAtLocation( "test text", new System.Drawing.Point { Y = 50, X = 50 } );
        }

        private void styleableButton1_Click( object sender, EventArgs e ) {
            MessageBox.Show( "i love it" );
        }
    }
}
