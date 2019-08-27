using System;
using System.Reflection;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class ConstructorControlTestFixture
{
	[Test]
	public void DefaultConstructor()
	{
		ConstructorControlPanel control = new ConstructorControlPanel();
		Assert.IsFalse(control.buttonCreate.Enabled);
		Assert.IsFalse(control.buttonDrillDown.Visible);
		Assert.IsFalse(control.buttonAddToPool.Visible);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
		Assert.AreEqual("1", control.comboBoxTimes.Text);
	}

	class TestClass
	{
	}

	[Test]
	public void InvokingCreateUpdatesStatsListing()
	{
		ConstructorControlPanel control = new ConstructorControlPanel();
		ConstructorInfo info = typeof(TestClass).GetConstructor(new Type[] { });
		control.Initialize(info);
		control.OnCreateClick(null, null);
		Assert.IsTrue(control.comboBoxMessageBar.Items[0].ToString().EndsWith("to Create."));
	}

	class MultipleInvokeTester : ConstructorControlPanel
	{
		public MultipleInvokeTester() : base()
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
	public void InvokeMultipleTimes()
	{
		MultipleInvokeTester control = new MultipleInvokeTester();
		ConstructorInfo info = typeof(TestClass).GetConstructor(new Type[] { });
		control.Initialize(info);
		control.InvokeCount = 10;
		control.OnCreateClick(null, null);
		Assert.AreEqual(10, control.invoked);
	}

	[Test]
	public void InitializationWithNoInfo()
	{
		ConstructorControlPanel control = new ConstructorControlPanel();
		control.comboBoxMessageBar.Enabled = true;
		control.comboBoxMessageBar.Items.Add("something");
		control.buttonDrillDown.Visible = true;
		control.buttonAddToPool.Visible = true;
		control.Initialize();
		Assert.AreEqual(0, control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsFalse(control.buttonCreate.Enabled);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.AreEqual(0, control.previewControl.textBoxToString.Text.Length);
		Assert.IsFalse(control.buttonDrillDown.Visible);
		Assert.IsFalse(control.buttonAddToPool.Visible);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
	}

	[Test]
	public void InitializationWithInfo()
	{
		ConstructorControlPanel control = new ConstructorControlPanel();
		ConstructorInfo info = typeof(TestClass).GetConstructor(new Type[] { });
		control.comboBoxMessageBar.Items.Add("something");
		control.Initialize(info);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.IsFalse(control.buttonDrillDown.Visible);
		Assert.IsFalse(control.buttonAddToPool.Visible);
	}

	[Test]
	public void PlusButtonAddsInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		ConstructorControlPanel control = new ConstructorControlPanel();
		ConstructorInfo info = typeof(TestClass).GetConstructor(new Type[] { });
		control.Initialize(info);
		string instance = "something";
		control.Lastresult = instance;
		control.OnAddToPool(null, null);
		Assert.IsTrue(ObjectPool.pool.Contains(instance));
	}

	[Test]
	public void AddingInstanceToObjectPoolLogsMessage()
	{
		ObjectPool.pool.Clear();
		ConstructorControlPanel control = new ConstructorControlPanel();
		ConstructorInfo info = typeof(TestClass).GetConstructor(new Type[] { });
		control.Initialize(info);
		control.comboBoxMessageBar.Items.Add("other stuff 1");
		control.comboBoxMessageBar.Items.Add("other stuff 2");
		string instance = "something";
		control.Lastresult = instance;
		control.OnAddToPool(null, null);
		Assert.AreEqual("String added to Object pool.", (string)control.comboBoxMessageBar.Items[0]);
	}

	[Test]
	public void DoNotAddSameInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		ConstructorControlPanel control = new ConstructorControlPanel();
		ConstructorInfo info = typeof(TestClass).GetConstructor(new Type[] { });
		control.Initialize(info);
		string instance = "something";
		control.Lastresult = instance;
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		Assert.AreEqual(1, ObjectPool.pool.Count);
	}

	[Test]
	public void UpdatingInvokeTimeUpdatesInvokeCount()
	{
		ConstructorControlPanel control = new ConstructorControlPanel();
		control.comboBoxTimes.Text = Convert.ToString(345);
		control.invoke_box_Validating(null, null);
		Assert.AreEqual(345, control.InvokeCount);
	}

	[Test]
	public void ResetButtonClearsCountersAndListBox()
	{
		ConstructorControlPanel control = new ConstructorControlPanel();
		ConstructorInfo info = typeof(TestClass).GetConstructor(new Type[] { });
		control.Initialize(info);
		control.OnCreateClick(null, null);
		control.OnCreateClick(null, null);
		control.OnCreateClick(null, null);
		control.OnCreateClick(null, null);
		control.OnCreateClick(null, null);
		control.buttonReset_Click(null, null);
		Assert.AreEqual(0, control.average_count);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
	}
}
