namespace net.wesleysteiner.SmokeTestControl
{
	partial class FieldControlPanel
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
			this.buttonAddToPool = new System.Windows.Forms.Button();
			this.comboBoxTimes = new System.Windows.Forms.ComboBox();
			this.comboBoxMessageBar = new System.Windows.Forms.ComboBox();
			this.previewControl = new net.wesleysteiner.SmokeTestControl.PreviewControl();
			this.buttonRead = new System.Windows.Forms.Button();
			this.buttonDrillDown = new System.Windows.Forms.Button();
			this.buttonWrite = new System.Windows.Forms.Button();
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
			this.splitContainer.Panel2.Controls.Add(this.buttonAddToPool);
			this.splitContainer.Panel2.Controls.Add(this.comboBoxTimes);
			this.splitContainer.Panel2.Controls.Add(this.comboBoxMessageBar);
			this.splitContainer.Panel2.Controls.Add(this.previewControl);
			this.splitContainer.Panel2.Controls.Add(this.buttonRead);
			this.splitContainer.Panel2.Controls.Add(this.buttonDrillDown);
			this.splitContainer.Panel2.Controls.Add(this.buttonWrite);
			this.splitContainer.Size = new System.Drawing.Size(400, 300);
			this.splitContainer.SplitterDistance = 60;
			this.splitContainer.TabIndex = 1;
			// 
			// buttonReset
			// 
			this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReset.Location = new System.Drawing.Point(341, 28);
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.Size = new System.Drawing.Size(56, 23);
			this.buttonReset.TabIndex = 6;
			this.buttonReset.Text = "Reset";
			this.buttonReset.UseVisualStyleBackColor = true;
			this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(112, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 18;
			this.label1.Text = "X";
			// 
			// buttonAddToPool
			// 
			this.buttonAddToPool.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonAddToPool.Location = new System.Drawing.Point(365, 2);
			this.buttonAddToPool.Name = "buttonAddToPool";
			this.buttonAddToPool.Size = new System.Drawing.Size(32, 23);
			this.buttonAddToPool.TabIndex = 4;
			this.buttonAddToPool.Text = "+";
			this.buttonAddToPool.UseVisualStyleBackColor = true;
			this.buttonAddToPool.Visible = false;
			this.buttonAddToPool.Click += new System.EventHandler(this.OnAddToPool);
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
			this.comboBoxTimes.Location = new System.Drawing.Point(131, 4);
			this.comboBoxTimes.Name = "comboBoxTimes";
			this.comboBoxTimes.Size = new System.Drawing.Size(86, 21);
			this.comboBoxTimes.TabIndex = 2;
			// 
			// comboBoxMessageBar
			// 
			this.comboBoxMessageBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMessageBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMessageBar.Enabled = false;
			this.comboBoxMessageBar.FormattingEnabled = true;
			this.comboBoxMessageBar.Location = new System.Drawing.Point(3, 29);
			this.comboBoxMessageBar.MaxDropDownItems = 25;
			this.comboBoxMessageBar.Name = "comboBoxMessageBar";
			this.comboBoxMessageBar.Size = new System.Drawing.Size(335, 21);
			this.comboBoxMessageBar.TabIndex = 5;
			// 
			// previewControl
			// 
			this.previewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.previewControl.Location = new System.Drawing.Point(3, 55);
			this.previewControl.Name = "previewControl";
			this.previewControl.Size = new System.Drawing.Size(394, 178);
			this.previewControl.TabIndex = 7;
			// 
			// buttonRead
			// 
			this.buttonRead.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonRead.Enabled = false;
			this.buttonRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonRead.Location = new System.Drawing.Point(3, 2);
			this.buttonRead.Name = "buttonRead";
			this.buttonRead.Size = new System.Drawing.Size(51, 23);
			this.buttonRead.TabIndex = 0;
			this.buttonRead.Text = "Read";
			this.buttonRead.UseVisualStyleBackColor = true;
			this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
			// 
			// buttonDrillDown
			// 
			this.buttonDrillDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonDrillDown.AutoSize = true;
			this.buttonDrillDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonDrillDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonDrillDown.Location = new System.Drawing.Point(321, 2);
			this.buttonDrillDown.Name = "buttonDrillDown";
			this.buttonDrillDown.Size = new System.Drawing.Size(42, 23);
			this.buttonDrillDown.TabIndex = 3;
			this.buttonDrillDown.Text = "result";
			this.buttonDrillDown.UseVisualStyleBackColor = true;
			this.buttonDrillDown.Visible = false;
			this.buttonDrillDown.Click += new System.EventHandler(this.buttonFieldsResult_Click);
			// 
			// buttonWrite
			// 
			this.buttonWrite.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonWrite.Enabled = false;
			this.buttonWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.buttonWrite.Location = new System.Drawing.Point(55, 2);
			this.buttonWrite.Name = "buttonWrite";
			this.buttonWrite.Size = new System.Drawing.Size(52, 23);
			this.buttonWrite.TabIndex = 1;
			this.buttonWrite.Text = "Write";
			this.buttonWrite.UseVisualStyleBackColor = true;
			this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
			// 
			// FieldControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Name = "FieldControlPanel";
			this.Size = new System.Drawing.Size(400, 300);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			this.splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		internal System.Windows.Forms.Button buttonDrillDown;
		internal System.Windows.Forms.Button buttonRead;
		internal System.Windows.Forms.Button buttonWrite;
		internal PreviewControl previewControl;
		internal System.Windows.Forms.ComboBox comboBoxMessageBar;
		internal System.Windows.Forms.Button buttonAddToPool;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ComboBox comboBoxTimes;
		private System.Windows.Forms.Button buttonReset;
	}
}
