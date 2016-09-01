﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SharedFunctionalities.drawing;

namespace winforms_collection.simple_components {

    /// <summary>
    /// A simple, yet very powerfull progressbar.
    /// with the possiblity to implement the whole drawing logic outside as well.
    /// </summary>
    public partial class Progressbar : Control {

        #region constants

        const int MOVING_PIXLES_FRAME = 2; //3 becomes "laggy, 1 and 2 at a time, looks equal. so its more effective to go with 2 pixels (half the work).

        #endregion

        #region instance variables( not shared).
        private IDrawMethod backgroundRender;
        private IDrawMethod textRender;
        private IDrawMethod FlashBarRender;


        #endregion

        #region property flash bar width
        private int _flashbarWidth = 16;
        public int flashbarWidth {
            get { return _flashbarWidth; }
            set { _flashbarWidth = value; }
        }
        #endregion



        #region property flashColor
        private List<Action<Color>> _flashColorChangeListners = new List<Action<Color>>();

        private Color _flashColor;
        [EditorBrowsable]
        [Description("The flash bar's color. the alpha is the intensity.")]
        public Color flashColor {
            get { return _flashColor; }
            set {
                _flashColor = value;
                foreach ( var item in _flashColorChangeListners ) { item( value ); }
            }
        }
        #endregion

        #region property fps
        private int _fps = 40;
        [EditorBrowsable]
        [Description("The Frames per seconds. use this to speed up or slow down the flash bar. However keep it as low as possible.")]
        public int fps {
            get { return _fps; }
            set {
                _fps = value;
                animationTimer.Interval = 1000 / fps;
            }
        }
        #endregion

        #region property borderSize
        private int _borderSize = 3;

        [EditorBrowsable]
        [Description("the side of the inner border")]
        public int borderSize {
            get { return _borderSize; }
            set { _borderSize = value; }
        }
        #endregion

        #region property drawOverlay
        private bool _drawOverlay;

        [EditorBrowsable]
        [Description("Any piece of text that comes(if activated) after the percent / value.")]
        public bool drawOverlay {
            get { return _drawOverlay; }
            set { _drawOverlay = value; }
        }
        #endregion

        #region property drawProcent
        private bool _drawProcent;
        [EditorBrowsable]
        [Description("true if we should draw the number (It does not contain the %)")]
        public bool drawProcent {
            get { return _drawProcent; }
            set { _drawProcent = value; }
        }
        #endregion

        #region property progressInProcent
        private int _progressInProcent = 50;
        [EditorBrowsable]
        [Description("the progress in %.")]
        public int progressInProcent {
            get { return _progressInProcent; }
            set {
                if ( value > 100 || value < 0 ) {
                    return;
                }
                _progressInProcent = value;
            }
        }
        #endregion

        #region property OnBackColorChanged
        private List<Action<Color>> _backColorListners = new List<Action<Color>>();

        protected override void OnBackColorChanged( EventArgs e ) {
            foreach ( var item in _backColorListners ) {
                item( BackColor );
            }
            base.OnBackColorChanged( e );
        }

        #endregion


        #region property OnForeColorChanged
        private List<Action<Color>> _foreColorListners = new List<Action<Color>>();

        protected override void OnForeColorChanged( EventArgs e ) {
            foreach ( var item in _foreColorListners ) {
                item( ForeColor );
            }
            base.OnForeColorChanged( e );
        }

        #endregion


        #region property multiColorStart
        private List<Action<Color>> _multiColorStartChangeListners = new List<Action<Color>>();

        private Color _multiColorStart = Color.FromArgb( 255, 0, 255, 255 );
        [EditorBrowsable]
        [Description("In the multi color mode, this is the start color")]
        public Color multiColorStart {
            get { return _multiColorStart; }
            set {
                _multiColorStart = value;
                foreach ( var item in _multiColorStartChangeListners ) { item( value ); }
            }
        }
        #endregion

        #region property multiColorEnd
        private List<Action<Color>> _multiColorEndChangeListners = new List<Action<Color>>();

        private Color _multiColorEnd = Color.FromArgb( 255, 29, 128, 193 );
        [EditorBrowsable]
        [Description("In the multi color mode, this is the end color")]
        public Color multiColorEnd {
            get { return _multiColorEnd; }
            set {
                _multiColorEnd = value;
                foreach ( var item in _multiColorEndChangeListners ) { item( value ); }
            }
        }
        #endregion

        #region property singleColorFilledColor
        private List<Action<Color>> _singleColorFilledColorChangeListners = new List<Action<Color>>();

        private Color _singleColorFilledColor;
        [EditorBrowsable]
        [Description("If only a single color is used, this is the fill color")]
        public Color singleColorFilledColor {
            get { return _singleColorFilledColor; }
            set {
                _singleColorFilledColor = value;
                foreach ( var item in _singleColorFilledColorChangeListners ) { item( value ); }
            }
        }
        #endregion


        public Progressbar() {
            InitializeComponent();
            animationTimer.Interval = 1000 / fps;
            SetStyle( ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true );
            backgroundRender = new DrawSingleColorStrategy( this );
            textRender = new TextRender( this );
            FlashBarRender = new FlashAnimator( this );
            this.DoubleBuffered = true;
            startAnimations();
            drawH.addLayer( backgroundRender );
            drawH.addLayer( textRender );
            drawH.addLayer( FlashBarRender );
        }





        #region animation



        private bool _disableAnimations = false;

        [EditorBrowsable]
        [Description("Controls if we should disable animations")]
        public bool disableAnimations {
            get { return _disableAnimations; }
            set {
                _disableAnimations = value;
                if ( value ) {
                    animationTimer.Stop();
                } else {
                    startAnimations();
                }
            }
        }



        private Timer animationTimer = new Timer();

        private void startAnimations() {
            if ( disableAnimations == false ) {
                animationTimer.Enabled = true;
                animationTimer.Start();
                animationTimer.Tick += animation_Run;
            }
        }


        private int timerCount = 0;
        private void animation_Run( object sender, EventArgs e ) {
            timerCount++;
            if ( (timerCount * MOVING_PIXLES_FRAME) - flashbarWidth >= (ClientRectangle.Width * progressInProcent / 100) ) {
                timerCount = 0;
            }
            var maxInvalidTo = (int)(ClientRectangle.Width * ((float)Math.Max( 60, progressInProcent ) / 100f));
            var maxInvalid = new Rectangle( borderSize, borderSize, maxInvalidTo, Height - (borderSize * 2) );
            this.Invalidate( maxInvalid );
        }
        private string _overlayText;
        #endregion

        #region property overlayText


        [EditorBrowsable]
        [Description("")]
        public string overlayText {
            get {
                return _overlayText;
            }
            set {
                this._overlayText = value;
            }
        }
        #endregion




        #region property flashColorIntensity
        private List<Action<int>> _flashColorIntensityChangeListners = new List<Action<int>>();

        private int _flashColorIntensity = 70;
        [EditorBrowsable]
        [Description("")]
        public int flashColorIntensity {
            get { return _flashColorIntensity; }
            set {
                _flashColorIntensity = value;
                foreach ( var item in _flashColorIntensityChangeListners ) { item( value ); }
            }
        }
        #endregion


        #region property colorMethod
        private ColorDrawing _colorMethod = ColorDrawing.SINGLE_COLOR;

        [EditorBrowsable]
        [Description("")]
        public ColorDrawing colorMethod {
            get {
                return this._colorMethod;
            }
            set {
                if ( value == ColorDrawing.SINGLE_COLOR ) {
                    backgroundRender = new DrawSingleColorStrategy( this );
                } else if ( value == ColorDrawing.GRADIENT_LEFT_RIGHT ) {
                    backgroundRender = new DrawMultiColorStrategy( this );
                }
                this._colorMethod = value;
            }
        }
        #endregion
        DrawingHandler drawH = new DrawingHandler();

        #region paint methods
        protected override void OnPaint( PaintEventArgs e ) {
            base.OnPaint( e );
            //backgroundRender.draw( e, this );
            //textRender.draw( e, this );
            //if ( disableAnimations == false ) {
            //    FlashBarRender.draw( e, this );
            //}
            drawH.draw( e.Graphics, ClientRectangle, e.ClipRectangle );

        }

        protected override void OnPaintBackground( PaintEventArgs pevent ) {
            base.OnPaintBackground( pevent );
            pevent.Graphics.FillRectangle( new SolidBrush( BackColor ), pevent.ClipRectangle );
        }


        private Rectangle calculateFilledPart() {
            var procent = progressInProcent;
            var endOfDrawing = ((ClientRectangle.Width * procent) / 100f);
            Rectangle inner = ClientRectangle;
            inner.X = (int)borderSize;
            inner.Width = (int)endOfDrawing - (int)(borderSize * 2);
            inner.Y = (int)borderSize;
            inner.Height -= inner.Y * 2;
            return inner;
        }

        #endregion

        #region internal implementations

        public enum ColorDrawing {
            SINGLE_COLOR,
            GRADIENT_LEFT_RIGHT
        }

        public class DrawSingleColorStrategy : IDrawMethod {
            private Brush drawingBrush;
            private Brush backColorBrush;
            private Progressbar prog;
            private bool isCacheValid = false;
            public DrawSingleColorStrategy( Progressbar prog ) {
                prog._singleColorFilledColorChangeListners.Add( onNewColor );
                onNewColor( prog.singleColorFilledColor );
                prog._backColorListners.Add( onNewBackColor );
                onNewBackColor( prog.BackColor );
                this.prog = prog;
            }

            public void onNewColor( Color col ) {
                drawingBrush = new SolidBrush( col );
                isCacheValid = false;
            }

            public void onNewBackColor( Color col ) {
                backColorBrush = new SolidBrush( col );
                isCacheValid = false;
            }

            //public void draw( PaintEventArgs e, Progressbar prog ) {
            //    e.Graphics.FillRectangle( backColorBrush, prog.ClientRectangle );
            //    e.Graphics.FillRectangle( drawingBrush, prog.calculateFilledPart() );
            //}

            public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                g.FillRectangle( backColorBrush, prog.ClientRectangle );
                g.FillRectangle( drawingBrush, prog.calculateFilledPart() );
                isCacheValid = true;
            }

            public bool isTransperant() {
                return true;
            }

            public bool willFillRectangleOut() {
                return true;
            }

            public bool mayCacheLayer() {
                return true;
            }

            public bool isCacheInvalid() {
                return !isCacheValid;
            }

            public bool mayDraw() {
                return true;

            }
            public void modifySize( ref Rectangle newSize ) {
                return;
            }
        }
        public class DrawMultiColorStrategy : IDrawMethod {
            private Progressbar prog;
            private Brush backColor;
            public DrawMultiColorStrategy( Progressbar prog ) {
                prog._backColorListners.Add( onNewColor );
                onNewColor( prog.BackColor );
                this.prog = prog;
            }

            public void onNewColor( Color col ) {
                backColor = new SolidBrush( col );
            }


            public void draw( PaintEventArgs e, Progressbar prog ) {
                Rectangle rf = prog.calculateFilledPart();
                //determine if we can offload this calculation as well.
                var linGrBrush = new LinearGradientBrush(
                  new Point( 0, 0 ),
                   new Point( (int)rf.Width + rf.X, 0 ),
                      prog.multiColorStart
                    , prog.multiColorEnd );

                e.Graphics.FillRectangle( backColor, prog.ClientRectangle );
                e.Graphics.FillRectangle( linGrBrush, rf );
            }

            public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                Rectangle rf = prog.calculateFilledPart();
                //determine if we can offload this calculation as well.
                var linGrBrush = new LinearGradientBrush(
                  new Point( 0, 0 ),
                   new Point( (int)rf.Width + rf.X, 0 ),
                      prog.multiColorStart
                    , prog.multiColorEnd );

                g.FillRectangle( backColor, prog.ClientRectangle );
                g.FillRectangle( linGrBrush, rf );
            }

            public bool isTransperant() {
                return true;
            }

            public bool willFillRectangleOut() {
                return true;
            }

            public bool mayCacheLayer() {
                return true;
            }

            public bool isCacheInvalid() {
                return false;
            }

            public bool mayDraw() {
                return true;
            }
            public void modifySize( ref Rectangle newSize ) {
                return;
            }
        }

        public class TextRender : IDrawMethod {
            private bool isCacheValid = false;
            private Brush textColorBrush;
            private StringFormat format;
            private Progressbar prog;
            public TextRender( Progressbar prog ) {
                prog._foreColorListners.Add( onNewColor );
                onNewColor( prog.ForeColor );
                format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                this.prog = prog;
            }

            public void onNewColor( Color col ) {
                textColorBrush = new SolidBrush( col );
                isCacheValid = false;
            }

            public bool isTransperant() {
                return true;
            }

            public bool willFillRectangleOut() {
                return true;
            }

            public bool mayCacheLayer() {
                return true;
            }

            public bool isCacheInvalid() {
                return !isCacheValid;
            }

            public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                var toDislpay = "";
                if ( prog.drawProcent ) {
                    toDislpay += prog.progressInProcent;
                }
                if ( prog.drawOverlay ) {
                    toDislpay += prog.overlayText;
                }
                g.DrawString( toDislpay, prog.Font, textColorBrush, prog.ClientRectangle, format );
                isCacheValid = true;
            }

            public bool mayDraw() {
                return true;
            }

            public void modifySize( ref Rectangle newSize ) {
                return;
            }
        }

        public class FlashAnimator : IDrawMethod {

            private SolidBrush highlighterBrush;
            private int colorIntensity = 70;
            private Progressbar prog;
            public bool shouldDraw = true;

            public FlashAnimator( Progressbar prog ) {
                prog._flashColorChangeListners.Add( onNewColor );
                prog._flashColorIntensityChangeListners.Add( onNewIntensity );
                this.colorIntensity = prog.flashColorIntensity;
                onNewColor( prog.flashColor );
                this.prog = prog;
            }

            public void onNewIntensity( int val ) {
                colorIntensity = val;
                var col = highlighterBrush.Color;
                var newColor = Color.FromArgb( colorIntensity, col.R, col.G, col.B );
                highlighterBrush = new SolidBrush( newColor );
            }
            public void onNewColor( Color col ) {
                var newColor = Color.FromArgb( colorIntensity, col.R, col.G, col.B );
                highlighterBrush = new SolidBrush( newColor );
            }

            public void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                Rectangle inner = prog.calculateFilledPart();
                inner.Height -= 4;
                inner.Y += 2; //it looks so much more cool.. 
                var timerVal = prog.timerCount;
                Rectangle high = inner;
                high.Width = prog.flashbarWidth;

                var end = inner.Width + inner.X;

                high.X = ((timerVal * MOVING_PIXLES_FRAME) - prog.flashbarWidth);
                if ( high.X + prog.flashbarWidth > end ) {
                    high.Width = end - high.X;
                } else if ( high.X < prog.borderSize ) {
                    high.Width = (timerVal * MOVING_PIXLES_FRAME) - (prog.borderSize);
                    high.X = prog.borderSize;
                }
                g.FillRectangle( highlighterBrush, high );
            }

            public bool isTransperant() {
                return true;
            }

            public bool willFillRectangleOut() {
                return false;
            }

            public bool mayCacheLayer() {
                return true;
            }

            public bool isCacheInvalid() {
                return true;
            }

            public bool mayDraw() {
                return shouldDraw;
            }
            public void modifySize( ref Rectangle newSize ) {
                return;
            }

        }

        #endregion
    }
}