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
        private static readonly Color InitColor = Color.FromArgb(255, 9, 74, 178); //a simple blue.

        private readonly TriangularShapeBackground _backLayer = new TriangularShapeBackground() { EndColor = Color.CadetBlue, StartColor = Color.BlueViolet };

        private readonly CenterText _textLayer = new CenterText();



        #region property startColor / end color delegates
        [EditorBrowsable]
        public Color StartColor {
            get { return _backLayer.StartColor; }
            set { _backLayer.StartColor = value; }

        }

        [EditorBrowsable]
        public Color EndColor {
            get { return _backLayer.EndColor; }
            set { _backLayer.EndColor = value; }
        }
        #endregion


        public StyleableButton() {
            DrawHandler.AddLayer(_backLayer);
            DrawHandler.AddLayer(_textLayer);
            _textLayer.Font = Font;
            ForeColor = Color.Cornsilk;
            StartColor = InitColor;
            EndColor = InitColor;

        }

        public override Font Font {
            get {
                return base.Font;
            }

            set {
                base.Font = value;
                _textLayer.Font = value;
            }
        }

        public override string Text {
            get {
                return base.Text;
            }
            set {
                base.Text = value;
                _textLayer.Text = value;
            }
        }

        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
            SharedAnimations.Highlight( this, 0.1f, 120 );
        }

        private HighlightOverlay _overlay;

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            _overlay = SharedAnimations.Highlight(this, 0.1f, 50, Brushes.Blue);
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
            _overlay.FadeOut(0, () => { _overlay.Dispose(); });
        }


        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            //make an "insert" effect.
            _overlay = SharedAnimations.Highlight(this, 0.4f, 100, Brushes.White);
        }
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            _overlay.FadeOut(150, () => { _overlay.Dispose(); });
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
                _textLayer.DisplayBrush = new SolidBrush(value);
            }
        }

        public DialogResult DialogResult { get; set; }
    }
}
