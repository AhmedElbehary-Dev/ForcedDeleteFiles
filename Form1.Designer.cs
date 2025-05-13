namespace FileForceDeleter
{
    partial class forced_delete_file
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(forced_delete_file));
            browse_btn = new Button();
            delete_btn = new Button();
            flowLayoutPanelFiles = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // browse_btn
            // 
            browse_btn.BackColor = Color.FromArgb(0, 192, 0);
            browse_btn.FlatAppearance.BorderSize = 0;
            browse_btn.FlatStyle = FlatStyle.Flat;
            browse_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            browse_btn.ForeColor = Color.Transparent;
            browse_btn.Location = new Point(12, 12);
            browse_btn.Name = "browse_btn";
            browse_btn.Size = new Size(776, 40);
            browse_btn.TabIndex = 1;
            browse_btn.Text = "Browse";
            browse_btn.TextAlign = ContentAlignment.TopCenter;
            browse_btn.UseVisualStyleBackColor = false;
            browse_btn.Click += btnBrowseFiles_Click;
            // 
            // delete_btn
            // 
            delete_btn.BackColor = Color.Tomato;
            delete_btn.FlatAppearance.BorderSize = 0;
            delete_btn.FlatStyle = FlatStyle.Flat;
            delete_btn.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            delete_btn.ImageAlign = ContentAlignment.TopCenter;
            delete_btn.Location = new Point(12, 428);
            delete_btn.Name = "delete_btn";
            delete_btn.Size = new Size(776, 45);
            delete_btn.TabIndex = 2;
            delete_btn.Text = "Delete";
            delete_btn.UseVisualStyleBackColor = false;
            delete_btn.Click += btnDelete_Click;
            // 
            // flowLayoutPanelFiles
            // 
            flowLayoutPanelFiles.AutoScroll = true;
            flowLayoutPanelFiles.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelFiles.Location = new Point(12, 58);
            flowLayoutPanelFiles.Name = "flowLayoutPanelFiles";
            flowLayoutPanelFiles.Size = new Size(776, 364);
            flowLayoutPanelFiles.TabIndex = 3;
            flowLayoutPanelFiles.WrapContents = false;
            // 
            // forced_delete_file
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 485);
            Controls.Add(flowLayoutPanelFiles);
            Controls.Add(delete_btn);
            Controls.Add(browse_btn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "forced_delete_file";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Forced delete file";
            ResumeLayout(false);
        }

        #endregion
        private Button browse_btn;
        private Button delete_btn;
        private FlowLayoutPanel flowLayoutPanelFiles;
    }
}
