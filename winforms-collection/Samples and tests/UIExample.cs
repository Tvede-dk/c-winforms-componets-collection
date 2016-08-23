using System;
using System.Windows.Forms;

namespace Samples_and_tests {
    public partial class UIExample : Form {
        public UIExample() {
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
