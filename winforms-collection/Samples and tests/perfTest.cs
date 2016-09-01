using SharedFunctionalities;
using System;
using System.Windows.Forms;

namespace Samples_and_tests {
    public partial class perfTest : Form {
        public perfTest() {
            InitializeComponent();
        }

        private void perfTest_Load(object sender, EventArgs e) {
            //SmartUITimer timer = new SmartUITimer( this );
            //timer.repeate = true;
            //timer.counter = this.Width;
            //timer.interval = 4;
            //this.Width = 1;
            //timer.start( ( object obj, System.Timers.ElapsedEventArgs args, SmartTimer st ) => {
            //    this.Width++;
            //}, () => { } );
            var timer = new SmartUITimer(this);
            timer.repeate = true;
            timer.counter = 250;
            timer.interval = 8;
            timer.start((object obj, System.Timers.ElapsedEventArgs args, SmartTimer st) => {
                graphComponent1.spaceBetween += 1;
            }, () => { Dispose(); });
        }

        private void graphComponent1_Click(object sender, EventArgs e) {

        }
    }
}
