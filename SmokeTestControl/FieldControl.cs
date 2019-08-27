using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal partial class FieldControlPanel : MemberControlPanel
	{
		internal FieldInfo field_under_test;
		internal Object instance_under_test;

		public FieldControlPanel()
		{
			InitializeComponent();
			InitializeCommonControls(this.splitContainer.Panel1, this.comboBoxTimes);
		}

		private void PreInitialize()
		{
			this.arguments_control.tableLayoutPanel.Controls.Clear();
			this.buttonDrillDown.Visible = false;
			this.buttonAddToPool.Visible = false;
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
			this.field_under_test = null;
			this.buttonRead.Enabled = false;
			this.buttonWrite.Enabled = false;
		}

		/// <summary>
		/// Initialize the control for the given Object instance and FieldInfo. Instance must be null for static members.
		/// </summary>
		public void Initialize(Object instance, FieldInfo info)
		{
			Debug.Assert(info != null);
			PreInitialize();
			this.instance_under_test = instance;
			this.field_under_test = info;
			this.buttonRead.Enabled = true;
			this.buttonWrite.Enabled = (!info.IsLiteral && !info.IsInitOnly);
			if (this.buttonWrite.Enabled)
			{
				PopulateNextArgumentRow(info.FieldType.Assembly, info.FieldType, info.Name);
			}
			buttonRead_Click(null, new EventArgs());
		}

		/// <summary>
		/// Initialize the control for the given FieldInfo.
		/// </summary>
		public void Initialize(FieldInfo info)
		{
			Initialize(null, info);
		}

		internal void buttonRead_Click(object sender, EventArgs e)
		{
			Stopwatch stopwatch = new Stopwatch();
			try
			{
				FieldInfo field = this.field_under_test as FieldInfo;
				stopwatch.Start();
				for (int i = 0; i < InvokeCount; ++i) Lastresult = field.GetValue(this.instance_under_test);
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
				ProcessLastresult(this.buttonRead.Text, Lastresult, this.buttonDrillDown, this.buttonAddToPool, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds, this.comboBoxMessageBar);
			}
		}

		internal void buttonWrite_Click(object sender, EventArgs e)
		{
			Stopwatch stopwatch = new Stopwatch();
			try
			{
				FieldInfo field = this.field_under_test as FieldInfo;
				Object argument = GetArguments()[0];
				stopwatch.Start();
				for (int i = 0; i < InvokeCount; ++i) field.SetValue(this.instance_under_test, argument);
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
				ProcessLastresult(this.buttonWrite.Text, Lastresult, this.buttonDrillDown, this.buttonAddToPool, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds, this.comboBoxMessageBar);
			}
		}

		private void buttonFieldsResult_Click(object sender, EventArgs e)
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
