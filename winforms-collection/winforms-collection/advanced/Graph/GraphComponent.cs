using SharedFunctionalities;
using SharedFunctionalities.drawing.layers.backgrounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.advanced.Graph {
    public class GraphComponent : CustomControl {

        private const int WHEEL_DELTA = 120;


        #region property spaceBetween

        public int spaceBetween {
            get { return back.SpaceBetween; }
            set {
                back.SpaceBetween = value;
                Invalidate();
            }
        }
        #endregion
        GridBackground back = new GridBackground();

        public GraphComponent() {
            DrawHandler.removeLayer( 0 );
            DrawHandler.addLayer( back );
            DrawHandler.disableCommCache();
        }

        protected override void OnMouseWheel( MouseEventArgs e ) {
            base.OnMouseWheel( e );
            int zoom = e.Delta / WHEEL_DELTA;
            if ( spaceBetween + zoom > 5 && spaceBetween + zoom < 200 ) {
                spaceBetween += zoom;
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
            if ( IsMouseInside() ) {
                back.translateX = e.X;
                Invalidate();
            }
        }
    }
}
