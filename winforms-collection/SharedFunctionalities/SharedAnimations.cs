using System;
using System.Timers;

using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using SharedFunctionalities.forms;

namespace SharedFunctionalities {
    public static class SharedAnimations {

        #region to refactor
        /// <summary>
        /// returns in MHZ
        /// TODO refactor.
        /// </summary>
        /// <returns></returns>
        private static int GetCpuSpeedInMhz() {
            RegistryKey processorName = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);
            if (processorName != null && processorName.GetValue("ProcessorNameString") != null) {
                return (int)processorName.GetValue("~MHz");
            } else {
                return 1500;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="durationInMs"></param>
        /// <param name="callbackWithDifference"></param>
        /// <param name="onCompleted"></param>
        /// <returns> null if unable to start animation. otherwise the ui timer </returns>
        public static SmartUiTimer AnimateProperty(Control ui, int fromValue, int toValue, int durationInMs, Action<int> callbackWithDifference, Action onCompleted)  {
            if (ui != null && ui.IsHandleCreated) {
                var newSizeDiff = toValue - fromValue;
                var numOfSteps = GetNumberOfFramesByTimeAndFps(durationInMs, _fps);
                var difference = (int)(newSizeDiff / (float)(numOfSteps + 1));
                return StartTimer(durationInMs, () => {
                    callbackWithDifference(difference);
                }, onCompleted, ui);
            }
            return null;
        }

        public static void AnimateWidth(Control ui, int animateTo, int displayTimeInMs, Action onCompleted) {
            if (ui != null && ui.IsHandleCreated) {
                var newSizeDiff = animateTo - ui.Width;
                var numOfSteps = GetNumberOfFramesByTimeAndFps(displayTimeInMs, _fps);
                var difference = (int)(newSizeDiff / (float)(numOfSteps + 1));
                StartTimer(displayTimeInMs, () => {
                    ui.Width += (int)difference;
                }, onCompleted, ui);
            }

        }
        #endregion
        #region constants & static variables
        private const int SecInMs = 1000;

        private static int _fps = 45;

        static SharedAnimations() {
            var speed = GetCpuSpeedInMhz();
            if (speed >= 2000) { //2 ghz
                SetHighQuality();
            } else if (speed > 1500) {
                SetMediumQuality();
            } else {
                SetLowQuality();
            }
        }


        #endregion
        #region quality setters
        /// <summary>
        /// WARNING requires a lot of processing power (120 FPS)
        /// </summary>
        public static void SetUltraQuality() {
            _fps = 120;
        }

        /// <summary>
        /// Looks good, 60 fps for the win
        /// </summary>
        public static void SetHighQuality() {
            _fps = 60;
        }

        /// <summary>
        /// Very smooth, almost not choppy (but might occur).
        /// </summary>
        public static void SetMediumQuality() {
            _fps = 45;
        }
        /// <summary>
        /// will be a bit choppy
        /// </summary>
        public static void SetLowQuality() {
            _fps = 25;
        }

        public static void SetQuality(int fps) {
            SharedAnimations._fps = fps;
        }
        #endregion
        #region fade effects 
        public static void FadeIn(Form f, int displayTimeInMs, Action after, float maxVal = 1f, float startVal = 0f) {
            if (f != null && f.IsHandleCreated) {
                var numOfSteps = GetNumberOfFramesByTimeAndFps(displayTimeInMs, _fps);
                f.BeginInvoke((MethodInvoker)(() => { f.Opacity = startVal; }));
                var difference = maxVal / (float)numOfSteps;
                StartTimer(displayTimeInMs, () => {
                    f.Opacity += difference;
                }, after, f);
            }
        }

        public static void FadeOut(Form f, int displayTimeInMs, Action after, float minVal = 1f, float startVal = 0f) {
            if (f != null && f.IsHandleCreated) {
                if (startVal <= 0f) {
                    startVal = (float)f.Opacity;
                }
                f.BeginInvoke((MethodInvoker)(() => { f.Opacity = startVal; }));
                var numOfSteps = GetNumberOfFramesByTimeAndFps(displayTimeInMs, _fps);
                var diff = minVal / (double)numOfSteps;
                StartTimer(displayTimeInMs, () => {
                    f.Opacity -= diff;
                }, after, f);
            }
        }



        #endregion

        public static void ExpandY(Form f, int displayTimeInMs, Action after, int newSizeY) {
            if (f != null && f.IsHandleCreated) {
                var newSizeDiff = newSizeY - f.Height;
                var numOfSteps = GetNumberOfFramesByTimeAndFps(displayTimeInMs, _fps);
                var difference = (int)(newSizeDiff / (float)(numOfSteps + 1));
                StartTimer(displayTimeInMs, () => {
                    f.Height += (int)difference;
                }, after, f);
            }
        }

        public static void CollapsY(Form f, int displayTimeInMs, Action after, int newSizeY) {
            if (f != null && f.IsHandleCreated) {
                var numOfSteps = GetNumberOfFramesByTimeAndFps(displayTimeInMs, _fps);
                var difference = newSizeY / (float)(numOfSteps + 1);
                StartTimer(displayTimeInMs, () => {
                    f.Height -= (int)difference;
                }, after, f);
            }
        }

        #region internal calculation
        private static SmartUiTimer StartTimer(int displayTimeInMs, Action handler, Action after, Control con) {
            var numOfSteps = GetNumberOfFramesByTimeAndFps(displayTimeInMs, _fps);
            var timer = new SmartUiTimer(con) { Repeate = true, Interval = GetIntervalFromFpsInMs(), Counter = numOfSteps };
            timer.Start((object sender, ElapsedEventArgs e, SmartTimer st) => { handler.Invoke(); }, after);
            return timer;
        }
        /// <summary>
        /// calculates the duration in time from the fps value.
        /// </summary>
        /// <returns>The timing in milisecounds for the fps</returns>
        public static int GetIntervalFromFpsInMs() {
            return SecInMs / _fps;
        }

        private static int GetNumberOfFramesByTimeAndFps(int durationInMs, int fps) {
            return (int)Math.Floor((double)(durationInMs * fps) / SecInMs);
        }



        #endregion



        #region cycle a color 

        public static void CycleColorLighting(Func<Color, Boolean> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, Control uiThreadControl) {
            CycleColorLightingWithTimerWithFunction(onCycle, cycleMax, cycleMin, increase, startColor, new SmartUiTimer(uiThreadControl));
        }

        public static void CycleColorLighting(Func<Color, Boolean> onCycle, float cycleMax, float cycleMin, float increase, Color startColor) {
            CycleColorLightingWithTimerWithFunction(onCycle, cycleMax, cycleMin, increase, startColor, new SmartTimer());
        }

        private static void CycleColorLightingWithTimerWithFunction(Func<Color, Boolean> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, SmartTimer st) {
            st.Repeate = true;
            st.Interval = GetIntervalFromFpsInMs();
            var currentVal = cycleMin;
            var isGoingUp = false;
            st.Start((object sender, ElapsedEventArgs args, SmartTimer timer) => {

                HandlecycleColorLightingValue(cycleMax, cycleMin, increase, ref currentVal, ref isGoingUp);
                if (onCycle(ControlPaint.Light(startColor, currentVal)) == false) {
                    timer.Stop();
                }
            }, null);
        }


        public static void CycleColorLighting(int cycleDurationInMs, Action<Color> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, Action after = null) {
            CycleColorLightingWithTimerWithAction(onCycle, cycleMax, cycleMin, increase, startColor,
                new SmartTimer() { Interval = GetIntervalFromFpsInMs(), Counter = GetNumberOfFramesByTimeAndFps(cycleDurationInMs, _fps) }, after);
        }

        private static void CycleColorLightingWithTimerWithAction(Action<Color> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, SmartTimer st, Action after = null) {
            var currentVal = cycleMin;
            var isGoingUp = false;
            st.Start((object sender, ElapsedEventArgs args, SmartTimer timer) => {
                HandlecycleColorLightingValue(cycleMax, cycleMin, increase, ref currentVal, ref isGoingUp);
                onCycle(ControlPaint.Light(startColor, currentVal));
            }, after);
        }

        public static void CycleColorLighting(int cycleDurationInMs, Action<Color> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, Control uiThreadControl, Action after = null) {
            CycleColorLightingWithTimerWithAction(onCycle, cycleMax, cycleMin, increase, startColor,
                new SmartUiTimer(uiThreadControl) { Interval = GetIntervalFromFpsInMs(), Counter = GetNumberOfFramesByTimeAndFps(cycleDurationInMs, _fps) }, after);
        }


        private static void HandlecycleColorLightingValue(float cycleMax, float cycleMin, float increase, ref float currentVal, ref bool isGoingUp) {
            if (isGoingUp) {
                isGoingUp = (currentVal + increase) < cycleMax;
                currentVal += increase;
            } else {
                isGoingUp = (currentVal - increase) < cycleMin;
                currentVal -= increase;
            }
        }



        #endregion
        #region highligh 


        public static HighlightOverlay Highlight(Control controle, float maxOverlayOpacity = 1.0f, int overlayEffectTimeInMs = 250, Brush overrideBackBrush = null) {
            var hadFocus = controle.Focused;
            var overlay = new HighlightOverlay() {
                StartPosition = FormStartPosition.Manual,
                Location = controle.PointToScreen(Point.Empty)
            };
            overlay.ShowInactiveTopmost();
            overlay.Size = controle.Size;
            if (overrideBackBrush != null) {
                overlay.SetBackgroundBrush(overrideBackBrush);
            }
            controle.FindForm().Move += (object sender, EventArgs e) => { overlay.Location = controle.PointToScreen(Point.Empty); };
            overlay.FadeIn(overlayEffectTimeInMs, null, maxOverlayOpacity);
            overlay.Size = controle.Size;
            if (hadFocus) {
                controle.Focus();
            }
            controle.MouseClick += (object sender, MouseEventArgs e) => { controle.Focus(); };
            controle.MouseLeave += (object sender, EventArgs e) => { if (overlay.Disposing || overlay.IsDisposed) { return; } overlay.FadeOut(overlayEffectTimeInMs, () => { overlay.Dispose(); }); };
            return overlay;
        }
        #endregion
    }
}
