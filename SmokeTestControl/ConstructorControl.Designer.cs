namespace net.wesleysteiner.SmokeTestControl
{
	partial class ConstructorControlPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.buttonReset = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxTimes = new System.Windows.Forms.ComboBox();
			this.buttonAddToPool = new System.Windows.Forms.Button();
			this.comboBoxMessageBar = new System.Windows.Forms.ComboBox();
			this.previewControl = new net.wesleysteiner.SmokeTestControl.PreviewControl();
			this.buttonDrillDown = new System.Windows.Forms.Button();
			this.buttonCreate = new System.Windows.Forms.Button();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.buttonReset);
			this.splitContainer.Panel2.Controls.Add(this.label1);
			this.splitContainer.Panel2.Controls.Add(this.comboBoxTimes);
			this.splitContainer.Panel2.Controls.Add(this.buttonAddToPool);
			this.splitContainer.Panel2.Controls.Add(this.comboBoxMessageBar);
			this.splitContainer.Panel2.Controls.Add(this.previewControl);
			this.splitContainer.Panel2.Controls.Add(this.buttonDrillDown);
			this.splitContainer.Panel2.Controls.Add(this.buttonCreate);
			this.splitContainer.Size = new System.Drawing.Size(400, 300);
			this.splitContainer.SplitterDistance = 142;
			this.splitContainer.TabIndex = 0;
			// 
			// buttonReset
			// 
			this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReset.Location = new System.Drawing.Point(341, 27);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(56, 23);
			this.buttonReset.TabIndex = 5;
			this.buttonReset.Text = "Reset";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(69, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "X";
			// 
			// comboBoxTimes
			// 
			this.comboBoxTimes.FormattingEnabled = true;
			this.comboBoxTimes.Items.AddRange(new object[] {
            "1",
            "2",
            "10",
            "100",
            "1000",
            "10000",
            "100000",
            "1000000"});
			this.comboBoxTimes.Location = new System.Drawing.Point(89, 5);
			this.comboBoxTimes.Name = "comboBoxTimes";
			this.comboBoxTimes.Size = new System.Drawing.Size(86, 21);
			this.comboBoxTimes.TabIndex = 1;
			// 
			// buttonAddToPool
			// 
			this.buttonAddToPool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddToPool.Location = new System.Drawing.Point(365, 3);
			this.buttonAddToPool.Name = "buttonAddToPool";
			this.buttonAddToPool.Size = new System.Drawing.Size(32, 23);
			this.buttonAddToPool.TabIndex = 3;
			this.buttonAddToPool.Text = "+";
			this.buttonAddToPool.UseVisualStyleBackColor = true;
			this.buttonAddToPool.Visible = false;
			this.buttonAddToPool.Click += new System.EventHandler(this.OnAddToPool);
			// 
			// comboBoxMessageBar
			// 
			this.comboBoxMessageBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMessageBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMessageBar.Enabled = false;
			this.comboBoxMessageBar.FormattingEnabled = true;
			this.comboBoxMessageBar.Location = new System.Drawing.Point(3, 28);
			this.comboBoxMessageBar.MaxDropDownItems = 25;
			this.comboBoxMessageBar.Name = "comboBoxMessageBar";
			this.comboBoxMessageBar.Size = new System.Drawing.Size(335, 21);
			this.comboBoxMessageBar.TabIndex = 4;
			// 
			// previewControl
			// 
			this.previewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.previewControl.Location = new System.Drawing.Point(3, 53);
			this.previewControl.Name = "previewControl";
			this.previewControl.Size = new System.Drawing.Size(394, 98);
			this.previewControl.TabIndex = 6;
			// 
			// buttonDrillDown
			// 
			this.buttonDrillDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDrillDown.AutoSize = true;
			this.buttonDrillDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonDrillDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonDrillDown.Location = new System.Drawing.Point(309, 3);
			this.buttonDrillDown.Name = "buttonDrillDown";
			this.buttonDrillDown.Size = new System.Drawing.Size(54, 23);
			this.buttonDrillDown.TabIndex = 2;
			this.buttonDrillDown.Text = "<result>";
			this.buttonDrillDown.UseVisualStyleBackColor = true;
			this.buttonDrillDown.Visible = false;
			this.buttonDrillDown.Click += new System.EventHandler(this.OnResultClick);
			// 
			// buttonCreate
			// 
			this.buttonCreate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonCreate.Enabled = false;
			this.buttonCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonCreate.Location = new System.Drawing.Point(3, 3);
			this.buttonCreate.Name = "buttonCreate";
			this.buttonCreate.Size = new System.Drawing.Size(61, 23);
			this.buttonCreate.TabIndex = 0;
			this.buttonCreate.Text = "Create";
			this.buttonCreate.UseVisualStyleBackColor = true;
			this.buttonCreate.Click += new System.EventHandler(this.OnCreateClick);
			// 
			// ConstructorControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "ConstructorControlPanel";
			this.Size = new System.Drawing.Size(400, 300);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		internal System.Windows.Forms.Button buttonCreate;
		internal System.Windows.Forms.Button buttonDrillDown;
		internal PreviewControl previewControl;
		internal System.Windows.Forms.ComboBox comboBoxMessageBar;
		internal System.Windows.Forms.Button buttonAddToPool;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ComboBox comboBoxTimes;
		private System.Windows.Forms.Button buttonReset;
	}
}
