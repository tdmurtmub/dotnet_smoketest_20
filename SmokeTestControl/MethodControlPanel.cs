using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal partial class MethodControlPanel : MemberControlPanel
	{
		internal Object instance_under_test;
		internal MethodInfo method_under_test;

		public MethodControlPanel()
		{
			InitializeComponent();
			InitializeCommonControls(this.splitContainer.Panel1, this.comboBoxTimes);
		}

		private void PreInitialize()
		{
			this.buttonDrillDown.Hide();
			this.buttonAddToPool.Hide();
			this.comboBoxMessageBar.Enabled = false;
			this.comboBoxMessageBar.Items.Clear();
			Reset();
			this.previewControl.textBoxToString.Text = String.Empty;
		}

		/// <summary>
		/// Initialize the control for no selection.
		/// </summary>
		public void Initialize()
		{
			PreInitialize();
			this.instance_under_test = null;
			this.method_under_test = null;
			this.arguments_control.tableLayoutPanel.Controls.Clear();
			this.buttonCall.Enabled = false;
		}

		/// <summary>
		/// Initialize the control for the given Object instance and MethodInfo. Instance must be null for static methods.
		/// </summary>
		public void Initialize(Object instance, MethodInfo info)
		{
			PreInitialize();
			this.instance_under_test = instance;
			this.method_under_test = info;
			this.arguments_control.tableLayoutPanel.Controls.Clear();
			PopulateArgumentTable(info.DeclaringType.Assembly, info.GetParameters());
			this.buttonCall.Enabled = true;
		}

		/// <summary>
		/// Initialize the control for the given static MethodInfo.
		/// </summary>
		public void Initialize(MethodInfo info)
		{
			Initialize(null, info);
		}

		internal void buttonCall_Click(object sender, EventArgs e)
		{
			Stopwatch stopwatch = new Stopwatch();
			Object[] arguments = GetArguments();
			try
			{
				stopwatch.Start();
				for (int i = 0; i < InvokeCount; ++i) Lastresult = method_under_test.Invoke(this.instance_under_test, arguments);
				stopwatch.Stop();
			}
			catch (Exception ex)
			{
				stopwatch.Stop();
				Lastresult = ex;
			}
			finally
			{
				this.previewControl.Update(Lastresult);
				ProcessLastresult(this.buttonCall.Text, Lastresult, this.buttonDrillDown, this.buttonAddToPool, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds, this.comboBoxMessageBar);
			}
		}

		private void buttonMethodsResult_Click(object sender, EventArgs e)
		{
			EditInstanceForm form = new EditInstanceForm(Lastresult);
			form.Show(this);
		}

		internal void OnAddToPool(object sender, EventArgs e)
		{
			AddInstanceToPool(this.comboBoxMessageBar);
		}

		internal void buttonReset_Click(object sender, EventArgs e)
		{
			ResetCounters(this.comboBoxMessageBar);
		}
	}
}
