using System;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;
using System.Reflection;

[TestFixture]
class FieldControlTestFixture
{
	[Test]
	public void DefaultConstructor()
	{
		FieldControlPanel control = new FieldControlPanel();
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
		Assert.AreEqual("1", control.comboBoxTimes.Text);
	}

	[Test]
	public void DefaultInitialize()
	{
		FieldControlPanel control = new FieldControlPanel();
		control.buttonDrillDown.Visible = true;
		control.buttonAddToPool.Visible = true;
		control.previewControl.textBoxToString.Text = "something";
		control.Initialize();
		Assert.IsNull(control.instance_under_test);
		Assert.IsNull(control.field_under_test);
		Assert.IsFalse(control.buttonRead.Enabled);
		Assert.IsFalse(control.buttonWrite.Enabled);
		Assert.IsFalse(control.buttonDrillDown.Visible);
		Assert.IsFalse(control.buttonAddToPool.Visible);
		Assert.AreEqual(0, control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.AreEqual(0, control.previewControl.textBoxToString.Text.Length);
		control.Hide();
	}

	class TestClass
	{
		public static double public_static_field = 3.141;
	}

	[Test]
	public void FieldInfoInitialize()
	{
		FieldControlPanel control = new FieldControlPanel();
		control.buttonDrillDown.Visible = true;
		control.buttonAddToPool.Visible = true;
		control.previewControl.textBoxToString.Text = "something";
		FieldInfo info = typeof(TestClass).GetField("public_static_field");
		control.Initialize(info);
		Assert.AreSame(info, control.field_under_test);
		Assert.AreEqual(4, control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsTrue(control.buttonRead.Enabled);
		Assert.IsTrue(control.buttonWrite.Enabled);
		Assert.AreNotEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.AreNotEqual(0, control.previewControl.textBoxToString.Text.Length);
		Assert.IsTrue(control.buttonDrillDown.Visible);
		Assert.IsTrue(control.buttonAddToPool.Visible);
		control.Hide();
	}

	[Test]
	public void InitializeDoesRead()
	{
		FieldControlPanel control = new FieldControlPanel();
		FieldInfo info = typeof(TestClass).GetField("public_static_field");
		control.Initialize(info);
		Assert.AreEqual("3.141", control.previewControl.textBoxToString.Text);
		control.Hide();
	}

	[Test]
	public void WritingUpdatesPreviewTabs()
	{
		FieldControlPanel control = new FieldControlPanel();
		FieldInfo info = typeof(TestClass).GetField("public_static_field");
		control.Initialize(info);
		control.buttonWrite_Click(null, new EventArgs());
		Assert.AreEqual("0", control.previewControl.textBoxToString.Text);
		control.Hide();
	}

	[Test]
	public void PlusButtonAddsInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		FieldControlPanel control = new FieldControlPanel();
		FieldInfo info = typeof(TestClass).GetField("public_static_field");
		control.Initialize(info);
		control.OnAddToPool(null, null);
		Assert.IsTrue(ObjectPool.pool.Contains(control.Lastresult));
		control.Hide();
	}

	[Test]
	public void DoNotAddSameInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		FieldControlPanel control = new FieldControlPanel();
		FieldInfo info = typeof(TestClass).GetField("public_static_field");
		control.Initialize(info);
		control.Lastresult = "some object";
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		Assert.AreEqual(1, ObjectPool.pool.Count);
		control.Hide();
	}

	class MultipleInvokeTester : FieldControlPanel
	{
		public MultipleInvokeTester()
			: base()
		{
			ObjectCreated += new ObjectCreatedHandler(OnObjectCreated);
		}

		public int invoked = 0;

		void OnObjectCreated(object instance)
		{
			++invoked;
		}
	}

	[Test]
	public void InvokeReadMultipleTimes()
	{
		MultipleInvokeTester control = new MultipleInvokeTester();
		control.Show();
		FieldInfo info = typeof(TestClass).GetField("public_static_field");
		control.Initialize(info);
		control.InvokeCount = 8;
		control.buttonRead_Click(null, null);
		Assert.AreEqual(9, control.invoked);
	}

	[Test]
	public void UpdatingInvokeTimeUpdatesInvokeCount()
	{
		FieldControlPanel control = new FieldControlPanel();
		control.comboBoxTimes.Text = Convert.ToString(5678);
		control.invoke_box_Validating(null, null);
		Assert.AreEqual(5678, control.InvokeCount);
	}

	[Test]
	public void ResetButtonClearsCountersAndListBox()
	{
		FieldControlPanel control = new FieldControlPanel();
		FieldInfo info = typeof(TestClass).GetField("public_static_field");
		control.Initialize(info);
		control.buttonRead_Click(null, null);
		control.buttonRead_Click(null, null);
		control.buttonRead_Click(null, null);
		control.buttonReset_Click(null, null);
		Assert.AreEqual(0, control.average_count);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
	}
}