using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace winforms_collection.extendedComponents {
    public class DraggableTabControl : System.Windows.Forms.TabControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.Container _components = null;

        public DraggableTabControl() {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            AllowDrop = true;
            // TODO: Add any initialization after the InitForm call

        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (_components != null) {
                    _components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion

        protected override void OnDragOver(System.Windows.Forms.DragEventArgs e) {
            base.OnDragOver(e);

            var pt = new Point(e.X, e.Y);
            //We need client coordinates.
            pt = PointToClient(pt);

            //Get the tab we are hovering over.
            TabPage hoverTab = GetTabPageByTab(pt);

            //Make sure we are on a tab.
            if (hoverTab != null) {
                //Make sure there is a TabPage being dragged.
                if (e.Data.GetDataPresent(typeof(TabPage))) {
                    e.Effect = DragDropEffects.Move;
                    var dragTab = (TabPage)e.Data.GetData(typeof(TabPage));

                    var itemDragIndex = FindIndex(dragTab);
                    var dropLocationIndex = FindIndex(hoverTab);

                    //Don't do anything if we are hovering over ourself.
                    if (itemDragIndex != dropLocationIndex) {
                        var pages = new ArrayList();

                        //Put all tab pages into an array.
                        for (var i = 0; i < TabPages.Count; i++) {
                            //Except the one we are dragging.
                            if (i != itemDragIndex)
                                pages.Add(TabPages[i]);
                        }

                        //Now put the one we are dragging it at the proper location.
                        pages.Insert(dropLocationIndex, dragTab);

                        //Make them all go away for a nanosec.
                        TabPages.Clear();

                        //Add them all back in.
                        TabPages.AddRange((TabPage[])pages.ToArray(typeof(TabPage)));

                        //Make sure the drag tab is selected.
                        SelectedTab = dragTab;
                    }
                }
            } else {
                e.Effect = DragDropEffects.None;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left) {
                var pt = new Point(e.X, e.Y);
                TabPage tp = GetTabPageByTab(pt);

                if (tp != null) {
                    DoDragDrop(tp, DragDropEffects.All);
                }
            }
        }

        /// <summary>
        /// Finds the TabPage whose tab is contains the given point.
        /// </summary>
        /// <param name="pt">The point (given in client coordinates) to look for a TabPage.</param>
        /// <returns>The TabPage whose tab is at the given point (null if there isn't one).</returns>
        private TabPage GetTabPageByTab(Point pt) {
            TabPage tp = null;

            for (var i = 0; i < TabPages.Count; i++) {
                if (GetTabRect(i).Contains(pt)) {
                    tp = TabPages[i];
                    break;
                }
            }

            return tp;
        }

        /// <summary>
        /// Loops over all the TabPages to find the index of the given TabPage.
        /// </summary>
        /// <param name="page">The TabPage we want the index for.</param>
        /// <returns>The index of the given TabPage(-1 if it isn't found.)</returns>
        private int FindIndex(TabPage page) {
            for (var i = 0; i < TabPages.Count; i++) {
                if (TabPages[i] == page)
                    return i;
            }

            return -1;
        }
    }
}
