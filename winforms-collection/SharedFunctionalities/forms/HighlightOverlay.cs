using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharedFunctionalities.forms {
    public partial class HighlightOverlay : BasePassthough {

        private Brush backgroundBrush;


        public HighlightOverlay() {
            InitializeComponent();
        }


        ~HighlightOverlay() {
            if ( backgroundBrush != null ) {
                backgroundBrush.Dispose();
            }
        }

        protected override void OnPaint( PaintEventArgs e ) {
            //base.OnPaint( e );
            if ( backgroundBrush == null ) {
                LinearGradientBrush toUse = new LinearGradientBrush( ClientRectangle, Color.FromArgb( 255, 28, 28, 28 ), Color.FromArgb( 89, 89, 89 ), 90f, true );
                toUse.SetBlendTriangularShape( 0.8f, 1.0f );
                //toUse.SetBlendTriangularShape( 1f);
                backgroundBrush = toUse;
            }
            e.Graphics.FillRectangle( backgroundBrush, ClientRectangle );
        }

        public void setBackgroundBrush( Brush overrideBackBrush ) {
            if ( backgroundBrush != null ) {
                backgroundBrush.Dispose();
            }
            backgroundBrush = overrideBackBrush;
        }


    }
}
