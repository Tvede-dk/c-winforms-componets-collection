using SharedFunctionalities;
using SharedFunctionalities.drawing;
using SharedFunctionalities.drawing.layers;
using SharedFunctionalities.forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection.advanced {
    public class StyleableButton : CustomControl {

        private drawBackground backLayer = new drawBackground();

        private CenterText textLayer = new CenterText();

        public StyleableButton() {
            DrawHandler.addLayer( backLayer );
            DrawHandler.addLayer( textLayer );
        }

        public override Font Font {
            get {
                return base.Font;
            }

            set {
                base.Font = value;
                textLayer.font = value;
            }
        }

        public override string Text {
            get {
                return base.Text;
            }

            set {
                base.Text = value;
                textLayer.Text = value;
            }
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

        protected override void OnKeyDown( KeyEventArgs e ) {
            base.OnKeyDown( e );
            if ( e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter ) {
                OnClick( e );
            }
        }

        private class drawBackground : BrushBackground {

            public override void draw( Graphics g, ref Rectangle wholeComponent, ref Rectangle clippingRect ) {
                LinearGradientBrush toUse = new LinearGradientBrush( wholeComponent, Color.FromArgb( 255, 230, 240, 163 ), Color.FromArgb( 255, 210, 230, 56 ), 90f, true );
                toUse.SetBlendTriangularShape( 0.5f, 1.0f );
                Background = toUse;
                base.draw( g, ref wholeComponent, ref clippingRect );
            }
        }
    }
}
