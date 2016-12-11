using System;
using System.Windows.Forms;
using SharedFunctionalities;

namespace winforms_collection.navigation {
    public partial class CollapsableSplitContainer : SplitContainer {

        private ContainerPanelState _state = new ContainerPanelState();

        private SmartUiTimer _currentAnimation = null;

        private readonly PanelCollapsedSelector _collapsedPanel = new PanelCollapsedSelector();

        public CollapsableSplitContainer() {
            InitializeComponent();
            _collapsedPanel.IsPanel1Collapsed = Panel1Collapsed;
            _collapsedPanel.IsPanel2Collapsed = Panel2Collapsed;
        }

        private void StopAnimations() {
            _currentAnimation?.Stop(false);
        }
        private void SavePanelState() {
            _state.isSplitterFixed = IsSplitterFixed;
            _state.splitterDistance = SplitterDistance;
            _state.FixPanel = FixedPanel;
        }

        private void LoadPanelState() {
            IsSplitterFixed = _state.isSplitterFixed;
            SplitterDistance = _state.splitterDistance;
            FixedPanel = _state.FixPanel;
        }


        private void ResetSplitter(bool isForPanel1) {
            IsSplitterFixed = false;
            FixedPanel = FixedPanel.None;
            if (isForPanel1) {
                Panel1MinSize = 0;
            } else {
                Panel2MinSize = 0;
            }
        }

        public void CollapsePanel1() {
            if (_collapsedPanel.IsAnyCollapsed) {
                return;
            }

            StopAnimations();

            _collapsedPanel.IsPanel1Collapsed = true;
            SavePanelState();
            ResetSplitter(true);

            SharedAnimations.AnimateProperty(this, SplitterDistance, 0, 300, (diff) => {
                SplitterDistance = Math.Max(SplitterDistance + diff, 0);
            }, () => {
                SplitterDistance = 0;
                _currentAnimation = null;
                _collapsedPanel.IsPanel1Collapsed = true;
            });

        }

        public void ExpandPanel1() {
            if (!_collapsedPanel.IsPanel1Collapsed) {
                return;
            }
            StopAnimations();
            ResetSplitter(false);

            _currentAnimation = SharedAnimations.AnimateProperty(this, SplitterDistance, _state.splitterDistance, 300, (diff) => {
                SplitterDistance += Math.Max(diff, 0);
            }, () => {
                _collapsedPanel.IsPanel1Collapsed = false;
                _currentAnimation = null;
                LoadPanelState();
            });
        }

        private class PanelCollapsedSelector {

            private bool _isPanel1Collapsed = false;
            private readonly bool _isPanel2Collapsed = false;

            public bool IsPanel1Collapsed
            {
                get
                {
                    return _isPanel1Collapsed;
                }
                set
                {
                    _isPanel1Collapsed = value;
                }
            }

            public bool IsPanel2Collapsed
            {
                get
                {
                    return _isPanel2Collapsed;
                }
                set
                {
                    _isPanel1Collapsed = value;
                }
            }

            public bool IsAnyCollapsed
            {
                get
                {
                    return _isPanel1Collapsed || _isPanel2Collapsed;
                }
            }



        }

        private struct ContainerPanelState {
            public int splitterDistance;
            public bool isSplitterFixed;
            public FixedPanel FixPanel;
        }
    }

}
