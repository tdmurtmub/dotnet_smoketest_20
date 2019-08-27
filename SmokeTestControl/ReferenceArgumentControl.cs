using System;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// The UI control for reference type argument values.
	/// </summary>
	internal class ReferenceValueControl : Button
	{
		private void OnClick(object sender, EventArgs e)
		{
			EditInstanceForm form = new EditInstanceForm(this.Tag);
			form.ShowDialog();
		}

		public ReferenceValueControl(Type type)
		{
			this.Click += new EventHandler(OnClick);
		}
	}
}