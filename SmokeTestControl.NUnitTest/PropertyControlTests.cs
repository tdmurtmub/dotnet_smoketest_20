using System;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;
using System.Reflection;

[TestFixture]
class PropertyControlTestFixture
{
	[Test]
	public void DefaultConstructor()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
		Assert.AreEqual("1", control.comboBoxTimes.Text);
	}

	[Test]
	public void DefaultInitialization()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		control.previewControl.textBoxToString.Text = "something";
		control.buttonDrillDown.Visible = true;
		control.buttonAddToPool.Visible = true;
		control.Initialize();
		Assert.IsNull(control.instance_under_test);
		Assert.IsNull(control.property_under_test);
		Assert.AreEqual(0, control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsFalse(control.buttonGet.Enabled);
		Assert.IsFalse(control.buttonSet.Enabled);
		Assert.IsFalse(control.buttonDrillDown.Visible);
		Assert.IsFalse(control.buttonAddToPool.Visible);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
	}

	class TestClass
	{
		public static int public_static_int = 365;

		public static int get_counter = 0;
		public static int set_counter = 0;

		public static int PublicStaticProperty 
		{
			get { ++get_counter;  return public_static_int; }
			set { ++set_counter;  public_static_int = value; } 
		}

		public static int public_static_write_only_property;
		public static int WriteOnlyProperty { set { public_static_write_only_property = value; } }

		public static Object null_field = null;
		public static Object NullProperty { get { return null_field; } }
	}

	[Test]
	public void InitializationWithPropertyInfo()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		control.previewControl.textBoxToString.Text = "something";
		PropertyInfo info = typeof(TestClass).GetProperty("PublicStaticProperty");
		control.Initialize(info);
		Assert.AreSame(info, control.property_under_test);
		Assert.AreEqual(4, control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsTrue(control.buttonGet.Enabled);
		Assert.IsTrue(control.buttonSet.Enabled);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
		Assert.AreNotEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.IsTrue(control.buttonDrillDown.Visible);
		Assert.IsTrue(control.buttonAddToPool.Visible);
	}

	[Test]
	public void InitializeCallsGetterIfAvailable()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		PropertyInfo info = typeof(TestClass).GetProperty("PublicStaticProperty");
		control.Initialize(info);
		Assert.AreEqual("365", control.previewControl.textBoxToString.Text);
	}

	[Test]
	public void InitializeWriteOnlyProperty()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		PropertyInfo info = typeof(TestClass).GetProperty("WriteOnlyProperty");
		control.Initialize(info);
		Assert.IsTrue(control.buttonSet.Enabled);
	}

	[Test]
	public void SetterUpdatesPreviewTabs()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		PropertyInfo info = typeof(TestClass).GetProperty("PublicStaticProperty");
		control.Initialize(info);
		control.buttonSet_Click(null, new EventArgs());
		Assert.AreEqual("0", control.previewControl.textBoxToString.Text);
	}

	[Test]
	public void HandleNullResult()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		PropertyInfo info = typeof(TestClass).GetProperty("NullProperty");
		control.Initialize(info);
		Assert.AreEqual("(null)", control.previewControl.textBoxToString.Text);
	}

	[Test]
	public void PlusButtonAddsInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		PropertyControlPanel control = new PropertyControlPanel();
		control.Lastresult = "something";
		control.OnAddToPool(null, null);
		Assert.IsTrue(ObjectPool.pool.Contains(control.Lastresult));
	}

	[Test]
	public void DoNotAddSameInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		PropertyControlPanel control = new PropertyControlPanel();
		control.Lastresult = "something";
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		control.OnAddToPool(null, null);
		Assert.AreEqual(1, ObjectPool.pool.Count);
	}

	[Test]
	public void InvokeGetMultipleTimes()
	{
		TestClass.get_counter = 0;
		PropertyControlPanel control = new PropertyControlPanel();
		PropertyInfo info = typeof(TestClass).GetProperty("PublicStaticProperty");
		control.Initialize(info);
		control.InvokeCount = 8;
		control.buttonGet_Click(null, null);
		Assert.AreEqual(9, TestClass.get_counter);
	}

	[Test]
	public void InvokeSetMultipleTimes()
	{
		TestClass.set_counter = 0;
		PropertyControlPanel control = new PropertyControlPanel();
		PropertyInfo info = typeof(TestClass).GetProperty("PublicStaticProperty");
		control.Initialize(info);
		control.InvokeCount = 28;
		control.buttonSet_Click(null, null);
		Assert.AreEqual(28, TestClass.set_counter);
	}

	[Test]
	public void UpdatingInvokeTimeUpdatesInvokeCount()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		control.comboBoxTimes.Text = Convert.ToString(4567);
		control.invoke_box_Validating(null, null);
		Assert.AreEqual(4567, control.InvokeCount);
	}

	[Test]
	public void ResetButtonClearsCountersAndListBox()
	{
		PropertyControlPanel control = new PropertyControlPanel();
		PropertyInfo info = typeof(TestClass).GetProperty("PublicStaticProperty");
		control.Initialize(info);
		control.buttonGet_Click(null, null);
		control.buttonGet_Click(null, null);
		control.buttonGet_Click(null, null);
		control.buttonGet_Click(null, null);
		control.buttonReset_Click(null, null);
		Assert.AreEqual(0, control.average_count);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
	}
}