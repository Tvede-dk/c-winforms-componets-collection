﻿namespace winforms_collection.advanced.dropdown {
    partial class Dropdown {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if ( disposing && (components != null) ) {
                components.Dispose();
            }
            if (_backBrush!=null) {
                _backBrush.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // dropdown
            // 
            this.Name = "dropdown";
            this.Size = new System.Drawing.Size(200, 30);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
