using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// A user control to contain the controls that present the arguments to a function, one per row.
	/// </summary>
	internal partial class ArgumentsControl : UserControl
	{
		public struct Control
		{
			public struct Index 
			{
				public const int NULL = 0;
				public const int NAME = 1;
				public const int INPUT = 2;
				public const int TYPE = 3;
			};
		}

		public ArgumentsControl()
		{
			InitializeComponent();
			this.Dock = DockStyle.Fill;
		}
	}
}
