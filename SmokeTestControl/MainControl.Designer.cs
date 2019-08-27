namespace net.wesleysteiner.SmokeTestControl
{
	partial class MainControl
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
			this.tabControlMembers = new System.Windows.Forms.TabControl();
			this.constructors_TabPage = new System.Windows.Forms.TabPage();
			this.splitContainerConstructors = new System.Windows.Forms.SplitContainer();
			this.listBoxConstructors = new System.Windows.Forms.ListBox();
			this.methods_TabPage = new System.Windows.Forms.TabPage();
			this.splitContainerMethods = new System.Windows.Forms.SplitContainer();
			this.listBoxMethods = new System.Windows.Forms.ListBox();
			this.properties_TabPage = new System.Windows.Forms.TabPage();
			this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
			this.listBoxProperties = new System.Windows.Forms.ListBox();
			this.fields_TabPage = new System.Windows.Forms.TabPage();
			this.splitContainerFields = new System.Windows.Forms.SplitContainer();
			this.listBoxFields = new System.Windows.Forms.ListBox();
			this.tabControlMembers.SuspendLayout();
			this.constructors_TabPage.SuspendLayout();
			this.splitContainerConstructors.Panel1.SuspendLayout();
			this.splitContainerConstructors.SuspendLayout();
			this.methods_TabPage.SuspendLayout();
			this.splitContainerMethods.Panel1.SuspendLayout();
			this.splitContainerMethods.SuspendLayout();
			this.properties_TabPage.SuspendLayout();
			this.splitContainerProperties.Panel1.SuspendLayout();
			this.splitContainerProperties.SuspendLayout();
			this.fields_TabPage.SuspendLayout();
			this.splitContainerFields.Panel1.SuspendLayout();
			this.splitContainerFields.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlMembers
			// 
			this.tabControlMembers.Controls.Add(this.constructors_TabPage);
			this.tabControlMembers.Controls.Add(this.methods_TabPage);
			this.tabControlMembers.Controls.Add(this.properties_TabPage);
			this.tabControlMembers.Controls.Add(this.fields_TabPage);
			this.tabControlMembers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabControlMembers.Location = new System.Drawing.Point(0, 0);
			this.tabControlMembers.Name = "tabControlMembers";
			this.tabControlMembers.SelectedIndex = 0;
			this.tabControlMembers.Size = new System.Drawing.Size(475, 472);
			this.tabControlMembers.TabIndex = 0;
			// 
			// constructors_TabPage
			// 
			this.constructors_TabPage.BackColor = System.Drawing.SystemColors.Control;
			this.constructors_TabPage.Controls.Add(this.splitContainerConstructors);
			this.constructors_TabPage.Location = new System.Drawing.Point(4, 22);
			this.constructors_TabPage.Name = "constructors_TabPage";
			this.constructors_TabPage.Size = new System.Drawing.Size(467, 446);
			this.constructors_TabPage.TabIndex = 3;
			this.constructors_TabPage.Text = "Constructors";
			// 
			// splitContainerConstructors
			// 
			this.splitContainerConstructors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerConstructors.Location = new System.Drawing.Point(0, 0);
			this.splitContainerConstructors.Name = "splitContainerConstructors";
			this.splitContainerConstructors.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerConstructors.Panel1
			// 
			this.splitContainerConstructors.Panel1.Controls.Add(this.listBoxConstructors);
			this.splitContainerConstructors.Size = new System.Drawing.Size(467, 446);
			this.splitContainerConstructors.SplitterDistance = 117;
			this.splitContainerConstructors.TabIndex = 3;
			// 
			// listBoxConstructors
			// 
			this.listBoxConstructors.BackColor = System.Drawing.Color.White;
			this.listBoxConstructors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxConstructors.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxConstructors.FormattingEnabled = true;
			this.listBoxConstructors.IntegralHeight = false;
			this.listBoxConstructors.Location = new System.Drawing.Point(0, 0);
			this.listBoxConstructors.Name = "listBoxConstructors";
			this.listBoxConstructors.Size = new System.Drawing.Size(467, 117);
			this.listBoxConstructors.Sorted = true;
			this.listBoxConstructors.TabIndex = 2;
			this.listBoxConstructors.SelectedIndexChanged += new System.EventHandler(this.listBoxConstructors_SelectedIndexChanged);
			// 
			// methods_TabPage
			// 
			this.methods_TabPage.BackColor = System.Drawing.SystemColors.Control;
			this.methods_TabPage.Controls.Add(this.splitContainerMethods);
			this.methods_TabPage.Location = new System.Drawing.Point(4, 22);
			this.methods_TabPage.Name = "methods_TabPage";
			this.methods_TabPage.Size = new System.Drawing.Size(467, 446);
			this.methods_TabPage.TabIndex = 0;
			this.methods_TabPage.Text = "Methods";
			// 
			// splitContainerMethods
			// 
			this.splitContainerMethods.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMethods.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMethods.Name = "splitContainerMethods";
			this.splitContainerMethods.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerMethods.Panel1
			// 
			this.splitContainerMethods.Panel1.Controls.Add(this.listBoxMethods);
			this.splitContainerMethods.Size = new System.Drawing.Size(467, 446);
			this.splitContainerMethods.SplitterDistance = 117;
			this.splitContainerMethods.TabIndex = 2;
			// 
			// listBoxMethods
			// 
			this.listBoxMethods.BackColor = System.Drawing.Color.White;
			this.listBoxMethods.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxMethods.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxMethods.FormattingEnabled = true;
			this.listBoxMethods.IntegralHeight = false;
			this.listBoxMethods.Location = new System.Drawing.Point(0, 0);
			this.listBoxMethods.Name = "listBoxMethods";
			this.listBoxMethods.Size = new System.Drawing.Size(467, 117);
			this.listBoxMethods.Sorted = true;
			this.listBoxMethods.TabIndex = 1;
			this.listBoxMethods.SelectedIndexChanged += new System.EventHandler(this.listBoxMethods_SelectedIndexChanged);
			// 
			// properties_TabPage
			// 
			this.properties_TabPage.BackColor = System.Drawing.SystemColors.Control;
			this.properties_TabPage.Controls.Add(this.splitContainerProperties);
			this.properties_TabPage.Location = new System.Drawing.Point(4, 22);
			this.properties_TabPage.Name = "properties_TabPage";
			this.properties_TabPage.Size = new System.Drawing.Size(467, 446);
			this.properties_TabPage.TabIndex = 1;
			this.properties_TabPage.Text = "Properties";
			// 
			// splitContainerProperties
			// 
			this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
			this.splitContainerProperties.Name = "splitContainerProperties";
			this.splitContainerProperties.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerProperties.Panel1
			// 
			this.splitContainerProperties.Panel1.Controls.Add(this.listBoxProperties);
			this.splitContainerProperties.Size = new System.Drawing.Size(467, 446);
			this.splitContainerProperties.SplitterDistance = 249;
			this.splitContainerProperties.TabIndex = 3;
			// 
			// listBoxProperties
			// 
			this.listBoxProperties.BackColor = System.Drawing.Color.White;
			this.listBoxProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxProperties.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxProperties.FormattingEnabled = true;
			this.listBoxProperties.IntegralHeight = false;
			this.listBoxProperties.Location = new System.Drawing.Point(0, 0);
			this.listBoxProperties.Name = "listBoxProperties";
			this.listBoxProperties.Size = new System.Drawing.Size(467, 249);
			this.listBoxProperties.Sorted = true;
			this.listBoxProperties.TabIndex = 2;
			this.listBoxProperties.SelectedIndexChanged += new System.EventHandler(this.listBoxProperties_SelectedIndexChanged);
			// 
			// fields_TabPage
			// 
			this.fields_TabPage.BackColor = System.Drawing.SystemColors.Control;
			this.fields_TabPage.Controls.Add(this.splitContainerFields);
			this.fields_TabPage.Location = new System.Drawing.Point(4, 22);
			this.fields_TabPage.Name = "fields_TabPage";
			this.fields_TabPage.Size = new System.Drawing.Size(467, 446);
			this.fields_TabPage.TabIndex = 2;
			this.fields_TabPage.Text = "Fields";
			// 
			// splitContainerFields
			// 
			this.splitContainerFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerFields.Location = new System.Drawing.Point(0, 0);
			this.splitContainerFields.Name = "splitContainerFields";
			this.splitContainerFields.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerFields.Panel1
			// 
			this.splitContainerFields.Panel1.Controls.Add(this.listBoxFields);
			this.splitContainerFields.Size = new System.Drawing.Size(467, 446);
			this.splitContainerFields.SplitterDistance = 249;
			this.splitContainerFields.TabIndex = 3;
			// 
			// listBoxFields
			// 
			this.listBoxFields.BackColor = System.Drawing.Color.White;
			this.listBoxFields.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxFields.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxFields.FormattingEnabled = true;
			this.listBoxFields.IntegralHeight = false;
			this.listBoxFields.Location = new System.Drawing.Point(0, 0);
			this.listBoxFields.Name = "listBoxFields";
			this.listBoxFields.Size = new System.Drawing.Size(467, 249);
			this.listBoxFields.Sorted = true;
			this.listBoxFields.TabIndex = 2;
			this.listBoxFields.SelectedIndexChanged += new System.EventHandler(this.listBoxFields_SelectedIndexChanged);
			// 
			// SmokeTestControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControlMembers);
			this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "SmokeTestControl";
			this.Size = new System.Drawing.Size(475, 472);
			this.Load += new System.EventHandler(this.OnLoad);
			this.tabControlMembers.ResumeLayout(false);
			this.constructors_TabPage.ResumeLayout(false);
			this.splitContainerConstructors.Panel1.ResumeLayout(false);
			this.splitContainerConstructors.ResumeLayout(false);
			this.methods_TabPage.ResumeLayout(false);
			this.splitContainerMethods.Panel1.ResumeLayout(false);
			this.splitContainerMethods.ResumeLayout(false);
			this.properties_TabPage.ResumeLayout(false);
			this.splitContainerProperties.Panel1.ResumeLayout(false);
			this.splitContainerProperties.ResumeLayout(false);
			this.fields_TabPage.ResumeLayout(false);
			this.splitContainerFields.Panel1.ResumeLayout(false);
			this.splitContainerFields.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.TabControl tabControlMembers;
		private System.Windows.Forms.TabPage constructors_TabPage;
		internal System.Windows.Forms.ListBox listBoxConstructors;
		private System.Windows.Forms.TabPage methods_TabPage;
		private System.Windows.Forms.SplitContainer splitContainerMethods;
		internal System.Windows.Forms.ListBox listBoxMethods;
		private System.Windows.Forms.TabPage properties_TabPage;
		private System.Windows.Forms.SplitContainer splitContainerProperties;
		internal System.Windows.Forms.ListBox listBoxProperties;
		private System.Windows.Forms.TabPage fields_TabPage;
		private System.Windows.Forms.SplitContainer splitContainerFields;
		internal System.Windows.Forms.ListBox listBoxFields;
		internal System.Windows.Forms.SplitContainer splitContainerConstructors;



	}
}
