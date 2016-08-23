using SharedFunctionalities;
using SharedFunctionalities.drawing.layers;
using SharedFunctionalities.drawing.layers.backgrounds;
using SharedFunctionalities.forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace winforms_collection.advanced {
    public class StyleableButton : CustomControl, IButtonControl {
        private static readonly Color initColor = Color.FromArgb(255, 9, 74, 178); //a simple blue.

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
            DrawHandler.addLayer(backLayer);
            DrawHandler.addLayer(textLayer);
            textLayer.font = Font;
            ForeColor = Color.Cornsilk;
            startColor = initColor;
            endColor = initColor;

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

        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
            //  SharedAnimations.Highlight( this, 0.1f, 120 );
        }

        private HighlightOverlay overlay;

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            //overlay = SharedAnimations.Highlight(this, 0.1f, 50, Brushes.Blue);
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
            //  overlay.FadeOut(0, () => { overlay.Dispose(); });
        }


        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            //make an "insert" effect.
            //overlay = SharedAnimations.Highlight(this, 0.4f, 100, Brushes.White);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            // overlay.FadeOut(150, () => { overlay.Dispose(); });
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter) {
                OnClick(e);
            }
        }

        public void NotifyDefault(bool value) {
            if (value) {

            }
        }

        public void PerformClick() {
            OnClick(new EventArgs());
        }

        public override Color ForeColor {
            get {
                return base.ForeColor;
            }

            set {
                base.ForeColor = value;
                textLayer.displayBrush = new SolidBrush(value);
            }
        }

        public DialogResult DialogResult { get; set; }
    }
}
