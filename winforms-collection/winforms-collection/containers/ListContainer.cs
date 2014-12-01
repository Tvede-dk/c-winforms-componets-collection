using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.containers {
    public partial class ListContainer : Panel {



        #region property SplitHeight
        private int _SplitHeight;

        [EditorBrowsable]
        [Description("Determines the amount of visible controls in the list, and splits the height equally")]
        public int SplitHeight {
            get { return _SplitHeight; }
            set {
                _SplitHeight = value;
                this.Invalidate( true );
            }
        }
        #endregion



        #region property minSplitHeight
        private int _minSplitHeight;


        public int minSplitHeight {
            get { return _minSplitHeight; }
            set { _minSplitHeight = value; }
        }
        #endregion




        #region property heightWeight
        private List<Action<BindingList<float>>> _heightWeightChangeListners = new List<Action<BindingList<float>>>();

        private BindingList<float> _heightWeight;
        [EditorBrowsable]
        [Editor()]
        [Description("")]
        public BindingList<float> heightWeight {
            get { return _heightWeight; }
            set {
                _heightWeight.Clear();//should suspend the invalidation here.
                _heightWeight.RaiseListChangedEvents = false;
                foreach ( var item in value ) {
                    _heightWeight.Add( item );
                }
                _heightWeight.RaiseListChangedEvents = true;

                //_heightWeight = value;
                foreach ( var item in _heightWeightChangeListners ) { item( value ); }
            }
        }
        private int HeightOf0WeightControls = 0;
        private float WeightHeight1Weight = 1;
        #endregion




        public ListContainer() {
            InitializeComponent();
            _heightWeight = new BindingList<float>();
            _heightWeight.ListChanged += HeightWeight_ListChanged;
            _heightWeightChangeListners.Add( new Action<BindingList<float>>( ( BindingList<float> data ) => { HeightWeight_ListChanged( null, null ); } ) );
        }


        private void HeightWeight_ListChanged( object sender, ListChangedEventArgs e ) {

            WeightHeight1Weight = 0;
            int counter = 0;
            foreach ( var item in heightWeight ) {
                if ( item == 0 ) {
                    HeightOf0WeightControls += Controls[counter].Height;
                } else {
                    WeightHeight1Weight += item;
                }
                counter++;
            }
            Refresh();
        }

        protected override void OnControlAdded( ControlEventArgs e ) {
            base.OnControlAdded( e );
            handleChildControl( e.Control );
        }

        public override Size GetPreferredSize( Size proposedSize ) {
            return base.GetPreferredSize( proposedSize );
        }
        protected override Rectangle GetScaledBounds( Rectangle bounds, SizeF factor, BoundsSpecified specified ) {
            return base.GetScaledBounds( bounds, factor, specified );
        }

        protected override void OnResize( EventArgs eventargs ) {
            base.OnResize( eventargs );
            handleChildrenSizing();
        }

        protected override void OnInvalidated( InvalidateEventArgs e ) {
            handleChildrenSizing();
            base.OnInvalidated( e );
        }

        private void handleChildrenSizing() {
            foreach ( Control item in Controls ) {
                handleChildControl( item );
            }
        }

        private void handleChildControl( Control item ) {
            if ( SplitHeight > 0 && Height > minSplitHeight || heightWeight.Count > 0 ) {
                int newHeight = 10;
                var ctrolIndex = Controls.GetChildIndex( item );
                if ( heightWeight != null && heightWeight.Count > ctrolIndex ) {
                    if ( heightWeight[ctrolIndex] > 0 ) {
                        newHeight = (int)(((Height- HeightOf0WeightControls) * heightWeight[ctrolIndex]) / WeightHeight1Weight);
                    } else {
                        newHeight = item.Height;
                    }
                } else if ( SplitHeight > 0 ) {
                    newHeight = Height / SplitHeight;
                } else {
                    newHeight = item.Height;
                }
                item.Size = new Size( Width, newHeight );
            } else {
                //either use virtual space or ??
                AutoScroll = true;
            }
            item.Dock = DockStyle.Top;
            item.AutoSize = false;   //should remove auto sizing ??
        }


    }
}
