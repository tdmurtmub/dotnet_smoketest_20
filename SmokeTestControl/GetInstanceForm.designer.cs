namespace net.wesleysteiner.SmokeTestControl
{
	partial class GetInstanceForm
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeTypes = new System.Windows.Forms.TreeView();
			this.comboBoxAssembly = new System.Windows.Forms.ComboBox();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.buttonSelect = new System.Windows.Forms.Button();
			this.listBoxObjects = new System.Windows.Forms.ListBox();
			this.previewControl = new net.wesleysteiner.SmokeTestControl.PreviewControl();
			this.smokeTestControlBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.createInstanceFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smokeTestControlBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.createInstanceFormBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeTypes);
			this.splitContainer1.Panel1.Controls.Add(this.comboBoxAssembly);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
			this.splitContainer1.Size = new System.Drawing.Size(715, 468);
			this.splitContainer1.SplitterDistance = 237;
			this.splitContainer1.TabIndex = 0;
			// 
			// treeTypes
			// 
			this.treeTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.treeTypes.HideSelection = false;
			this.treeTypes.Location = new System.Drawing.Point(0, 30);
			this.treeTypes.Name = "treeTypes";
			this.treeTypes.Size = new System.Drawing.Size(234, 438);
			this.treeTypes.TabIndex = 0;
			this.treeTypes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeTypes_AfterSelect);
			// 
			// comboBoxAssembly
			// 
			this.comboBoxAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxAssembly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAssembly.FormattingEnabled = true;
			this.comboBoxAssembly.Location = new System.Drawing.Point(3, 3);
			this.comboBoxAssembly.Name = "comboBoxAssembly";
			this.comboBoxAssembly.Size = new System.Drawing.Size(231, 21);
			this.comboBoxAssembly.Sorted = true;
			this.comboBoxAssembly.TabIndex = 1;
			this.comboBoxAssembly.SelectedIndexChanged += new System.EventHandler(this.comboBoxAssembly_SelectedIndexChanged);
			// 
			// tabControl2
			// 
			this.tabControl2.Controls.Add(this.tabPage1);
			this.tabControl2.Controls.Add(this.tabPage2);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.Location = new System.Drawing.Point(0, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(474, 468);
			this.tabControl2.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(466, 442);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "New Instance";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.previewControl);
			this.tabPage2.Controls.Add(this.buttonSelect);
			this.tabPage2.Controls.Add(this.listBoxObjects);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(466, 442);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Object Pool";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// buttonSelect
			// 
			this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonSelect.Enabled = false;
			this.buttonSelect.Location = new System.Drawing.Point(383, 411);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(75, 23);
			this.buttonSelect.TabIndex = 1;
			this.buttonSelect.Text = "Select";
			this.buttonSelect.UseVisualStyleBackColor = true;
			this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
			// 
			// listBoxObjects
			// 
			this.listBoxObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxObjects.FormattingEnabled = true;
			this.listBoxObjects.IntegralHeight = false;
			this.listBoxObjects.Location = new System.Drawing.Point(3, 3);
			this.listBoxObjects.Name = "listBoxObjects";
			this.listBoxObjects.Size = new System.Drawing.Size(460, 211);
			this.listBoxObjects.TabIndex = 0;
			this.listBoxObjects.SelectedIndexChanged += new System.EventHandler(this.listBoxObjects_SelectedIndexChanged);
			this.listBoxObjects.DoubleClick += new System.EventHandler(this.listBoxObjects_DoubleClick);
			// 
			// previewControl
			// 
			this.previewControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.previewControl.Location = new System.Drawing.Point(3, 220);
			this.previewControl.Name = "previewControl";
			this.previewControl.Size = new System.Drawing.Size(460, 185);
			this.previewControl.TabIndex = 2;
			// 
			// smokeTestControlBindingSource
			// 
			this.smokeTestControlBindingSource.DataSource = typeof(net.wesleysteiner.SmokeTestControl.MainControl);
			// 
			// createInstanceFormBindingSource
			// 
			this.createInstanceFormBindingSource.DataSource = typeof(net.wesleysteiner.SmokeTestControl.GetInstanceForm);
			// 
			// GetInstanceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(715, 468);
			this.Controls.Add(this.splitContainer1);
			this.Name = "GetInstanceForm";
			this.Text = "Get Instance";
			this.Load += new System.EventHandler(this.GetInstanceForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetInstanceForm_FormClosing);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smokeTestControlBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.createInstanceFormBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		internal System.Windows.Forms.ComboBox comboBoxAssembly;
		private System.Windows.Forms.BindingSource smokeTestControlBindingSource;
		private System.Windows.Forms.BindingSource createInstanceFormBindingSource;
		internal System.Windows.Forms.TreeView treeTypes;
		internal System.Windows.Forms.TabControl tabControl2;
		internal System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		internal System.Windows.Forms.ListBox listBoxObjects;
		internal System.Windows.Forms.Button buttonSelect;
		private PreviewControl previewControl;
	}
}