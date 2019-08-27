using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// The UI control to create an argument of a given Type.
	/// </summary>
	internal partial class ArgumentTypeControl : Button
	{
		private Control target_control;
		private Assembly assembly_under_test;

		private void OnClick(object sender, EventArgs e)
		{
			Type type = this.Tag as Type;
			GetInstanceForm form = new GetInstanceForm(Utility.FilterAssemblies(AppDomain.CurrentDomain.GetAssemblies(), type), this.assembly_under_test, type);
			form.ShowDialog();
			ProcessOnClickResult(form.Instance);
		}

		/// <summary>
		/// Process the instance created, if any, from the last OnClick invocation.
		/// </summary>
		internal void ProcessOnClickResult(Object instance)
		{
			if (instance != null)
			{
				target_control.Tag = instance;
				target_control.Enabled = true;
				target_control.Text = Utility.CreateEditableInstanceText(instance);
			}
		}

		/// <summary>
		/// Constructs a 'control' for the argument Type and target Value control.
		/// </summary>
		public ArgumentTypeControl(Assembly assembly, Type type, Control target)
		{
			Debug.Assert(assembly != null);
			Debug.Assert(type != null);
			Debug.Assert(target != null);
			this.assembly_under_test = assembly;
			this.target_control = target;
			this.Tag = type;
			this.Text = Utility.GetFriendlyTypeName(type) + "...";
			this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Enabled = Utility.IsConstructable(type);
			this.AutoSize = true;
			this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
			this.Click += new EventHandler(OnClick);
		}
	}
}
