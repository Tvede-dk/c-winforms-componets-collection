namespace Samples_and_tests {
    partial class perfTest {
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
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.styleableButton1 = new winforms_collection.advanced.StyleableButton();
            this.SuspendLayout();
            // 
            // styleableButton1
            // 
            this.styleableButton1.BorderColor = System.Drawing.Color.Empty;
            this.styleableButton1.BorderSize = 0;
            this.styleableButton1.endColor = System.Drawing.SystemColors.HotTrack;
            this.styleableButton1.FlashBorderColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(74)))), ((int)(((byte)(178)))));
            this.styleableButton1.FlashBorderOnMouseOver = false;
            this.styleableButton1.ForeColor = System.Drawing.Color.White;
            this.styleableButton1.Location = new System.Drawing.Point(12, 12);
            this.styleableButton1.Name = "styleableButton1";
            this.styleableButton1.Size = new System.Drawing.Size(123, 83);
            this.styleableButton1.startColor = System.Drawing.Color.Blue;
            this.styleableButton1.TabIndex = 0;
            this.styleableButton1.Text = "styleableButton1";
            // 
            // perfTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 398);
            this.Controls.Add(this.styleableButton1);
            this.Name = "perfTest";
            this.Text = "5";
            this.Load += new System.EventHandler(this.perfTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private winforms_collection.advanced.StyleableButton styleableButton1;
    }
}