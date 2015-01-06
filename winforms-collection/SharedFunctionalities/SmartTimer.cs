using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SharedFunctionalities {
    public class SmartTimer {
        #region variables and properties
        private Timer innerTimer = new Timer();

        private Action afterHandler;

        private int currentCounter = 0;

        private Action<object, System.Timers.ElapsedEventArgs, SmartTimer> TimerHandler;
        #region property interval 

        public int interval {
            get { return (int)innerTimer.Interval; }
            set { innerTimer.Interval = value; }
        }

        #endregion


        #region property counter
        private int _counter = int.MaxValue;


        public int counter {
            get { return _counter; }
            set { _counter = value; }
        }
        #endregion



        #region property continuous


        public bool repeate {
            get { return innerTimer.AutoReset; }
            set { innerTimer.AutoReset = value; }
        }
        #endregion

        #endregion

        public SmartTimer() {
            //always have a default value, rather than null's n zeros. That usually results in a much more "simple" code. 
            innerTimer.AutoReset = true;
            innerTimer.Interval = 250;
            innerTimer.Elapsed += onTimer;
        }

        private void onTimer( object sender, ElapsedEventArgs e ) {
            if ( TimerHandler != null && currentCounter <= counter && repeate ) {
                TimerHandler.Invoke( sender, e, this );
                currentCounter++;
            } else {
                stop();
            }
        }

        #region public methods
        /// <summary>
        /// Starts a timer with the current settings. It calls the first argument each "interval".
        /// </summary>
        /// <param name="handler"> the on "ticeket" function. NB: the first object is the sender.</param>
        /// <param name="after">The event after we are done.(can be null)</param>
        public virtual void start( Action<object, ElapsedEventArgs, SmartTimer> handler, Action after ) {
            this.TimerHandler = handler;
            this.afterHandler = after;
            currentCounter = 0;
            innerTimer.Enabled = true;
            innerTimer.Start();
        }
        /// <summary>
        /// Starts a timer with the current settings. When timeout it calls the onDone method. 
        /// </summary>
        /// <param name="onDone">Waits till the timer times out then call the onDone method</param>
        public virtual void start( Action onDone ) {
            start( ( object sender, ElapsedEventArgs e, SmartTimer timer ) => { }, onDone );

        }
        /// <summary>
        /// Stops the timer. and calls the after handler / onDone (if any)
        /// </summary>
        public void stop() {
            currentCounter = 0;
            TimerHandler = null;
            innerTimer.Enabled = false;
            innerTimer.Stop();
            if ( afterHandler != null ) {
                afterHandler.Invoke();
            }
        }
        #endregion

    }
}
