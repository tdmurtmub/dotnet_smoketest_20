using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal class EnumValueControl : ComboBox
	{
		private Type type;

		public EnumValueControl(Type type)
		{
			this.type = type;
			this.Enabled = true;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			this.Items.AddRange(Enum.GetNames(type));
			this.SelectedIndexChanged += new EventHandler(OnSelectedIndexChanged);
			this.SelectedIndex = 0;
		}

		void OnSelectedIndexChanged(object sender, EventArgs e)
		{
			this.Tag = Enum.Parse(this.type, this.Text, true);
		}
	}
}