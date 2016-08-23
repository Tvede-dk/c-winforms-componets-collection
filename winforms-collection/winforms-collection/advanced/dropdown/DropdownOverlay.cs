using System.Windows.Forms;

namespace winforms_collection.advanced.dropdown {
    public partial class DropdownOverlay : Form {
        public DropdownOverlay() {
            InitializeComponent();
        }
        protected override bool ShowWithoutActivation {
            get {
                return true;
            }
        }
    }
}
