using System;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// A control that requires user input to define the value (int, double, string).
	/// </summary>
	internal class LiteralValueControl<T> : TextBox
	{
		public LiteralValueControl(string initial_value)
		{
			Initialize(initial_value);
		}

		public LiteralValueControl(string initial_value, int max_length)
		{
			Initialize(initial_value);
			this.MaxLength = max_length;
		}

		internal Type type;

		private void Initialize(string initial_value)
		{
			this.type = typeof(T);
			this.Enabled = true;
			this.Text = initial_value;
		}
	}

	internal class StringValueControl : LiteralValueControl<String>
	{
		public StringValueControl(string initial_value) : base(initial_value)
		{
			this.TextAlign = HorizontalAlignment.Left;
		}
	}
}