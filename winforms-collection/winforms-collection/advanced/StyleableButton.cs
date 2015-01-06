using SharedFunctionalities;
using SharedFunctionalities.forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.advanced {
    public class StyleableButton : CustomControl {

        private Brush backgroundBrush;

        public StyleableButton() {
            BorderSize = 1;
            FlashBorderColorStart = Color.Transparent;
        }




        protected override void OnPaint( PaintEventArgs e ) {
            base.OnPaint( e );
            drawBackground( e );
            drawText( e );


        }

        private void drawBackground( PaintEventArgs e ) {
            if ( backgroundBrush == null ) {
                LinearGradientBrush toUse = new LinearGradientBrush( ClientRectangle, Color.FromArgb( 255, 230, 240, 163 ), Color.FromArgb( 255, 210, 230, 56 ), 90f, true );
                toUse.SetBlendTriangularShape( 0.5f, 1.0f );
                backgroundBrush = toUse;
            }
            e.Graphics.FillRectangle( backgroundBrush, ClientRectangle.InnerPart( BorderSize ) );
        }

        private void drawText( PaintEventArgs e ) {
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            e.Graphics.DrawString( Text, Font, Brushes.Black, ClientRectangle.InnerPart( BorderSize ), format );
        }

        protected override void OnMouseEnter( EventArgs e ) {
            base.OnMouseEnter( e );
            SharedAnimations.Highlight( this, 0.1f, 120 );
        }

        private HighlightOverlay overlay;

        protected override void OnMouseDown( MouseEventArgs e ) {
            base.OnMouseDown( e );
            //make an "insert" effect.
            overlay = SharedAnimations.Highlight( this, 0.5f, 100, Brushes.White );
        }
        protected override void OnMouseUp( MouseEventArgs e ) {
            base.OnMouseUp( e );
            overlay.FadeOut( 150, () => { overlay.Dispose(); } );
        }

        protected override void OnClick( EventArgs e ) {
            base.OnClick( e );
        }

        protected override void OnKeyDown( KeyEventArgs e ) {
            base.OnKeyDown( e );
            if ( e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter ) {
                OnClick( e );
            }
        }

    }
}
