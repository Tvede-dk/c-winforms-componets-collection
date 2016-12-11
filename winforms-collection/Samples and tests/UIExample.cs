using System;
using System.Windows.Forms;

namespace Samples_and_tests {
    public partial class UiExample : Form {
        public UiExample() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            collapsableSplitContainer1.CollapsePanel1();
        }

        private void button2_Click(object sender, EventArgs e) {
            collapsableSplitContainer1.ExpandPanel1();
        }
    }
}
