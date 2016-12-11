using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharedFunctionalities.forms {
    public partial class HighlightOverlay : BasePassthough {

        private Brush _backgroundBrush;


        public HighlightOverlay() {
            InitializeComponent();
        }


        ~HighlightOverlay() {
            if ( _backgroundBrush != null ) {
                _backgroundBrush.Dispose();
            }
            Dispose(false);
        }
        
        protected override void OnPaint( PaintEventArgs e ) {
            //base.OnPaint( e );
            if ( _backgroundBrush == null ) {
                var toUse = new LinearGradientBrush( ClientRectangle, Color.FromArgb( 255, 28, 28, 28 ), Color.FromArgb( 89, 89, 89 ), 90f, true );
                toUse.SetBlendTriangularShape( 0.8f, 1.0f );
                //toUse.SetBlendTriangularShape( 1f);
                _backgroundBrush = toUse;
            }
            e.Graphics.FillRectangle( _backgroundBrush, ClientRectangle );
        }

        public void SetBackgroundBrush( Brush overrideBackBrush ) {
            if ( _backgroundBrush != null ) {
                _backgroundBrush.Dispose();
            }
            _backgroundBrush = overrideBackBrush;
        }


    }
}
