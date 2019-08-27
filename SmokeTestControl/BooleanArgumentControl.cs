using System;
using System.Drawing;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// The UI control for editing Boolean values.
	/// </summary>
	internal class BooleanValueControl : CheckBox
	{
		public BooleanValueControl()
		{
			this.Appearance = Appearance.Button;
			this.TextAlign = ContentAlignment.MiddleCenter;
			this.Text = false.ToString();
			this.CheckedChanged += new EventHandler(OnCheckedChanged);
		}

		void OnCheckedChanged(object sender, EventArgs e)
		{
			this.Text = (this.CheckState == CheckState.Checked).ToString();
		}
	}
}