using SharedFunctionalities;
using System;
using System.Timers;

using System.Windows.Forms;
using System.Management;
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
        private static int getCPUSpeedInMhz() {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey( @"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree );
            if ( processor_name != null && processor_name.GetValue( "ProcessorNameString" ) != null ) {
                return (int)processor_name.GetValue( "~MHz" );
            } else {
                return 1500;
            }
        }
        #endregion
        #region constants & static variables
        private const int secInMS = 1000;

        private static int fps = 45;

        static SharedAnimations() {
            var speed = getCPUSpeedInMhz();
            if ( speed >= 2000 ) { //2 ghz
                SetHighQuality();
            } else if ( speed > 1500 ) {
                setMediumQuality();
            } else {
                setLowQuality();
            }
        }


        #endregion
        #region quality setters
        /// <summary>
        /// WARNING requires a lot of processing power (120 FPS)
        /// </summary>
        public static void setUltraQuality() {
            fps = 120;
        }

        /// <summary>
        /// Looks good, 60 fps for the win
        /// </summary>
        public static void SetHighQuality() {
            fps = 60;
        }

        /// <summary>
        /// Very smooth, almost not choppy (but might occur).
        /// </summary>
        public static void setMediumQuality() {
            fps = 45;
        }
        /// <summary>
        /// will be a bit choppy
        /// </summary>
        public static void setLowQuality() {
            fps = 25;
        }

        public static void setQuality( int fps ) {
            SharedAnimations.fps = fps;
        }
        #endregion
        #region fade effects 
        public static void fadeIn( Form f, int displayTimeInMs, Action after, float maxVal = 1f, float startVal = 0f ) {
            if ( f.IsHandleCreated ) {
                int numOfSteps = getNumberOfFramesByTimeAndFps( displayTimeInMs, fps );
                f.BeginInvoke( (MethodInvoker)(() => { f.Opacity = startVal; }) );
                float difference = maxVal / (float)numOfSteps;
                startTimer( displayTimeInMs, () => {
                    f.Opacity += difference;
                }, after, f );
            }
        }

        public static void fadeOut( Form f, int displayTimeInMs, Action after, float minVal = 1f, float startVal = 0f ) {
            if ( f.IsHandleCreated ) {
                if ( startVal <= 0f ) {
                    startVal = (float)f.Opacity;
                }
                f.BeginInvoke( (MethodInvoker)(() => { f.Opacity = startVal; }) );
                int numOfSteps = getNumberOfFramesByTimeAndFps( displayTimeInMs, fps );
                double diff = minVal / (double)numOfSteps;
                startTimer( displayTimeInMs, () => {
                    f.Opacity -= diff;
                }, after, f );
            }
        }

        #endregion
        #region internal calculation
        private static void startTimer( int displayTimeInMs, Action handler, Action after, Control con ) {
            int numOfSteps = getNumberOfFramesByTimeAndFps( displayTimeInMs, fps );
            SmartUITimer timer = new SmartUITimer( con ) { repeate = true, interval = getIntervalFromFpsInMs(), counter = numOfSteps };
            timer.start( ( object sender, ElapsedEventArgs e, SmartTimer st ) => { handler.Invoke(); }, after );
        }
        /// <summary>
        /// calculates the duration in time from the fps value.
        /// </summary>
        /// <returns>The timing in milisecounds for the fps</returns>
        public static int getIntervalFromFpsInMs() {
            return secInMS / fps;
        }

        private static int getNumberOfFramesByTimeAndFps( int durationInMs, int fps ) {
            return (int)Math.Ceiling( (double)(durationInMs * fps) / secInMS );
        }



        #endregion



        #region cycle a color 

        public static void cycleColorLighting( Func<Color, Boolean> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, Control uiThreadControl ) {
            cycleColorLightingWithTimerWithFunction( onCycle, cycleMax, cycleMin, increase, startColor, new SmartUITimer( uiThreadControl ) );
        }

        public static void cycleColorLighting( Func<Color, Boolean> onCycle, float cycleMax, float cycleMin, float increase, Color startColor ) {
            cycleColorLightingWithTimerWithFunction( onCycle, cycleMax, cycleMin, increase, startColor, new SmartTimer() );
        }

        private static void cycleColorLightingWithTimerWithFunction( Func<Color, Boolean> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, SmartTimer st ) {
            st.repeate = true;
            st.interval = getIntervalFromFpsInMs();
            float currentVal = cycleMin;
            bool isGoingUp = false;
            st.start( ( object sender, ElapsedEventArgs args, SmartTimer timer ) => {

                handlecycleColorLightingValue( cycleMax, cycleMin, increase, ref currentVal, ref isGoingUp );
                if ( onCycle( ControlPaint.Light( startColor, currentVal ) ) == false ) {
                    timer.stop();
                }
            }, null );
        }


        public static void cycleColorLighting( int cycleDurationInMs, Action<Color> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, Action after = null ) {
            cycleColorLightingWithTimerWithAction( onCycle, cycleMax, cycleMin, increase, startColor,
                new SmartTimer() { interval = getIntervalFromFpsInMs(), counter = getNumberOfFramesByTimeAndFps( cycleDurationInMs, fps ) }, after );
        }

        private static void cycleColorLightingWithTimerWithAction( Action<Color> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, SmartTimer st, Action after = null ) {
            float currentVal = cycleMin;
            bool isGoingUp = false;
            st.start( ( object sender, ElapsedEventArgs args, SmartTimer timer ) => {
                handlecycleColorLightingValue( cycleMax, cycleMin, increase, ref currentVal, ref isGoingUp );
                onCycle( ControlPaint.Light( startColor, currentVal ) );
            }, after );
        }

        public static void cycleColorLighting( int cycleDurationInMs, Action<Color> onCycle, float cycleMax, float cycleMin, float increase, Color startColor, Control uiThreadControl, Action after = null ) {
            cycleColorLightingWithTimerWithAction( onCycle, cycleMax, cycleMin, increase, startColor,
                new SmartUITimer( uiThreadControl ) { interval = getIntervalFromFpsInMs(), counter = getNumberOfFramesByTimeAndFps( cycleDurationInMs, fps ) }, after );
        }


        private static void handlecycleColorLightingValue( float cycleMax, float cycleMin, float increase, ref float currentVal, ref bool isGoingUp ) {
            if ( isGoingUp ) {
                isGoingUp = (currentVal + increase) < cycleMax;
                currentVal += increase;
            } else {
                isGoingUp = (currentVal - increase) < cycleMin;
                currentVal -= increase;
            }
        }



        #endregion
        #region highligh 


        public static HighlightOverlay Highlight( Control controle, float maxOverlayOpacity = 1.0f, int overlayEffectTimeInMs = 250, Brush overrideBackBrush = null ) {
            var hadFocus = controle.Focused;
            var overlay = new forms.HighlightOverlay();
            overlay.StartPosition = FormStartPosition.Manual;
            overlay.Location = controle.PointToScreen( Point.Empty );
            overlay.ShowInactiveTopmost();
            overlay.Size = controle.Size;
            if ( overrideBackBrush != null ) {
                overlay.setBackgroundBrush( overrideBackBrush );
            }
            controle.FindForm().Move += ( object sender, EventArgs e ) => { overlay.Location = controle.PointToScreen( Point.Empty ); };
            overlay.FadeIn( overlayEffectTimeInMs, null, maxOverlayOpacity );
            overlay.Size = controle.Size;
            if ( hadFocus ) {
                controle.Focus();
            }
            controle.MouseClick += ( object sender, MouseEventArgs e ) => { controle.Focus(); };
            controle.MouseLeave += ( object sender, EventArgs e ) => { if ( overlay.Disposing || overlay.IsDisposed ) { return; } overlay.FadeOut( overlayEffectTimeInMs, () => { overlay.Dispose(); } ); };
            return overlay;
        }
        #endregion
    }
}
