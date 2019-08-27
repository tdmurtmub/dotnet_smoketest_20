using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal partial class PreviewControl : UserControl
	{
		public PreviewControl()
		{
			InitializeComponent();
		}

		public void Update(Object instance)
		{
			this.textBoxToString.Text = (instance == null) ?MemberControlPanel.NULL_TEXT :instance.ToString();
		}
	}
}
