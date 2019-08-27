namespace SmokeTest
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.treeViewInternalAssemblies = new System.Windows.Forms.TreeView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControlAssemblies = new System.Windows.Forms.TabControl();
            this.tabPageInternal = new System.Windows.Forms.TabPage();
            this.tabPageExternal = new System.Windows.Forms.TabPage();
            this.treeViewExternalAssemblies = new System.Windows.Forms.TreeView();
            this.toolStripAssemblies = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControlAssemblies.SuspendLayout();
            this.tabPageInternal.SuspendLayout();
            this.tabPageExternal.SuspendLayout();
            this.toolStripAssemblies.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeViewInternalAssemblies
            // 
            this.treeViewInternalAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewInternalAssemblies.HideSelection = false;
            this.treeViewInternalAssemblies.Location = new System.Drawing.Point(3, 3);
            this.treeViewInternalAssemblies.Name = "treeViewInternalAssemblies";
            this.treeViewInternalAssemblies.PathSeparator = ".";
            this.treeViewInternalAssemblies.Size = new System.Drawing.Size(230, 538);
            this.treeViewInternalAssemblies.TabIndex = 0;
            this.treeViewInternalAssemblies.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnInternalAssembyNodeSelected);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tabControlAssemblies);
            this.splitContainer.Panel1.Controls.Add(this.toolStripAssemblies);
            this.splitContainer.Size = new System.Drawing.Size(871, 596);
            this.splitContainer.SplitterDistance = 250;
            this.splitContainer.TabIndex = 0;
            // 
            // tabControlAssemblies
            // 
            this.tabControlAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlAssemblies.Controls.Add(this.tabPageInternal);
            this.tabControlAssemblies.Controls.Add(this.tabPageExternal);
            this.tabControlAssemblies.Location = new System.Drawing.Point(3, 23);
            this.tabControlAssemblies.Name = "tabControlAssemblies";
            this.tabControlAssemblies.SelectedIndex = 0;
            this.tabControlAssemblies.Size = new System.Drawing.Size(244, 570);
            this.tabControlAssemblies.TabIndex = 3;
            // 
            // tabPageInternal
            // 
            this.tabPageInternal.Controls.Add(this.treeViewInternalAssemblies);
            this.tabPageInternal.Location = new System.Drawing.Point(4, 22);
            this.tabPageInternal.Name = "tabPageInternal";
            this.tabPageInternal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInternal.Size = new System.Drawing.Size(236, 544);
            this.tabPageInternal.TabIndex = 0;
            this.tabPageInternal.Text = "Internal";
            this.tabPageInternal.UseVisualStyleBackColor = true;
            // 
            // tabPageExternal
            // 
            this.tabPageExternal.Controls.Add(this.treeViewExternalAssemblies);
            this.tabPageExternal.Location = new System.Drawing.Point(4, 22);
            this.tabPageExternal.Name = "tabPageExternal";
            this.tabPageExternal.Size = new System.Drawing.Size(236, 544);
            this.tabPageExternal.TabIndex = 1;
            this.tabPageExternal.Text = "External";
            this.tabPageExternal.UseVisualStyleBackColor = true;
            // 
            // treeViewExternalAssemblies
            // 
            this.treeViewExternalAssemblies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewExternalAssemblies.HideSelection = false;
            this.treeViewExternalAssemblies.Location = new System.Drawing.Point(0, 0);
            this.treeViewExternalAssemblies.Name = "treeViewExternalAssemblies";
            this.treeViewExternalAssemblies.PathSeparator = ".";
            this.treeViewExternalAssemblies.Size = new System.Drawing.Size(236, 544);
            this.treeViewExternalAssemblies.TabIndex = 1;
            this.treeViewExternalAssemblies.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnExternalAssembyNodeSelected);
            // 
            // toolStripAssemblies
            // 
            this.toolStripAssemblies.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripAssemblies.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton});
            this.toolStripAssemblies.Location = new System.Drawing.Point(0, 0);
            this.toolStripAssemblies.Name = "toolStripAssemblies";
            this.toolStripAssemblies.Size = new System.Drawing.Size(250, 25);
            this.toolStripAssemblies.TabIndex = 2;
            this.toolStripAssemblies.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Load Assembly";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productToolStripMenuItem,
            this.assemblyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(871, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip";
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem,
            this.blogToolStripMenuItem});
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.productToolStripMenuItem.Text = "&Product";
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.informationToolStripMenuItem.Text = "&Information...";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // blogToolStripMenuItem
            // 
            this.blogToolStripMenuItem.Name = "blogToolStripMenuItem";
            this.blogToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.blogToolStripMenuItem.Text = "&Blog";
            this.blogToolStripMenuItem.Click += new System.EventHandler(this.blogToolStripMenuItem_Click);
            // 
            // assemblyToolStripMenuItem
            // 
            this.assemblyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.assemblyToolStripMenuItem.Name = "assemblyToolStripMenuItem";
            this.assemblyToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.assemblyToolStripMenuItem.Text = "&Assembly";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 620);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = ".net SmokeTest";
            this.Load += new System.EventHandler(this.OnLoad);
            this.SizeChanged += new System.EventHandler(this.OnSizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.tabControlAssemblies.ResumeLayout(false);
            this.tabPageInternal.ResumeLayout(false);
            this.tabPageExternal.ResumeLayout(false);
            this.toolStripAssemblies.ResumeLayout(false);
            this.toolStripAssemblies.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		internal System.Windows.Forms.TreeView treeViewInternalAssemblies;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem assemblyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStripAssemblies;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.TabPage tabPageInternal;
		private System.Windows.Forms.TabPage tabPageExternal;
		internal System.Windows.Forms.TreeView treeViewExternalAssemblies;
		internal System.Windows.Forms.TabControl tabControlAssemblies;
        private System.Windows.Forms.ToolStripMenuItem blogToolStripMenuItem;
	}
}

