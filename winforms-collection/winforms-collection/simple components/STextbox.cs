using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;
using winforms_collection.validator;

namespace winforms_collection {
    public partial class STextbox : TextBox {

        private string _placeHolder = "";

        private Font _placeHolderFont;
        private readonly SolidBrush _placeHolderBrush = new SolidBrush(Color.FromArgb(150, 205, 205, 205)); // #cccccc with 50% transperency

        [EditorBrowsable()]
        [Description("This is also known as a hint, or watermark. Its a hint to the user about what this textfield should contain.")]

        public string PlaceHolder {
            get { return _placeHolder; }
            set { _placeHolder = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private IValidatorType _validator;
        public IValidatorType Validator {
            get {
                return _validator;
            }
            set {
                _validator = value;
            }
        }

        private TextboxType _dataType = TextboxType.RegularText;

        [EditorBrowsable]
        [Description("...")]

        public TextboxType DataType {
            get {
                return _dataType;
            }
            set {
                _dataType = value;
                OnTextboxTypeChange(value);
            }
        }


        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            //run validation in background.
            if (_validator != null) {
                RunValidator();
            }
        }



        private void RunValidator() {
            //propperly a threadpool , given that it is a cpu intensitive validation, and we might want this multiple places and alike..
            //TODO make in another thread
            if (!Validate()) {
                var loc = PointToScreen(Point.Empty);
                var displayPoint = new Point(loc.X + Width, loc.Y);
                popup_boxes.NotificationBar.ShowAtLocation(Validator.GetErrorMessage(), displayPoint);
            }
        }
        public bool Validate() {
            if (_validator != null) {
                return _validator.Validate(Text);
            } else {
                return true;
            }
        }


        public STextbox() {
            MaxLength = 0;
            _placeHolderFont = DefaultFont;
            InitializeComponent();
            //SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.CacheText | ControlStyles.ResizeRedraw, true); // | ControlStyles.UserPaint

        }

        protected override void OnKeyDown(KeyEventArgs e) {
            var handled = false;
            #region ctrl + back => delete word
            if (e.Control && e.KeyCode == Keys.Back) {
                handled = true;
                if (SelectionLength > 1) {
                    Slice(SelectionStart, SelectionStart + SelectionLength);
                } else {
                    DeleteFromCursorToLeftWord();
                }
            }

            #endregion

            if (e.KeyCode == Keys.Delete && e.Shift) {
                handled = true;
                if (this.Multiline) {
                    var preStart = SelectionStart;
                    var lineIndex = GetLineFromCharIndex(preStart);
                    Lines = SharedFunctionalities.SharedStringUtils.RemoveIndexFromArray(Lines, lineIndex);
                    SelectionStart = Math.Max(GetFirstCharIndexFromLine(lineIndex), preStart - 1);
                } else {
                    Text = "";
                }
            }

            if (e.KeyCode == Keys.A && e.Control) {
                SelectionStart = 0;
                SelectionLength = Text.Length;
                handled = true;
            }
            if (e.KeyCode == Keys.Down && e.Control && e.Shift && Multiline) {
                var lineIndex = GetCurrentLine();
                this.Lines = SharedFunctionalities.SharedStringUtils.InsertValueIntoIndexInArray(Lines, Lines[GetLineFromCharIndex(SelectionStart)], GetLineFromCharIndex(SelectionStart));
                SetCurrentLine(lineIndex + 1);
                handled = true;
            }
            if (e.KeyCode == Keys.Up && e.Control && e.Shift && Multiline) {
                var lineIndex = GetCurrentLine();
                this.Lines = SharedFunctionalities.SharedStringUtils.InsertValueIntoIndexInArray(Lines, Lines[GetLineFromCharIndex(SelectionStart)], GetLineFromCharIndex(SelectionStart));
                SetCurrentLine(lineIndex - 1);
                handled = true;
            }
            if (handled) {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            base.OnKeyDown(e);
        }

        public void SetCurrentLine(int line) {
            this.SelectionStart = GetFirstCharIndexFromLine(line);
        }

        public int GetCurrentLine() {
            return GetLineFromCharIndex(SelectionStart);
        }

        public void DeleteFromCursorToLeftWord() {
            var end = SelectionStart;
            var lineStart = GetFirstCharIndexOfCurrentLine();
            var start = Text.LastIndexOf(' ', end - 1);

            if (start == -1) {
                start = 0;
            }
            Slice(Math.Max(start, lineStart - 1), end);
            SelectionStart = Text.Length;
        }


        public void ShowTextHint(String hint) {

        }

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
        }

        private void Slice(int sliceStart, int sliceEnd) {
            var secoundPart = "";
            if (sliceEnd < Text.Length) {
                secoundPart = Text.Substring(sliceEnd);
            }
            Text = Text.Substring(0, sliceStart) + secoundPart;
        }
        private void LoadNames() {
            AutoCompleteCustomSource = StaticsLoads.GetAutocompleteNames();
        }

        private static class StaticsLoads {
            static AutoCompleteStringCollection _autoCompleteNames;
            public static AutoCompleteStringCollection GetAutocompleteNames() {
                if (_autoCompleteNames == null && IsDesignMode() == false && IsInDesignMode() == false) {
                    _autoCompleteNames = new AutoCompleteStringCollection();
                    _autoCompleteNames.AddRange(Properties.Resources.names.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
                }
                return _autoCompleteNames;
            }
        }

        private void OnTextboxTypeChange(TextboxType value) {
            switch (value) {
                case TextboxType.RegularText:
                    this.AutoCompleteCustomSource = null;
                    break;
                case TextboxType.PersonName:
                    LoadNames();
                    break;
                case TextboxType.Decimal:
                    var dec = new NumberString();
                    dec.AllowDecimal = true;
                    this.Validator = dec;
                    break;
                case TextboxType.Number:
                    var num = new NumberString();
                    num.AllowInt = true;
                    this.Validator = num;
                    break;
                default:
                    break;
            }
        }
        public static bool IsInDesignMode() {
            if (Application.ExecutablePath.IndexOf("devenv.exe", StringComparison.OrdinalIgnoreCase) > -1) {
                return true;
            }
            return false;
        }


        private bool GetDesignMode() {
            IDesignerHost host;
            if (Site != null) {
                host = Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host != null) {
                    return host.RootComponent.Site.DesignMode;
                }
            }
            MessageBox.Show("Runtime Mode");
            return false;
        }
        private static bool IsDesignMode() {
            return (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        }

        public enum TextboxType {
            RegularText,
            PersonName,
            Number,
            Decimal
        }
    }
}
