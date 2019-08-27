using System;
using System.Diagnostics;
using System.Reflection; 
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	internal partial class ConstructorControlPanel : MemberControlPanel
	{
		private void OnResultClick(object sender, EventArgs e)
		{
			EditInstanceForm form = new EditInstanceForm(Lastresult);
			form.Show(this);
		}

		internal ConstructorInfo constructor_info;

		internal void OnCreateClick(object sender, EventArgs e)
		{
			Stopwatch stopwatch = new Stopwatch();
			Object[] arguments = GetArguments();
			try
			{
				stopwatch.Start();
				for (int i = 0; i < InvokeCount; ++i) Lastresult = (this.constructor_info).Invoke(arguments);
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
				ProcessLastresult(this.buttonCreate.Text, Lastresult, this.buttonDrillDown, this.buttonAddToPool, stopwatch.ElapsedTicks, stopwatch.ElapsedMilliseconds, this.comboBoxMessageBar);
			}
		}

		public ConstructorControlPanel()
		{
			InitializeComponent();
			InitializeCommonControls(this.splitContainer.Panel1, this.comboBoxTimes);
		}

		private void PreInitialize()
		{
			this.buttonDrillDown.Hide();
			this.buttonAddToPool.Hide();
			this.previewControl.textBoxToString.Text = String.Empty;
			this.comboBoxMessageBar.Enabled = false;
			this.comboBoxMessageBar.Items.Clear();
			Reset();
		}

		/// <summary>
		/// Initialize the control for no selection.
		/// </summary>
		public void Initialize()
		{
			PreInitialize();
			this.constructor_info = null;
			this.arguments_control.tableLayoutPanel.Controls.Clear();
			this.buttonCreate.Enabled = false;
		}

		/// <summary>
		/// Initialize the control for the given constructor.
		/// </summary>
		public void Initialize(ConstructorInfo info)
		{
			PreInitialize();
			this.constructor_info = info;
			PopulateArgumentTable(info.DeclaringType.Assembly, info.GetParameters());
			this.buttonCreate.Enabled = true;
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
