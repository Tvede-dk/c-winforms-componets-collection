using SharedFunctionalities;
using SharedFunctionalities.drawing.layers.backgrounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winforms_collection.advanced.Graph {
    public class GraphComponent : CustomControl {


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
            DrawHandler.addLayer( back );
        }
        /**
        zooming:
                    SmartUITimer timer = new SmartUITimer( this );
            timer.repeate = true;
            timer.counter = 200;
            timer.interval = 20;
            timer.start( ( object obj, System.Timers.ElapsedEventArgs args, SmartTimer st ) => {
                graphComponent1.spaceBetween += 2;
            }, () => { } );
        */
    }
}
