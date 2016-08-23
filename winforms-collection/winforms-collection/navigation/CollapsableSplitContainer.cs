using System;
using System.Windows.Forms;
using SharedFunctionalities;

namespace winforms_collection.navigation {
    public partial class CollapsableSplitContainer : SplitContainer {

        private ContainerPanelState state = new ContainerPanelState();

        private SmartUITimer currentAnimation = null;

        private PanelCollapsedSelector collapsedPanel = new PanelCollapsedSelector();

        public CollapsableSplitContainer() {
            InitializeComponent();
            collapsedPanel.IsPanel1Collapsed = Panel1Collapsed;
            collapsedPanel.IsPanel2Collapsed = Panel2Collapsed;
        }

        private void stopAnimations() {
            currentAnimation?.Stop(false);
        }
        private void savePanelState() {
            state.isSplitterFixed = IsSplitterFixed;
            state.splitterDistance = SplitterDistance;
            state.fixPanel = FixedPanel;
        }

        private void loadPanelState() {
            IsSplitterFixed = state.isSplitterFixed;
            SplitterDistance = state.splitterDistance;
            FixedPanel = state.fixPanel;
        }


        private void resetSplitter(bool isForPanel1) {
            IsSplitterFixed = false;
            FixedPanel = FixedPanel.None;
            if (isForPanel1) {
                Panel1MinSize = 0;
            } else {
                Panel2MinSize = 0;
            }
        }

        public void CollapsePanel1() {
            if (collapsedPanel.IsAnyCollapsed) {
                return;
            }

            stopAnimations();

            collapsedPanel.IsPanel1Collapsed = true;
            savePanelState();
            resetSplitter(true);

            SharedAnimations.animateProperty(this, SplitterDistance, 0, 300, (diff) => {
                SplitterDistance = Math.Max(SplitterDistance + diff, 0);
            }, () => {
                SplitterDistance = 0;
                currentAnimation = null;
                collapsedPanel.IsPanel1Collapsed = true;
            });

        }

        public void ExpandPanel1() {
            if (!collapsedPanel.IsPanel1Collapsed) {
                return;
            }
            stopAnimations();
            resetSplitter(false);

            currentAnimation = SharedAnimations.animateProperty(this, SplitterDistance, state.splitterDistance, 300, (diff) => {
                SplitterDistance += Math.Max(diff, 0);
            }, () => {
                collapsedPanel.IsPanel1Collapsed = false;
                currentAnimation = null;
                loadPanelState();
            });
        }

        private class PanelCollapsedSelector {

            private bool isPanel1Collapsed = false;
            private bool isPanel2Collapsed = false;

            public bool IsPanel1Collapsed
            {
                get
                {
                    return isPanel1Collapsed;
                }
                set
                {
                    isPanel1Collapsed = value;
                }
            }

            public bool IsPanel2Collapsed
            {
                get
                {
                    return isPanel2Collapsed;
                }
                set
                {
                    isPanel1Collapsed = value;
                }
            }

            public bool IsAnyCollapsed
            {
                get
                {
                    return isPanel1Collapsed || isPanel2Collapsed;
                }
            }



        }

        private struct ContainerPanelState {
            public int splitterDistance;
            public bool isSplitterFixed;
            public FixedPanel fixPanel;
        }
    }

}
