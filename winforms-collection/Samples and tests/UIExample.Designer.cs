namespace Samples_and_tests {
    partial class UIExample {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.collapsableSplitContainer1 = new winforms_collection.navigation.CollapsableSplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.collapsableSplitContainer1)).BeginInit();
            this.collapsableSplitContainer1.Panel1.SuspendLayout();
            this.collapsableSplitContainer1.Panel2.SuspendLayout();
            this.collapsableSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // collapsableSplitContainer1
            // 
            this.collapsableSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collapsableSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.collapsableSplitContainer1.Name = "collapsableSplitContainer1";
            // 
            // collapsableSplitContainer1.Panel1
            // 
            this.collapsableSplitContainer1.Panel1.BackColor = System.Drawing.Color.DarkRed;
            this.collapsableSplitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // collapsableSplitContainer1.Panel2
            // 
            this.collapsableSplitContainer1.Panel2.Controls.Add(this.button2);
            this.collapsableSplitContainer1.Size = new System.Drawing.Size(980, 570);
            this.collapsableSplitContainer1.SplitterDistance = 203;
            this.collapsableSplitContainer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 547);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "=";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 535);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Expand panel1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // UIExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 570);
            this.Controls.Add(this.collapsableSplitContainer1);
            this.Name = "UIExample";
            this.Text = "UIExample";
            this.collapsableSplitContainer1.Panel1.ResumeLayout(false);
            this.collapsableSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.collapsableSplitContainer1)).EndInit();
            this.collapsableSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private winforms_collection.navigation.CollapsableSplitContainer collapsableSplitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}