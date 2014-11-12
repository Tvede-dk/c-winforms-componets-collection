using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winforms_collection {
    public partial class STextbox : TextBox {
        public STextbox() {
            InitializeComponent();
        }

        protected override void OnKeyDown( KeyEventArgs e ) {
            #region ctrl + back => delete word
            if ( e.Control && e.KeyCode == Keys.Back ) {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if ( SelectionLength > 1 ) {
                    slice( SelectionStart, SelectionStart + SelectionLength );
                } else {
                    deleteFromCursorToLeftWord();
                }
                return;
            }

            #endregion

            if ( e.KeyCode == Keys.Delete && e.Shift ) {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if ( this.Multiline ) {
                    int preStart = SelectionStart;
                    int lineIndex = GetLineFromCharIndex( preStart );
                    Lines = SharedFunctionalities.SharedStringUtils.removeIndexFromArray( Lines, lineIndex );
                    SelectionStart = Math.Max( GetFirstCharIndexFromLine( lineIndex ), preStart - 1 );
                } else {
                    Text = "";
                }
                return;
            }




            base.OnKeyDown( e );
        }

        public void deleteFromCursorToLeftWord() {
            int end = SelectionStart;
            int start = Text.LastIndexOf( ' ', end - 1 );
            if ( start == -1 ) {
                start = 0;
            }
            slice( start, end );
            SelectionStart = Text.Length;
        }

        private void slice( int sliceStart, int sliceEnd ) {
            Text = Text.Substring( 0, sliceStart ) + Text.Substring( sliceEnd );
        }
    }
}
