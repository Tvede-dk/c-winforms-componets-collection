namespace winforms_collection.popup_boxes {
    partial class SelectionPopup<T> {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectionPopup<object>));
            this.simpleListControl1 = new winforms_collection.lists.SimpleListControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listContainer1 = new winforms_collection.containers.ListContainer();
            this.styleableButton2 = new winforms_collection.advanced.StyleableButton();
            this.styleableButton1 = new winforms_collection.advanced.StyleableButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.listContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleListControl1
            // 
            this.simpleListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleListControl1.Location = new System.Drawing.Point(0, 0);
            this.simpleListControl1.Name = "simpleListControl1";
            this.simpleListControl1.Size = new System.Drawing.Size(517, 431);
            this.simpleListControl1.TabIndex = 0;
            this.simpleListControl1.UseCompatibleStateImageBehavior = false;
            this.simpleListControl1.View = System.Windows.Forms.View.List;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.simpleListControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listContainer1);
            this.splitContainer1.Size = new System.Drawing.Size(517, 492);
            this.splitContainer1.SplitterDistance = 431;
            this.splitContainer1.TabIndex = 1;
            // 
            // listContainer1
            // 
            this.listContainer1.AutoScroll = true;
            this.listContainer1.Controls.Add(this.styleableButton2);
            this.listContainer1.Controls.Add(this.styleableButton1);
            this.listContainer1.DirectionHorizontal = true;
            this.listContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContainer1.Location = new System.Drawing.Point(0, 0);
            this.listContainer1.MinSplitHeight = 0;
            this.listContainer1.Name = "listContainer1";
            this.listContainer1.Size = new System.Drawing.Size(517, 57);
            this.listContainer1.SplitHeight = 2;
            this.listContainer1.TabIndex = 0;
            // 
            // styleableButton2
            // 
            this.styleableButton2.BorderColor = System.Drawing.Color.Black;
            this.styleableButton2.BorderSize = 1;
            this.styleableButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.styleableButton2.Dock = System.Windows.Forms.DockStyle.Left;
            this.styleableButton2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(74)))), ((int)(((byte)(19)))));
            this.styleableButton2.FlashBorderColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(74)))), ((int)(((byte)(19)))));
            this.styleableButton2.FlashBorderOnMouseOver = false;
            this.styleableButton2.ForeColor = System.Drawing.Color.Cornsilk;
            this.styleableButton2.Location = new System.Drawing.Point(258, 0);
            this.styleableButton2.Name = "styleableButton2";
            this.styleableButton2.Size = new System.Drawing.Size(258, 57);
            this.styleableButton2.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(74)))), ((int)(((byte)(19)))));
            this.styleableButton2.TabIndex = 1;
            this.styleableButton2.Text = "Cancel";
            this.styleableButton2.Click += new System.EventHandler(this.styleableButton2_Click);
            // 
            // styleableButton1
            // 
            this.styleableButton1.BorderColor = System.Drawing.Color.Black;
            this.styleableButton1.BorderSize = 1;
            this.styleableButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.styleableButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.styleableButton1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(74)))), ((int)(((byte)(178)))));
            this.styleableButton1.FlashBorderColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(74)))), ((int)(((byte)(178)))));
            this.styleableButton1.FlashBorderOnMouseOver = false;
            this.styleableButton1.ForeColor = System.Drawing.Color.Cornsilk;
            this.styleableButton1.Location = new System.Drawing.Point(0, 0);
            this.styleableButton1.Name = "styleableButton1";
            this.styleableButton1.Size = new System.Drawing.Size(258, 57);
            this.styleableButton1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(74)))), ((int)(((byte)(178)))));
            this.styleableButton1.TabIndex = 0;
            this.styleableButton1.Text = "Accept";
            this.styleableButton1.Click += new System.EventHandler(this.styleableButton1_Click);
            // 
            // SelectionPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 492);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SelectionPopup";
            this.Text = "SelectionPopup";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.listContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private lists.SimpleListControl simpleListControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private containers.ListContainer listContainer1;
        private advanced.StyleableButton styleableButton2;
        private advanced.StyleableButton styleableButton1;
    }
}