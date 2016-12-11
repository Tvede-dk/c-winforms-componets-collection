using System;
using System.Timers;

namespace SharedFunctionalities {
    public class SmartTimer {
        #region variables and properties
        private readonly Timer _innerTimer = new Timer();

        private Action _afterHandler;

        private int _currentCounter = 0;

        private Action<object, System.Timers.ElapsedEventArgs, SmartTimer> _timerHandler;
        #region property interval 

        public int Interval
        {
            get { return (int)_innerTimer.Interval; }
            set { _innerTimer.Interval = value; }
        }

        #endregion


        #region property counter
        private int _counter = int.MaxValue;


        public int Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }
        #endregion



        #region property continuous


        public bool Repeate
        {
            get { return _innerTimer.AutoReset; }
            set { _innerTimer.AutoReset = value; }
        }
        #endregion

        #endregion

        public SmartTimer() {
            //always have a default value, rather than null's n zeros. That usually results in a much more "simple" code. 
            _innerTimer.AutoReset = true;
            _innerTimer.Interval = 250;
            _innerTimer.Elapsed += OnTimer;
        }

        private void OnTimer(object sender, ElapsedEventArgs e) {
            if (_timerHandler != null && _currentCounter <= Counter && Repeate) {
                _timerHandler.Invoke(sender, e, this);
                _currentCounter++;
            } else {
                Stop();
            }
        }

        #region public methods
        /// <summary>
        /// Starts a timer with the current settings. It calls the first argument each "interval".
        /// </summary>
        /// <param name="handler"> the on "ticeket" function. NB: the first object is the sender.</param>
        /// <param name="after">The event after we are done.(can be null)</param>
        public virtual void Start(Action<object, ElapsedEventArgs, SmartTimer> handler, Action after) {
            this._timerHandler = handler;
            this._afterHandler = after;
            _currentCounter = 0;
            _innerTimer.Enabled = true;
            _innerTimer.Start();
        }
        /// <summary>
        /// Starts a timer with the current settings. When timeout it calls the onDone method. 
        /// </summary>
        /// <param name="onDone">Waits till the timer times out then call the onDone method</param>
        public virtual void Start(Action onDone) {
            Start((object sender, ElapsedEventArgs e, SmartTimer timer) => { }, onDone);

        }
        /// <summary>
        /// Stops the timer. and calls the after handler / onDone (if any)
        /// </summary>
        /// <param name="mayCallAfterHandler">set to false if afterHandler may not be called. defaults to true.</param>
        public void Stop(bool mayCallAfterHandler = true) {
            _currentCounter = 0;
            _timerHandler = null;
            _innerTimer.Enabled = false;
            _innerTimer.Stop();
            if (mayCallAfterHandler) {
                _afterHandler?.Invoke();
            }
        }
        #endregion

    }
}
