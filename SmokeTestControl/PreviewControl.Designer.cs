namespace net.wesleysteiner.SmokeTestControl
{
	partial class PreviewControl
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
			this.textBoxToString = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxToString
			// 
			this.textBoxToString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.textBoxToString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxToString.Location = new System.Drawing.Point(0, 0);
			this.textBoxToString.Multiline = true;
			this.textBoxToString.Name = "textBoxToString";
			this.textBoxToString.ReadOnly = true;
			this.textBoxToString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxToString.Size = new System.Drawing.Size(352, 120);
			this.textBoxToString.TabIndex = 6;
			// 
			// PreviewControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.textBoxToString);
			this.Name = "PreviewControl";
			this.Size = new System.Drawing.Size(352, 120);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.TextBox textBoxToString;

	}
}
