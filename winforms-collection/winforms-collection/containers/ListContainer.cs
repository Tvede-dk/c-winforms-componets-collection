using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace winforms_collection.containers {
    public partial class ListContainer : Panel {



        #region property DirectionHorizontal
        private bool _directionHorizontal;


        public bool DirectionHorizontal {
            get { return _directionHorizontal; }
            set { _directionHorizontal = value; }
        }
        #endregion


        #region property SplitHeight
        private int _splitHeight;

        [EditorBrowsable]
        [Description("Determines the amount of visible controls in the list, and splits the height equally")]
        public int SplitHeight {
            get { return _splitHeight; }
            set {
                _splitHeight = value;
                this.Invalidate( true );
            }
        }
        #endregion



        #region property minSplitHeight
        private int _minSplitHeight;


        public int MinSplitHeight {
            get { return _minSplitHeight; }
            set { _minSplitHeight = value; }
        }
        #endregion




        #region property heightWeight
        private readonly List<Action<BindingList<float>>> _heightWeightChangeListners = new List<Action<BindingList<float>>>();

        private readonly BindingList<float> _heightWeight;
        [EditorBrowsable]
        [Editor()]
        [Description("")]
        public BindingList<float> HeightWeight {
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
        private int _heightOf0WeightControls = 0;
        private float _weightHeight1Weight = 1;
        #endregion




        public ListContainer() {
            InitializeComponent();
            _heightWeight = new BindingList<float>();
            _heightWeight.ListChanged += HeightWeight_ListChanged;
            _heightWeightChangeListners.Add( new Action<BindingList<float>>( ( BindingList<float> data ) => { HeightWeight_ListChanged( null, null ); } ) );
        }


        private void HeightWeight_ListChanged( object sender, ListChangedEventArgs e ) {

            _weightHeight1Weight = 0;
            var counter = 0;
            foreach ( var item in HeightWeight ) {
                if ( item == 0 ) {
                    if ( DirectionHorizontal ) {
                        _heightOf0WeightControls += Controls[counter].Width;
                    } else {
                        _heightOf0WeightControls += Controls[counter].Height;
                    }
                } else {
                    _weightHeight1Weight += item;
                }
                counter++;
            }
            Refresh();
        }

        protected override void OnControlAdded( ControlEventArgs e ) {
            base.OnControlAdded( e );
            HandleChildControl( e.Control );
        }

        public override Size GetPreferredSize( Size proposedSize ) {
            return base.GetPreferredSize( proposedSize );
        }
        protected override Rectangle GetScaledBounds( Rectangle bounds, SizeF factor, BoundsSpecified specified ) {
            return base.GetScaledBounds( bounds, factor, specified );
        }

        protected override void OnResize( EventArgs eventargs ) {
            base.OnResize( eventargs );
            HandleChildrenSizing();
        }

        protected override void OnInvalidated( InvalidateEventArgs e ) {
            HandleChildrenSizing();
            base.OnInvalidated( e );
        }

        private void HandleChildrenSizing() {
            foreach ( Control item in Controls ) {
                HandleChildControl( item );
            }
        }

        private void HandleChildControl( Control item ) {

            if ( DirectionHorizontal ) {
                if ( SplitHeight > 0 && Width > MinSplitHeight || HeightWeight.Count > 0 ) {
                    var newWidth = 10;
                    var ctrolIndex = Controls.GetChildIndex( item );
                    if ( HeightWeight != null && HeightWeight.Count > ctrolIndex ) {
                        if ( HeightWeight[ctrolIndex] > 0 ) {
                            newWidth = (int)(((Width - _heightOf0WeightControls) * HeightWeight[ctrolIndex]) / _weightHeight1Weight);
                        } else {
                            newWidth = item.Width;
                        }
                    } else if ( SplitHeight > 0 ) {
                        newWidth = Width / SplitHeight;
                    } else {
                        newWidth = item.Width;
                    }
                    item.Size = new Size( newWidth, Height );
                } else {
                    AutoScroll = true;
                }
                item.Dock = DockStyle.Left;
            } else {
                if ( SplitHeight > 0 && Height > MinSplitHeight || HeightWeight.Count > 0 ) {
                    var newHeight = 10;
                    var ctrolIndex = Controls.GetChildIndex( item );
                    if ( HeightWeight != null && HeightWeight.Count > ctrolIndex ) {
                        if ( HeightWeight[ctrolIndex] > 0 ) {
                            newHeight = (int)(((Height - _heightOf0WeightControls) * HeightWeight[ctrolIndex]) / _weightHeight1Weight);
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
                    AutoScroll = true;
                }
                item.Dock = DockStyle.Top;
            }
            item.AutoSize = false;   //should remove auto sizing ??
        }


    }
}
