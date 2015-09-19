using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.simple_components {
    public partial class DescriptionValue : UserControl {


        #region property LabelText

        [Browsable(true)]
        [Description("The text above the input field")]
        public string LabelText {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        #endregion



        #region property EnteredText
        public string EnteredText {
            get { return sTextbox1.Text; }
        }
        #endregion


        public DescriptionValue() {
            InitializeComponent();
        }
    }
}
