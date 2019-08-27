using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal class UnsupportedArgumentControl : Button
	{
		public UnsupportedArgumentControl(Type type)
		{
			Debug.Assert(type != null, "type cannot be null");
			this.Text = MemberControlPanel.NULL_TEXT;
		}
	}
}