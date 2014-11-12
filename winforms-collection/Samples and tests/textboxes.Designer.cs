namespace Samples_and_tests {
    partial class textboxes {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.sTextbox2 = new winforms_collection.STextbox();
            this.sTextbox1 = new winforms_collection.STextbox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Standard text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Standard text (multiline)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Disover multithreadning winning time";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sTextbox2
            // 
            this.sTextbox2.Location = new System.Drawing.Point(134, 38);
            this.sTextbox2.Multiline = true;
            this.sTextbox2.Name = "sTextbox2";
            this.sTextbox2.Size = new System.Drawing.Size(203, 73);
            this.sTextbox2.TabIndex = 2;
            // 
            // sTextbox1
            // 
            this.sTextbox1.Location = new System.Drawing.Point(134, 12);
            this.sTextbox1.Name = "sTextbox1";
            this.sTextbox1.Size = new System.Drawing.Size(203, 20);
            this.sTextbox1.TabIndex = 0;
            // 
            // textboxes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sTextbox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sTextbox1);
            this.Name = "textboxes";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private winforms_collection.STextbox sTextbox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private winforms_collection.STextbox sTextbox2;
        private System.Windows.Forms.Button button1;
    }
}

