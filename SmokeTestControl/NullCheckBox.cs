using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal class NullCheckBox : CheckBox
	{
		private Control input_control;
		private Button type_button;

		private void EnableTargets()
		{
			this.input_control.Enabled = this.Checked && Utility.IsEditable((Type)this.type_button.Tag, input_control.Tag);
			this.type_button.Enabled = this.Checked && Utility.IsConstructable((Type)this.type_button.Tag);
		}

		internal void OnClicked(object sender, EventArgs e)
		{
			EnableTargets();
			this.input_control.Select();
		}

		public NullCheckBox(Control target, Button button, bool enabled)
		{
			Debug.Assert(target != null);
			Debug.Assert(button != null);
			this.input_control = target;
			this.type_button = button;
			this.Appearance = Appearance.Normal;
			this.TextAlign = ContentAlignment.MiddleCenter;
			this.Enabled = enabled;
			this.Checked = enabled;
			this.Click += new EventHandler(OnClicked);
		}
	}
}