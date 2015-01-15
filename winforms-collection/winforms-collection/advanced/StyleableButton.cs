using SharedFunctionalities;
using SharedFunctionalities.drawing;
using SharedFunctionalities.drawing.layers;
using SharedFunctionalities.drawing.layers.backgrounds;
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

        private TriangularShapeBackground backLayer = new TriangularShapeBackground() { endColor = Color.CadetBlue, startColor = Color.BlueViolet };

        private CenterText textLayer = new CenterText();



        #region property startColor / end color delegates
        [EditorBrowsable]
        public Color startColor {
            get { return backLayer.startColor; }
            set { backLayer.startColor = value; }
        }

        [EditorBrowsable]
        public Color endColor {
            get { return backLayer.endColor; }
            set { backLayer.endColor = value; }
        }
        #endregion


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

        public override Color ForeColor {
            get {
                return base.ForeColor;
            }

            set {
                base.ForeColor = value;
                textLayer.displayBrush = new SolidBrush( value );
            }
        }

    }
}
