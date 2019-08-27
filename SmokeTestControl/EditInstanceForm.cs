using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// A user Form that encapsulates a SmokeTestControl for editing.
	/// </summary>
	public partial class EditInstanceForm : Form
	{
		/// <summary>
		/// Default constructor code for this form.
		/// </summary>
		private void Construct(Type type)
		{
			InitializeComponent();
			this.Text = type.ToString();
		}

		private void OnLoad(object sender, EventArgs e)
		{
		}

		internal MainControl smoketest_control;

		/// <summary>
		/// Instantiate the form based on an Object instance.
		/// </summary>
		/// <param name="instance_under_test">The object instance under test.</param>
		public EditInstanceForm(Object instance)
		{
			Debug.Assert(instance != null);
			Construct(instance.GetType());
			smoketest_control = new MainControl(instance);
			smoketest_control.Dock = DockStyle.Fill;
			Controls.Add(smoketest_control);
		}
	}
}
