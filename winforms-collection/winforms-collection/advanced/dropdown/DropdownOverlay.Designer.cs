namespace winforms_collection.advanced.dropdown {
    partial class DropdownOverlay {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DropdownOverlay));
            this.listContainer1 = new winforms_collection.containers.ListContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.listContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listContainer1
            // 
            this.listContainer1.AutoScroll = true;
            this.listContainer1.BackColor = System.Drawing.Color.DimGray;
            this.listContainer1.Controls.Add(this.label1);
            this.listContainer1.DirectionHorizontal = false;
            this.listContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContainer1.heightWeight = ((System.ComponentModel.BindingList<float>)(resources.GetObject("listContainer1.heightWeight")));
            this.listContainer1.Location = new System.Drawing.Point(0, 0);
            this.listContainer1.minSplitHeight = 0;
            this.listContainer1.Name = "listContainer1";
            this.listContainer1.Size = new System.Drawing.Size(120, 25);
            this.listContainer1.SplitHeight = 0;
            this.listContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ctrl+<number>";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DropdownOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(120, 25);
            this.Controls.Add(this.listContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DropdownOverlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DropdownOverlay";
            this.TransparencyKey = System.Drawing.Color.White;
            this.listContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private containers.ListContainer listContainer1;
        private System.Windows.Forms.Label label1;
    }
}