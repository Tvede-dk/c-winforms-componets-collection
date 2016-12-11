using System;
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

        const int MovingPixlesFrame = 2; //3 becomes "laggy, 1 and 2 at a time, looks equal. so its more effective to go with 2 pixels (half the work).

        #endregion

        #region instance variables( not shared).
        private IDrawMethod _backgroundRender;
        private readonly IDrawMethod _textRender;
        private readonly IDrawMethod _flashBarRender;


        #endregion

        #region property flash bar width
        private int _flashbarWidth = 16;
        public int FlashbarWidth {
            get { return _flashbarWidth; }
            set { _flashbarWidth = value; }
        }
        #endregion



        #region property flashColor
        private readonly List<Action<Color>> _flashColorChangeListners = new List<Action<Color>>();

        private Color _flashColor;
        [EditorBrowsable]
        [Description("The flash bar's color. the alpha is the intensity.")]
        public Color FlashColor {
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
        public int Fps {
            get { return _fps; }
            set {
                _fps = value;
                _animationTimer.Interval = 1000 / Fps;
            }
        }
        #endregion

        #region property borderSize
        private int _borderSize = 3;

        [EditorBrowsable]
        [Description("the side of the inner border")]
        public int BorderSize {
            get { return _borderSize; }
            set { _borderSize = value; }
        }
        #endregion

        #region property drawOverlay
        private bool _drawOverlay;

        [EditorBrowsable]
        [Description("Any piece of text that comes(if activated) after the percent / value.")]
        public bool DrawOverlay {
            get { return _drawOverlay; }
            set { _drawOverlay = value; }
        }
        #endregion

        #region property drawProcent
        private bool _drawProcent;
        [EditorBrowsable]
        [Description("true if we should draw the number (It does not contain the %)")]
        public bool DrawProcent {
            get { return _drawProcent; }
            set { _drawProcent = value; }
        }
        #endregion

        #region property progressInProcent
        private int _progressInProcent = 50;
        [EditorBrowsable]
        [Description("the progress in %.")]
        public int ProgressInProcent {
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
        private readonly List<Action<Color>> _backColorListners = new List<Action<Color>>();

        protected override void OnBackColorChanged( EventArgs e ) {
            foreach ( var item in _backColorListners ) {
                item( BackColor );
            }
            base.OnBackColorChanged( e );
        }

        #endregion


        #region property OnForeColorChanged
        private readonly List<Action<Color>> _foreColorListners = new List<Action<Color>>();

        protected override void OnForeColorChanged( EventArgs e ) {
            foreach ( var item in _foreColorListners ) {
                item( ForeColor );
            }
            base.OnForeColorChanged( e );
        }

        #endregion


        #region property multiColorStart
        private readonly List<Action<Color>> _multiColorStartChangeListners = new List<Action<Color>>();

        private Color _multiColorStart = Color.FromArgb( 255, 0, 255, 255 );
        [EditorBrowsable]
        [Description("In the multi color mode, this is the start color")]
        public Color MultiColorStart {
            get { return _multiColorStart; }
            set {
                _multiColorStart = value;
                foreach ( var item in _multiColorStartChangeListners ) { item( value ); }
            }
        }
        #endregion

        #region property multiColorEnd
        private readonly List<Action<Color>> _multiColorEndChangeListners = new List<Action<Color>>();

        private Color _multiColorEnd = Color.FromArgb( 255, 29, 128, 193 );
        [EditorBrowsable]
        [Description("In the multi color mode, this is the end color")]
        public Color MultiColorEnd {
            get { return _multiColorEnd; }
            set {
                _multiColorEnd = value;
                foreach ( var item in _multiColorEndChangeListners ) { item( value ); }
            }
        }
        #endregion

        #region property singleColorFilledColor
        private readonly List<Action<Color>> _singleColorFilledColorChangeListners = new List<Action<Color>>();

        private Color _singleColorFilledColor;
        [EditorBrowsable]
        [Description("If only a single color is used, this is the fill color")]
        public Color SingleColorFilledColor {
            get { return _singleColorFilledColor; }
            set {
                _singleColorFilledColor = value;
                foreach ( var item in _singleColorFilledColorChangeListners ) { item( value ); }
            }
        }
        #endregion


        public Progressbar() {
            InitializeComponent();
            _animationTimer.Interval = 1000 / Fps;
            SetStyle( ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true );
            _backgroundRender = new DrawSingleColorStrategy( this );
            _textRender = new TextRender( this );
            _flashBarRender = new FlashAnimator( this );
            this.DoubleBuffered = true;
            StartAnimations();
            _drawH.AddLayer( _backgroundRender );
            _drawH.AddLayer( _textRender );
            _drawH.AddLayer( _flashBarRender );
        }





        #region animation



        private bool _disableAnimations = false;

        [EditorBrowsable]
        [Description("Controls if we should disable animations")]
        public bool DisableAnimations {
            get { return _disableAnimations; }
            set {
                _disableAnimations = value;
                if ( value ) {
                    _animationTimer.Stop();
                } else {
                    StartAnimations();
                }
            }
        }



        private readonly Timer _animationTimer = new Timer();

        private void StartAnimations() {
            if ( DisableAnimations == false ) {
                _animationTimer.Enabled = true;
                _animationTimer.Start();
                _animationTimer.Tick += animation_Run;
            }
        }


        private int _timerCount = 0;
        private void animation_Run( object sender, EventArgs e ) {
            _timerCount++;
            if ( (_timerCount * MovingPixlesFrame) - FlashbarWidth >= (ClientRectangle.Width * ProgressInProcent / 100) ) {
                _timerCount = 0;
            }
            var maxInvalidTo = (int)(ClientRectangle.Width * ((float)Math.Max( 60, ProgressInProcent ) / 100f));
            var maxInvalid = new Rectangle( BorderSize, BorderSize, maxInvalidTo, Height - (BorderSize * 2) );
            this.Invalidate( maxInvalid );
        }
        private string _overlayText;
        #endregion

        #region property overlayText


        [EditorBrowsable]
        [Description("")]
        public string OverlayText {
            get {
                return _overlayText;
            }
            set {
                this._overlayText = value;
            }
        }
        #endregion




        #region property flashColorIntensity
        private readonly List<Action<int>> _flashColorIntensityChangeListners = new List<Action<int>>();

        private int _flashColorIntensity = 70;
        [EditorBrowsable]
        [Description("")]
        public int FlashColorIntensity {
            get { return _flashColorIntensity; }
            set {
                _flashColorIntensity = value;
                foreach ( var item in _flashColorIntensityChangeListners ) { item( value ); }
            }
        }
        #endregion


        #region property colorMethod
        private ColorDrawing _colorMethod = ColorDrawing.SingleColor;

        [EditorBrowsable]
        [Description("")]
        public ColorDrawing ColorMethod {
            get {
                return this._colorMethod;
            }
            set {
                if ( value == ColorDrawing.SingleColor ) {
                    _backgroundRender = new DrawSingleColorStrategy( this );
                } else if ( value == ColorDrawing.GradientLeftRight ) {
                    _backgroundRender = new DrawMultiColorStrategy( this );
                }
                this._colorMethod = value;
            }
        }
        #endregion

        readonly DrawingHandler _drawH = new DrawingHandler();

        #region paint methods
        protected override void OnPaint( PaintEventArgs e ) {
            base.OnPaint( e );
            //backgroundRender.draw( e, this );
            //textRender.draw( e, this );
            //if ( disableAnimations == false ) {
            //    FlashBarRender.draw( e, this );
            //}
            _drawH.Draw( e.Graphics, ClientRectangle, e.ClipRectangle );

        }

        protected override void OnPaintBackground( PaintEventArgs pevent ) {
            base.OnPaintBackground( pevent );
            pevent.Graphics.FillRectangle( new SolidBrush( BackColor ), pevent.ClipRectangle );
        }


        private Rectangle CalculateFilledPart() {
            var procent = ProgressInProcent;
            var endOfDrawing = ((ClientRectangle.Width * procent) / 100f);
            Rectangle inner = ClientRectangle;
            inner.X = (int)BorderSize;
            inner.Width = (int)endOfDrawing - (int)(BorderSize * 2);
            inner.Y = (int)BorderSize;
            inner.Height -= inner.Y * 2;
            return inner;
        }

        #endregion

        #region internal implementations

        public enum ColorDrawing {
            SingleColor,
            GradientLeftRight
        }

        public class DrawSingleColorStrategy : IDrawMethod {
            private Brush _drawingBrush;
            private Brush _backColorBrush;
            private readonly Progressbar _prog;
            private bool _isCacheValid = false;
            public DrawSingleColorStrategy( Progressbar prog ) {
                prog._singleColorFilledColorChangeListners.Add( OnNewColor );
                OnNewColor( prog.SingleColorFilledColor );
                prog._backColorListners.Add( OnNewBackColor );
                OnNewBackColor( prog.BackColor );
                this._prog = prog;
            }

            public void OnNewColor( Color col ) {
                _drawingBrush = new SolidBrush( col );
                _isCacheValid = false;
            }

            public void OnNewBackColor( Color col ) {
                _backColorBrush = new SolidBrush( col );
                _isCacheValid = false;
            }

            //public void draw( PaintEventArgs e, Progressbar prog ) {
            //    e.Graphics.FillRectangle( backColorBrush, prog.ClientRectangle );
            //    e.Graphics.FillRectangle( drawingBrush, prog.calculateFilledPart() );
            //}

            public void Draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                g.FillRectangle( _backColorBrush, _prog.ClientRectangle );
                g.FillRectangle( _drawingBrush, _prog.CalculateFilledPart() );
                _isCacheValid = true;
            }

            public bool IsTransperant() {
                return true;
            }

            public bool WillFillRectangleOut() {
                return true;
            }

            public bool MayCacheLayer() {
                return true;
            }

            public bool IsCacheInvalid() {
                return !_isCacheValid;
            }

            public bool MayDraw() {
                return true;

            }
            public void ModifySize( ref Rectangle newSize ) {
                return;
            }
        }
        public class DrawMultiColorStrategy : IDrawMethod {
            private readonly Progressbar _prog;
            private Brush _backColor;
            public DrawMultiColorStrategy( Progressbar prog ) {
                prog._backColorListners.Add( OnNewColor );
                OnNewColor( prog.BackColor );
                this._prog = prog;
            }

            public void OnNewColor( Color col ) {
                _backColor = new SolidBrush( col );
            }


            public void Draw( PaintEventArgs e, Progressbar prog ) {
                Rectangle rf = prog.CalculateFilledPart();
                //determine if we can offload this calculation as well.
                var linGrBrush = new LinearGradientBrush(
                  new Point( 0, 0 ),
                   new Point( (int)rf.Width + rf.X, 0 ),
                      prog.MultiColorStart
                    , prog.MultiColorEnd );

                e.Graphics.FillRectangle( _backColor, prog.ClientRectangle );
                e.Graphics.FillRectangle( linGrBrush, rf );
            }

            public void Draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                Rectangle rf = _prog.CalculateFilledPart();
                //determine if we can offload this calculation as well.
                var linGrBrush = new LinearGradientBrush(
                  new Point( 0, 0 ),
                   new Point( (int)rf.Width + rf.X, 0 ),
                      _prog.MultiColorStart
                    , _prog.MultiColorEnd );

                g.FillRectangle( _backColor, _prog.ClientRectangle );
                g.FillRectangle( linGrBrush, rf );
            }

            public bool IsTransperant() {
                return true;
            }

            public bool WillFillRectangleOut() {
                return true;
            }

            public bool MayCacheLayer() {
                return true;
            }

            public bool IsCacheInvalid() {
                return false;
            }

            public bool MayDraw() {
                return true;
            }
            public void ModifySize( ref Rectangle newSize ) {
                return;
            }
        }

        public class TextRender : IDrawMethod {
            private bool _isCacheValid = false;
            private Brush _textColorBrush;
            private readonly StringFormat _format;
            private readonly Progressbar _prog;
            public TextRender( Progressbar prog ) {
                prog._foreColorListners.Add( OnNewColor );
                OnNewColor( prog.ForeColor );
                _format = new StringFormat();
                _format.LineAlignment = StringAlignment.Center;
                _format.Alignment = StringAlignment.Center;
                this._prog = prog;
            }

            public void OnNewColor( Color col ) {
                _textColorBrush = new SolidBrush( col );
                _isCacheValid = false;
            }

            public bool IsTransperant() {
                return true;
            }

            public bool WillFillRectangleOut() {
                return true;
            }

            public bool MayCacheLayer() {
                return true;
            }

            public bool IsCacheInvalid() {
                return !_isCacheValid;
            }

            public void Draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                var toDislpay = "";
                if ( _prog.DrawProcent ) {
                    toDislpay += _prog.ProgressInProcent;
                }
                if ( _prog.DrawOverlay ) {
                    toDislpay += _prog.OverlayText;
                }
                g.DrawString( toDislpay, _prog.Font, _textColorBrush, _prog.ClientRectangle, _format );
                _isCacheValid = true;
            }

            public bool MayDraw() {
                return true;
            }

            public void ModifySize( ref Rectangle newSize ) {
                return;
            }
        }

        public class FlashAnimator : IDrawMethod {

            private SolidBrush _highlighterBrush;
            private int _colorIntensity = 70;
            private readonly Progressbar _prog;
            public bool ShouldDraw = true;

            public FlashAnimator( Progressbar prog ) {
                prog._flashColorChangeListners.Add( OnNewColor );
                prog._flashColorIntensityChangeListners.Add( OnNewIntensity );
                this._colorIntensity = prog.FlashColorIntensity;
                OnNewColor( prog.FlashColor );
                this._prog = prog;
            }

            public void OnNewIntensity( int val ) {
                _colorIntensity = val;
                var col = _highlighterBrush.Color;
                var newColor = Color.FromArgb( _colorIntensity, col.R, col.G, col.B );
                _highlighterBrush = new SolidBrush( newColor );
            }
            public void OnNewColor( Color col ) {
                var newColor = Color.FromArgb( _colorIntensity, col.R, col.G, col.B );
                _highlighterBrush = new SolidBrush( newColor );
            }

            public void Draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                Rectangle inner = _prog.CalculateFilledPart();
                inner.Height -= 4;
                inner.Y += 2; //it looks so much more cool.. 
                var timerVal = _prog._timerCount;
                Rectangle high = inner;
                high.Width = _prog.FlashbarWidth;

                var end = inner.Width + inner.X;

                high.X = ((timerVal * MovingPixlesFrame) - _prog.FlashbarWidth);
                if ( high.X + _prog.FlashbarWidth > end ) {
                    high.Width = end - high.X;
                } else if ( high.X < _prog.BorderSize ) {
                    high.Width = (timerVal * MovingPixlesFrame) - (_prog.BorderSize);
                    high.X = _prog.BorderSize;
                }
                g.FillRectangle( _highlighterBrush, high );
            }

            public bool IsTransperant() {
                return true;
            }

            public bool WillFillRectangleOut() {
                return false;
            }

            public bool MayCacheLayer() {
                return true;
            }

            public bool IsCacheInvalid() {
                return true;
            }

            public bool MayDraw() {
                return ShouldDraw;
            }
            public void ModifySize( ref Rectangle newSize ) {
                return;
            }

        }

        #endregion
    }
}