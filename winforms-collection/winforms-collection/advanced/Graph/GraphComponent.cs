using SharedFunctionalities;
using SharedFunctionalities.drawing.layers.backgrounds;
using System.Windows.Forms;

namespace winforms_collection.advanced.Graph {
    public class GraphComponent : CustomControl {

        private const int WheelDelta = 120;


        #region property spaceBetween

        public int SpaceBetween {
            get { return _back.SpaceBetween; }
            set {
                _back.SpaceBetween = value;
                Invalidate();
            }
        }
        #endregion

        readonly GridBackground _back = new GridBackground();

        public GraphComponent() {
            DrawHandler.RemoveLayer( 0 );
            DrawHandler.AddLayer( _back );
            DrawHandler.DisableCommCache();
        }

        protected override void OnMouseWheel( MouseEventArgs e ) {
            base.OnMouseWheel( e );
            var zoom = e.Delta / WheelDelta;
            if ( SpaceBetween + zoom > 5 && SpaceBetween + zoom < 200 ) {
                SpaceBetween += zoom;
            }
        }
        protected override void OnMouseClick( MouseEventArgs e ) {
            base.OnMouseClick( e );
            Invalidate();//well for testing
        }
        /**
        zooming effect:
                    SmartUITimer timer = new SmartUITimer( this );
            timer.repeate = true;
            timer.counter = 200;
            timer.interval = 20;
            timer.start( ( object obj, System.Timers.ElapsedEventArgs args, SmartTimer st ) => {
                graphComponent1.spaceBetween += 2;
            }, () => { } );
        */

        //panning

        protected override void OnMouseMove( MouseEventArgs e ) {
            base.OnMouseMove( e );
            //if ( IsMouseInside() ) {
            //    back.translateX = e.X;
            //    Invalidate();
            //}
        }
    }
}
