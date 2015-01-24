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
            Graph.Compatibility.AlwaysCompatible alwaysCompatible1 = new Graph.Compatibility.AlwaysCompatible();
            this.graphControl1 = new Graph.GraphControl();
            this.graphComponent1 = new winforms_collection.advanced.Graph.GraphComponent();
            this.SuspendLayout();
            // 
            // graphControl1
            // 
            this.graphControl1.CompatibilityStrategy = alwaysCompatible1;
            this.graphControl1.FocusElement = null;
            this.graphControl1.HighlightCompatible = false;
            this.graphControl1.LargeGridStep = 128F;
            this.graphControl1.LargeStepGridColor = System.Drawing.Color.LightGray;
            this.graphControl1.Location = new System.Drawing.Point(12, 12);
            this.graphControl1.Name = "graphControl1";
            this.graphControl1.ShowLabels = false;
            this.graphControl1.Size = new System.Drawing.Size(478, 389);
            this.graphControl1.SmallGridStep = 16F;
            this.graphControl1.SmallStepGridColor = System.Drawing.Color.Gray;
            this.graphControl1.TabIndex = 0;
            this.graphControl1.Text = "graphControl1";
            // 
            // graphComponent1
            // 
            this.graphComponent1.BorderColor = System.Drawing.Color.Empty;
            this.graphComponent1.BorderSize = 0;
            this.graphComponent1.FlashBorderColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(74)))), ((int)(((byte)(178)))));
            this.graphComponent1.FlashBorderOnMouseOver = false;
            this.graphComponent1.Location = new System.Drawing.Point(510, 12);
            this.graphComponent1.Name = "graphComponent1";
            this.graphComponent1.Size = new System.Drawing.Size(488, 389);
            this.graphComponent1.spaceBetween = 75;
            this.graphComponent1.TabIndex = 1;
            this.graphComponent1.Text = "graphComponent1";
            // 
            // perfTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 398);
            this.Controls.Add(this.graphComponent1);
            this.Controls.Add(this.graphControl1);
            this.Name = "perfTest";
            this.Text = "5";
            this.Load += new System.EventHandler(this.perfTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Graph.GraphControl graphControl1;
        private winforms_collection.advanced.Graph.GraphComponent graphComponent1;
    }
}