using System;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class MethodControlPanelTestFixture
{
	[Test]
	public void DefaultConstructor()
	{
		MethodControlPanel control = new MethodControlPanel();
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
		Assert.AreEqual("1", control.comboBoxTimes.Text);
	}

	[Test]
	public void DefaultInitialization()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.comboBoxMessageBar.Items.Add("something");
		control.comboBoxMessageBar.Enabled = true;
		control.buttonDrillDown.Visible = true;
		control.buttonAddToPool.Visible = true;
		control.Initialize();
		Assert.AreEqual(0, control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.AreEqual(0, control.previewControl.textBoxToString.Text.Length);
		Assert.IsFalse(control.buttonCall.Enabled);
		Assert.IsFalse(control.buttonDrillDown.Visible);
		Assert.IsFalse(control.buttonAddToPool.Visible);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
		Assert.IsNull(control.instance_under_test);
		Assert.IsNull(control.method_under_test);
	}

	[Test]
	public void InitializationWithMethodInfo()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.comboBoxMessageBar.Items.Add("something");
		control.comboBoxMessageBar.Enabled = true;
		MethodInfo info = typeof(String).GetMethod("Contains", new Type[] { typeof(String) });
		control.Initialize(info);
		Assert.AreEqual(4, control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.AreEqual(0, control.previewControl.textBoxToString.Text.Length);
		Assert.AreSame(info, control.method_under_test);
		Assert.IsFalse(control.buttonDrillDown.Visible);
		Assert.IsFalse(control.buttonAddToPool.Visible);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
		Assert.IsTrue(control.buttonCall.Enabled);
	}

	[Test]
	public void InvokingStaticMethod()
	{
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo method = typeof(String).GetMethod("Copy", new Type[] { typeof(String) });
		control.Initialize(method);
		control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT].Text = "abc";
		control.buttonCall_Click(null, null);
		Assert.AreEqual("abc", control.previewControl.textBoxToString.Text);
	}

	[Test]
	public void InvokingInstanceMethod()
	{
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo info = typeof(String).GetMethod("ToUpper", new Type[] { });
		control.Initialize("abc", info);
		control.buttonCall_Click(null, null);
		Assert.AreEqual("ABC", control.previewControl.textBoxToString.Text);
	}

	class TestClass
	{
		public static string PublicStaticVoid() { return "Hello World"; }
	}

	class MultipleInvokeTester : MethodControlPanel
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
	public void InvokeMultipleTimes()
	{
		MultipleInvokeTester control = new MultipleInvokeTester();
		MethodInfo info = typeof(TestClass).GetMethod("PublicStaticVoid", new Type[] { });
		control.Initialize(info);
		control.InvokeCount = 18;
		control.buttonCall_Click(null, null);
		Assert.AreEqual(18, control.invoked);
	}

	[Test]
	public void InvokingMethodUpdatesStats()
	{
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo info = typeof(TestClass).GetMethod("PublicStaticVoid", new Type[] { });
		control.Initialize(info);
		control.buttonCall_Click(null, null);
		Assert.IsTrue(control.comboBoxMessageBar.Items[0].ToString().EndsWith("to Call."));
	}

	[Test]
	public void InvokingMethodUpdatesPreview()
	{
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo info = typeof(TestClass).GetMethod("PublicStaticVoid", new Type[] { });
		control.Initialize(info);
		control.buttonCall_Click(null, null);
		Assert.AreEqual("Hello World", control.previewControl.textBoxToString.Text);
	}

	[Test]
	public void InvokingMethodCatchesAndReportsExceptions ()
	{
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo info = typeof(String).GetMethod("Substring", new Type[] { typeof(Int32) });
		control.Initialize("string object", info);
		TextBox box = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT] as TextBox;
		box.Text = "-1";
		control.buttonCall_Click(null, null);
		Assert.AreEqual("TargetInvocationException...", control.buttonDrillDown.Text);
	}

	[Test]
	public void PlusButtonAddsInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo info = typeof(String).GetMethod("Substring", new Type[] { typeof(Int32) });
		control.Initialize("string object", info);
		string instance = "something";
		control.Lastresult = instance;
		control.OnAddToPool(null, null);
		Assert.IsTrue(ObjectPool.pool.Contains(instance));
	}

	[Test]
	public void DoNotAddSameInstanceToObjectPool()
	{
		ObjectPool.pool.Clear();
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo info = typeof(String).GetMethod("Substring", new Type[] { typeof(Int32) });
		control.Initialize("string object", info);
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
		MethodControlPanel control = new MethodControlPanel();
		control.comboBoxTimes.Text = Convert.ToString(3456);
		control.invoke_box_Validating(null, null);
		Assert.AreEqual(3456, control.InvokeCount);
	}

	[Test]
	public void ResetButtonClearsCountersAndListBox()
	{
		MethodControlPanel control = new MethodControlPanel();
		MethodInfo info = typeof(TestClass).GetMethod("PublicStaticVoid", new Type[] { });
		control.Initialize(info);
		control.InvokeCount = 18;
		control.buttonCall_Click(null, null);
		control.buttonCall_Click(null, null);
		control.buttonCall_Click(null, null);
		control.buttonCall_Click(null, null);
		control.buttonReset_Click(null, null);
		Assert.AreEqual(0, control.average_count);
		Assert.AreEqual(0, control.comboBoxMessageBar.Items.Count);
		Assert.IsFalse(control.comboBoxMessageBar.Enabled);
	}
}
