using SharedFunctionalities;
using System;
using System.Timers;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;

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
        public static void fadeIn( Form f, int displayTimeInMs, Action after ) {
            int numOfSteps = getNumberOfFramesByTimeAndFps( displayTimeInMs, fps );
            f.Opacity = 0;
            double difference = 1d / (double)numOfSteps;
            startTimer( displayTimeInMs, () => {
                f.Opacity += difference;

            }, after, f );
        }

        public static void fadeOut( Form f, int displayTimeInMs, Action after ) {
            int numOfSteps = getNumberOfFramesByTimeAndFps( displayTimeInMs, fps );
            double difference = 1d / (double)numOfSteps;
            startTimer( displayTimeInMs, () => {
                f.Opacity -= difference;
            }, after, f );
        }

        #endregion
        private static void startTimer( int displayTimeInMs, Action handler, Action after, Control con ) {
            int numOfSteps = getNumberOfFramesByTimeAndFps( displayTimeInMs, fps );
            SmartUITimer timer = new SmartUITimer( con ) { continous = true, interval = secInMS / fps, counter = numOfSteps };
            timer.start( ( object sender, ElapsedEventArgs e, SmartTimer st ) => { handler.Invoke(); }, after );
        }

        private static int getNumberOfFramesByTimeAndFps( int durationInMs, int fps ) {
            return (int)Math.Ceiling( (double)(durationInMs * fps) / secInMS );
        }

    }
}
