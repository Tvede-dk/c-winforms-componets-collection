using System;

namespace winforms_collection {
    partial class SimpleList {
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






        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleList));
            this.listContainer1 = new winforms_collection.containers.ListContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listContainer2 = new winforms_collection.containers.ListContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.simpleListControl1 = new winforms_collection.lists.SimpleListControl();
            this.label1 = new System.Windows.Forms.Label();
            this.listContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.listContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listContainer1
            // 
            this.listContainer1.AutoScroll = true;
            this.listContainer1.Controls.Add(this.panel1);
            this.listContainer1.Controls.Add(this.simpleListControl1);
            this.listContainer1.Controls.Add(this.label1);
            this.listContainer1.DirectionHorizontal = false;
            this.listContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContainer1.heightWeight = ((System.ComponentModel.BindingList<float>)(resources.GetObject("listContainer1.heightWeight")));
            this.listContainer1.Location = new System.Drawing.Point(0, 0);
            this.listContainer1.minSplitHeight = 0;
            this.listContainer1.Name = "listContainer1";
            this.listContainer1.Size = new System.Drawing.Size(280, 395);
            this.listContainer1.SplitHeight = 0;
            this.listContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 343);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 52);
            this.panel1.TabIndex = 3;
            // 
            // listContainer2
            // 
            this.listContainer2.AutoScroll = true;
            this.listContainer2.Controls.Add(this.button1);
            this.listContainer2.Controls.Add(this.button4);
            this.listContainer2.Controls.Add(this.button3);
            this.listContainer2.DirectionHorizontal = true;
            this.listContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContainer2.heightWeight = ((System.ComponentModel.BindingList<float>)(resources.GetObject("listContainer2.heightWeight")));
            this.listContainer2.Location = new System.Drawing.Point(0, 0);
            this.listContainer2.minSplitHeight = 0;
            this.listContainer2.Name = "listContainer2";
            this.listContainer2.Size = new System.Drawing.Size(280, 52);
            this.listContainer2.SplitHeight = 3;
            this.listContainer2.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(186, 0);
            this.button1.MinimumSize = new System.Drawing.Size(45, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 52);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.Image = global::winforms_collection.Properties.Resources.pencil;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(93, 0);
            this.button4.MinimumSize = new System.Drawing.Size(45, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 52);
            this.button4.TabIndex = 7;
            this.button4.Text = "Edit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.MinimumSize = new System.Drawing.Size(45, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 52);
            this.button3.TabIndex = 8;
            this.button3.Text = "  Remove";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // simpleListControl1
            // 
            this.simpleListControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.simpleListControl1.FullRowSelect = true;
            this.simpleListControl1.GridLines = true;
            this.simpleListControl1.Location = new System.Drawing.Point(0, 32);
            this.simpleListControl1.MultiSelect = false;
            this.simpleListControl1.Name = "simpleListControl1";
            this.simpleListControl1.ShowItemToolTips = true;
            this.simpleListControl1.Size = new System.Drawing.Size(280, 311);
            this.simpleListControl1.TabIndex = 2;
            this.simpleListControl1.UseCompatibleStateImageBehavior = false;
            this.simpleListControl1.View = System.Windows.Forms.View.List;
            this.simpleListControl1.SelectedIndexChanged += new System.EventHandler(this.simpleListControl1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "TITEL";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SimpleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listContainer1);
            this.Name = "SimpleList";
            this.Size = new System.Drawing.Size(280, 395);
            this.listContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.listContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }




        #endregion

        private containers.ListContainer listContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private containers.ListContainer listContainer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private lists.SimpleListControl simpleListControl1;
    }
}
