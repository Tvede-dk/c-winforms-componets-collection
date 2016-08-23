﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel.Design;
using winforms_collection.validator;

namespace winforms_collection {
    public partial class STextbox : TextBox {

        private string _placeHolder = "";

        private Font _placeHolderFont;
        private SolidBrush _placeHolderBrush = new SolidBrush(Color.FromArgb(150, 205, 205, 205)); // #cccccc with 50% transperency

        [EditorBrowsable()]
        [Description("This is also known as a hint, or watermark. Its a hint to the user about what this textfield should contain.")]

        public string placeHolder {
            get { return _placeHolder; }
            set { _placeHolder = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private IValidatorType _validator;
        public IValidatorType validator {
            get {
                return _validator;
            }
            set {
                _validator = value;
            }
        }

        private TextboxType _dataType = TextboxType.REGULAR_TEXT;

        [EditorBrowsable]
        [Description("...")]

        public TextboxType dataType {
            get {
                return _dataType;
            }
            set {
                _dataType = value;
                onTextboxTypeChange(value);
            }
        }


        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            //run validation in background.
            if (_validator != null) {
                runValidator();
            }
        }



        private void runValidator() {
            //propperly a threadpool , given that it is a cpu intensitive validation, and we might want this multiple places and alike..
            //TODO make in another thread
            if (!validate()) {
                var loc = PointToScreen(Point.Empty);
                Point displayPoint = new Point(loc.X + Width, loc.Y);
                popup_boxes.NotificationBar.showAtLocation(validator.getErrorMessage(), displayPoint);
            }
        }
        public bool validate() {
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
            bool handled = false;
            #region ctrl + back => delete word
            if (e.Control && e.KeyCode == Keys.Back) {
                handled = true;
                if (SelectionLength > 1) {
                    slice(SelectionStart, SelectionStart + SelectionLength);
                } else {
                    deleteFromCursorToLeftWord();
                }
            }

            #endregion

            if (e.KeyCode == Keys.Delete && e.Shift) {
                handled = true;
                if (this.Multiline) {
                    int preStart = SelectionStart;
                    int lineIndex = GetLineFromCharIndex(preStart);
                    Lines = SharedFunctionalities.SharedStringUtils.removeIndexFromArray(Lines, lineIndex);
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
                int lineIndex = getCurrentLine();
                this.Lines = SharedFunctionalities.SharedStringUtils.insertValueIntoIndexInArray(Lines, Lines[GetLineFromCharIndex(SelectionStart)], GetLineFromCharIndex(SelectionStart));
                setCurrentLine(lineIndex + 1);
                handled = true;
            }
            if (e.KeyCode == Keys.Up && e.Control && e.Shift && Multiline) {
                int lineIndex = getCurrentLine();
                this.Lines = SharedFunctionalities.SharedStringUtils.insertValueIntoIndexInArray(Lines, Lines[GetLineFromCharIndex(SelectionStart)], GetLineFromCharIndex(SelectionStart));
                setCurrentLine(lineIndex - 1);
                handled = true;
            }
            if (handled) {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            base.OnKeyDown(e);
        }

        public void setCurrentLine(int line) {
            this.SelectionStart = GetFirstCharIndexFromLine(line);
        }

        public int getCurrentLine() {
            return GetLineFromCharIndex(SelectionStart);
        }

        public void deleteFromCursorToLeftWord() {
            int end = SelectionStart;
            int lineStart = GetFirstCharIndexOfCurrentLine();
            int start = Text.LastIndexOf(' ', end - 1);

            if (start == -1) {
                start = 0;
            }
            slice(Math.Max(start, lineStart - 1), end);
            SelectionStart = Text.Length;
        }


        public void showTextHint(String hint) {

        }

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e) {
            base.OnLostFocus(e);
        }

        private void slice(int sliceStart, int sliceEnd) {
            var secoundPart = "";
            if (sliceEnd < Text.Length) {
                secoundPart = Text.Substring(sliceEnd);
            }
            Text = Text.Substring(0, sliceStart) + secoundPart;
        }
        private void loadNames() {
            AutoCompleteCustomSource = StaticsLoads.getAutocompleteNames();
        }

        private static class StaticsLoads {
            static AutoCompleteStringCollection autoCompleteNames;
            public static AutoCompleteStringCollection getAutocompleteNames() {
                if (autoCompleteNames == null && isDesignMode() == false && IsInDesignMode() == false) {
                    autoCompleteNames = new AutoCompleteStringCollection();
                    autoCompleteNames.AddRange(Properties.Resources.names.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
                }
                return autoCompleteNames;
            }
        }

        private void onTextboxTypeChange(TextboxType value) {
            switch (value) {
                case TextboxType.REGULAR_TEXT:
                    this.AutoCompleteCustomSource = null;
                    break;
                case TextboxType.PERSON_NAME:
                    loadNames();
                    break;
                case TextboxType.DECIMAL:
                    var dec = new NumberString();
                    dec.allowDecimal = true;
                    this.validator = dec;
                    break;
                case TextboxType.NUMBER:
                    var num = new NumberString();
                    num.allowInt = true;
                    this.validator = num;
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


        private bool getDesignMode() {
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
        private static bool isDesignMode() {
            return (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        }

        public enum TextboxType {
            REGULAR_TEXT,
            PERSON_NAME,
            NUMBER,
            DECIMAL
        }
    }
}
