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
            this.styleableButton1.BorderSize = 1;
            this.styleableButton1.FlashBorderColorStart = System.Drawing.Color.Transparent;
            this.styleableButton1.FlashBorderOnMouseOver = false;
            this.styleableButton1.Font = new System.Drawing.Font("Monospace", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleableButton1.Location = new System.Drawing.Point(12, 12);
            this.styleableButton1.Name = "styleableButton1";
            this.styleableButton1.Size = new System.Drawing.Size(986, 374);
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
            this.Text = "perfTest";
            this.Load += new System.EventHandler(this.perfTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private winforms_collection.advanced.StyleableButton styleableButton1;
    }
}