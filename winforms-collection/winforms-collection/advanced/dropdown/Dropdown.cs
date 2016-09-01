using System;
using System.Drawing;
using System.Windows.Forms;
using SharedFunctionalities;

namespace winforms_collection.advanced.dropdown {
    public partial class dropdown : CustomControl {


        public dropdown() {
            InitializeComponent();
        }

        protected override void OnPaint( PaintEventArgs e ) {
            base.OnPaint( e );
            drawBackground( e );
            drawBorder( e );

        }

        #region back color and brush

        public override Color BackColor {
            get {
                return base.BackColor;
            }
            set {
                base.BackColor = value;
                _backBrush = new SolidBrush( value );
            }
        }

        private Brush _backBrush;
        #endregion

        protected override void OnMouseEnter( EventArgs e ) {
            base.OnMouseEnter( e );
            SharedFunctionalities.SharedAnimations.Highlight( this, 0.7f, 200 );
        }

        protected override void OnMouseLeave( EventArgs e ) {
            base.OnMouseLeave( e );
        }



        #region simple draw stuff
        private void drawBorder( PaintEventArgs e ) {
            e.Graphics.DrawRectangle( Pens.Black, 0, 0, Width - (int)Pens.Black.Width, Height - (int)Pens.Black.Width ); // 1 px wide margin .
        }

        private void drawBackground( PaintEventArgs e ) {
            e.Graphics.FillRectangle( _backBrush, e.ClipRectangle );
        }

        #endregion

        protected override void OnPaintBackground( PaintEventArgs e ) {
        }

    }
}
