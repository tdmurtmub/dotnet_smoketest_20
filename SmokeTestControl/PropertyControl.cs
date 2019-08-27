using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal partial class PropertyControlPanel : MemberControlPanel
	{
		public PropertyControlPanel()
		{
			InitializeComponent();
			InitializeCommonControls(this.splitContainer.Panel1, this.comboBoxTimes);
		}

		private void PreInitialize()
		{
			this.arguments_control.tableLayoutPanel.Controls.Clear();
			this.comboBoxMessageBar.Enabled = false;
			this.comboBoxMessageBar.Items.Clear();
			Reset();
			this.previewControl.textBoxToString.Text = String.Empty;
			this.buttonDrillDown.Hide();
			this.buttonAddToPool.Hide();
		}

		/// <summary>
		/// Initialize the control for no selection.
		/// </summary>
		public void Initialize()
		{
			PreInitialize();
			this.instance_under_test = null;
			this.property_under_test = null;
			this.buttonGet.Enabled = false;
			this.buttonSet.Enabled = false;
		}

		/// <summary>
		/// Initialize the control for the given Object instance and PropertyInfo. Instance must be null for static methods.
		/// </summary>
		public void Initialize(Object instance, PropertyInfo info)
		{
			Debug.Assert(info != null);
			PreInitialize();
			this.instance_under_test = instance;
			this.property_under_test = info;
			this.buttonGet.Enabled = property_under_test.CanRead;
			this.buttonSet.Enabled = property_under_test.CanWrite;
			if (this.property_under_test.CanWrite)
			{
				MethodInfo method = property_under_test.GetSetMethod(true);
				PopulateArgumentTable(method.DeclaringType.Assembly, method.GetParameters());
			}
			if (property_under_test.CanRead) buttonGet_Click(null, new EventArgs());
		}

		/// <summary>
		/// Initialize the control for the given static PropertyInfo.
		/// </summary>
		public void Initialize(PropertyInfo info)
		{
			Initialize(null, info);
		}

		internal Object instance_under_test;
		internal PropertyInfo property_under_test;

		internal void buttonGet_Click(object sender, EventArgs e)
		{
			Stopwatch stopwatch = new Stopwatch();
			try
			{
			    stopwatch.Start();
				for (int i = 0; i < InvokeCount; ++i) Lastresult = (property_under_test.CanRead ? property_under_test.GetValue(this.instance_under_test, null) : null);
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
				ProcessLastresult(this.buttonGet.Text, Lastresult, this.buttonDrillDown, this.buttonAddToPool, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds, this.comboBoxMessageBar);
			}
		}

		internal void buttonSet_Click(object sender, EventArgs e)
		{
			Stopwatch stopwatch = new Stopwatch();
			Object argument = GetArguments()[0];
			try
			{
				stopwatch.Start();
				for (int i = 0; i < InvokeCount; ++i)
				{
					property_under_test.SetValue(this.instance_under_test, argument, null);
				}
				stopwatch.Stop();
				Lastresult = argument;
			}
			catch (Exception ex)
			{
				stopwatch.Stop();
				Lastresult = ex;
			}
			finally
			{
				this.previewControl.Update(Lastresult);
				ProcessLastresult(this.buttonSet.Text, Lastresult, this.buttonDrillDown, this.buttonAddToPool, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds, this.comboBoxMessageBar);
			}
		}

		private void buttonPropertiesResult_Click(object sender, EventArgs e)
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
