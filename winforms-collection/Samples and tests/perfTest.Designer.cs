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
            this.graphComponent1 = new winforms_collection.advanced.Graph.GraphComponent();
            this.SuspendLayout();
            // 
            // graphComponent1
            // 
            this.graphComponent1.BorderColor = System.Drawing.Color.Black;
            this.graphComponent1.BorderSize = 10;
            this.graphComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphComponent1.FlashBorderColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(74)))), ((int)(((byte)(178)))));
            this.graphComponent1.FlashBorderOnMouseOver = false;
            this.graphComponent1.Location = new System.Drawing.Point(0, 0);
            this.graphComponent1.Name = "graphComponent1";
            this.graphComponent1.Size = new System.Drawing.Size(1260, 816);
            this.graphComponent1.spaceBetween = 5;
            this.graphComponent1.TabIndex = 0;
            this.graphComponent1.Text = "graphComponent1";
            this.graphComponent1.Click += new System.EventHandler(this.graphComponent1_Click);
            // 
            // perfTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 816);
            this.Controls.Add(this.graphComponent1);
            this.Name = "perfTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "5";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.perfTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private winforms_collection.advanced.Graph.GraphComponent graphComponent1;
    }
}