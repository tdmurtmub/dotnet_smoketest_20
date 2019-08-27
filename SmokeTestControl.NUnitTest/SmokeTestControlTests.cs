using System;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class SmokeTestControlTestFixture
{
	[Test]
	public void InitialState ()
	{
		MainControl control = new MainControl();
		Assert.IsFalse(control.method_control.buttonCall.Enabled);
		Assert.IsFalse(control.property_control.buttonGet.Enabled);
		Assert.IsFalse(control.property_control.buttonSet.Enabled);
		Assert.IsFalse(control.field_control.buttonRead.Enabled);
		Assert.IsFalse(control.field_control.buttonWrite.Enabled);
		Assert.IsFalse(control.method_control.buttonDrillDown.Visible);
		Assert.IsFalse(control.property_control.buttonDrillDown.Visible);
		Assert.IsFalse(control.field_control.buttonDrillDown.Visible);
	}

	[Test]
	public void ConstructForType()
	{
		MainControl control = new MainControl(typeof(String));
		Assert.Greater(control.Controls.Count, 0);
		Assert.Greater(control.splitContainerConstructors.Panel2.Controls.Count, 0);
		Assert.Greater(control.listBoxConstructors.Items.Count, 0);
		Assert.Greater(control.listBoxMethods.Items.Count, 0);
	}

	class AnyRefType { }

	[Test]
	public void InstanceConstructorRemovesConstructorsTab ()
	{
		MainControl control = new MainControl(new AnyRefType());
		Assert.AreEqual(3, control.tabControlMembers.TabCount);
	}

	[Test]
	public void InitializeType ()
	{
		MainControl control = new MainControl();
		control.InitializeType(typeof(String));
		Assert.Greater(control.listBoxConstructors.Items.Count, 0);
		Assert.Greater(control.listBoxMethods.Items.Count, 0);
	}

	enum EnumType { a, b, c };
	class ClassType { }
	abstract class AbstractClassType { }
	class GenericClass<T> { }
	static class StaticClassType { }
	interface Interface { }

	[Test]
	public void SmokeTestableTypes()
	{
		Assert.IsFalse(MainControl.IsSmokeTestable(typeof(Interface)));
		Assert.IsTrue(MainControl.IsSmokeTestable(typeof(EnumType)));
		Assert.IsTrue(MainControl.IsSmokeTestable(typeof(ValueType)));
		Assert.IsTrue(MainControl.IsSmokeTestable(typeof(ClassType)));
		Assert.IsFalse(MainControl.IsSmokeTestable(typeof(GenericClass<int>)));
	}

	public delegate void TestDelegate();

	class TestClass
	{
		public TestClass() { }
		public TestClass(int i) { }
		public TestClass(double d) { }
		public static event TestDelegate event1 = delegate { };
		public static void Method1(int i) { }
		public static void Method2(int i) { }
		public static void Method3(int i) { }
		public static void IgnoreGenericMethods<T>(int i) { }
		public static int field1 = 1;
		public static int field2 = 2;
		public static int field3 = 3;
		public static int Property1 { get { return field1; } set { field1 = value; } }
		public static int Property2 { get { return field2; } set { field2 = value; } }
		public static int Property3 { get { return field3; } set { field3 = value; } }
		public static int Property4 { get { return field1; } set { field1 = value; } }
	}

	[Test]
	public void PopulatingConstructorsTab ()
	{
		MainControl control = new MainControl();
		control.PopulateConstructorsTab(typeof(TestClass));
		Assert.AreEqual(3, control.listBoxConstructors.Items.Count);
		string item = control.listBoxConstructors.Items[0].ToString();
		Assert.AreEqual("TestClass()", item);
	}

	class ClassWith_cctor
	{
		public static int i = -1; // initialization of a static member forces a .cctor() constructor
		public ClassWith_cctor() { }
	}

	[Test]
	public void PopulatingConstructorsTab_removes_cctor ()
	{
		MainControl control = new MainControl();
		control.PopulateConstructorsTab(typeof(ClassWith_cctor));
		Assert.AreEqual(1, control.listBoxConstructors.Items.Count);
	}

	[Test]
	public void PopulatingConstructorsTabReplacesListContents ()
	{
		MainControl control = new MainControl();
		control.PopulateConstructorsTab(typeof(TestClass));
		control.PopulateConstructorsTab(typeof(TestClass));
		Assert.AreEqual(3, control.listBoxConstructors.Items.Count);
	}

	[Test]
	public void PopulatingConstructorsTabShouldResetSelectedMember()
	{
		MainControl control = new MainControl();
		control.PopulateConstructorsTab(typeof(TestClass));
		control.listBoxConstructors.SetSelected(0, true);
		control.PopulateConstructorsTab(typeof(string));
		Assert.IsNull(control.constructor_control.constructor_info);
	}

	[Test]
	public void SelectingConstructorListItemShouldReinitializeControls()
	{
		MainControl control = new MainControl();
		control.PopulateConstructorsTab(typeof(TestClass));
		control.listBoxConstructors.SetSelected(0, true);
		control.listBoxConstructors_SelectedIndexChanged(null, null);
		Assert.AreEqual(typeof(TestClass).GetConstructors()[0], control.constructor_control.constructor_info);
	}

	[Test]
	public void SelectingMethodListItemShouldReinitializeControls()
	{
		MainControl control = new MainControl();
		control.PopulateMethodsTab(typeof(TestClass));
		control.listBoxMethods.SetSelected(0, true);
		control.listBoxMethods_SelectedIndexChanged(null, null);
		Assert.AreEqual(typeof(TestClass).GetMethod("Method1"), control.method_control.method_under_test);
	}

	[Test]
	public void SelectingPropertyListItemShouldReinitializeControls()
	{
		MainControl control = new MainControl();
		control.PopulatePropertiesTab(typeof(TestClass));
		control.listBoxProperties.SetSelected(0, true);
		control.listBoxProperties_SelectedIndexChanged(null, null);
		Assert.AreEqual(typeof(TestClass).GetProperty("Property1"), control.property_control.property_under_test);
	}

	[Test]
	public void SelectingFieldListItemShouldReinitializeControls()
	{
		MainControl control = new MainControl();
		control.PopulateFieldsTab(typeof(TestClass));
		control.listBoxFields.SetSelected(1, true);
		control.listBoxFields_SelectedIndexChanged(null, null);
		Assert.AreEqual(typeof(TestClass).GetField("field1"), control.field_control.field_under_test);
	}
	
	[Test]
	public void PopulatingConstructorsTabShouldResetArgumentTableAndControls()
	{
		MainControl control = new MainControl();
		control.PopulateConstructorsTab(typeof(TestClass));
		control.listBoxConstructors.SetSelected(0, true);
		control.constructor_control.previewControl.textBoxToString.Text = "something";
		control.constructor_control.buttonDrillDown.Visible = true;
		control.PopulateConstructorsTab(typeof(string));
		Assert.AreEqual(0, control.constructor_control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsFalse(control.constructor_control.buttonCreate.Enabled);
		Assert.AreEqual(0, control.constructor_control.comboBoxMessageBar.Items.Count);
		Assert.AreEqual(0, control.constructor_control.previewControl.textBoxToString.Text.Length);
		Assert.IsFalse(control.constructor_control.buttonDrillDown.Visible);
	}

	[Test]
	public void PopulatingMethodsTab ()
	{
		MainControl control = new MainControl();
		control.PopulateMethodsTab(typeof(TestClass));
		Assert.AreEqual(3, control.listBoxMethods.Items.Count);
	}

	[Test]
	public void PopulatingMethodsTabShouldRemoveEventMethods ()
	{
		MainControl control = new MainControl();
		control.PopulateMethodsTab(typeof(TestClass));
		Assert.AreEqual(3, control.listBoxMethods.Items.Count);
	}

	[Test]
	public void PopulatingMethodsTabReplacesListContents()
	{
		MainControl control = new MainControl();
		control.PopulateMethodsTab(typeof(TestClass));
		control.PopulateMethodsTab(typeof(TestClass));
		Assert.AreEqual(3, control.listBoxMethods.Items.Count);
	}

	[Test]
	public void PopulatingMethodsTabShouldResetSelectedMember()
	{
		MainControl control = new MainControl();
		control.PopulateMethodsTab(typeof(TestClass));
		control.listBoxMethods.SetSelected(0, true);
		control.PopulateMethodsTab(typeof(string));
		Assert.IsNull(control.method_control.method_under_test);
	}

	[Test]
	public void PopulatingMethodsTabShouldResetArgumentTableAndControls()
	{
		MainControl control = new MainControl();
		control.PopulateMethodsTab(typeof(TestClass));
		control.listBoxMethods.SetSelected(0, true);
		control.method_control.previewControl.textBoxToString.Text = "something";
		control.method_control.buttonDrillDown.Visible = true;
		control.PopulateMethodsTab(typeof(string));
		Assert.AreEqual(0, control.method_control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsFalse(control.method_control.buttonCall.Enabled);
		Assert.AreEqual(0, control.method_control.comboBoxMessageBar.Items.Count);
		Assert.AreEqual(0, control.method_control.previewControl.textBoxToString.Text.Length);
		Assert.IsFalse(control.method_control.buttonDrillDown.Visible);
	}

	[Test]
	public void PopulatingPropertiesTab()
	{
		MainControl control = new MainControl();
		control.PopulatePropertiesTab(typeof(TestClass));
		Assert.AreEqual(4, control.listBoxProperties.Items.Count);
	}

	[Test]
	public void PopulatingPropertiesTabReplacesListContents()
	{
		MainControl control = new MainControl();
		control.PopulatePropertiesTab(typeof(TestClass));
		control.PopulatePropertiesTab(typeof(TestClass));
		Assert.AreEqual(4, control.listBoxProperties.Items.Count);
	}

	[Test]
	public void PopulatingPropertiesTabShouldResetArgumentTableAndControls()
	{
		MainControl control = new MainControl();
		control.PopulatePropertiesTab(typeof(TestClass));
		control.listBoxProperties.SetSelected(0, true);
		control.property_control.previewControl.textBoxToString.Text = "something";
		control.property_control.buttonDrillDown.Visible = true;
		control.PopulatePropertiesTab(typeof(string));
		Assert.AreEqual(0, control.property_control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsFalse(control.property_control.buttonGet.Enabled);
		Assert.IsFalse(control.property_control.buttonSet.Enabled);
		Assert.AreEqual(0, control.property_control.comboBoxMessageBar.Items.Count);
		Assert.AreEqual(0, control.property_control.previewControl.textBoxToString.Text.Length);
		Assert.IsFalse(control.property_control.buttonDrillDown.Visible);
	}

	[Test]
	public void PopulatingFieldsTab()
	{
		MainControl control = new MainControl();
		control.PopulateFieldsTab(typeof(TestClass));
		Assert.AreEqual(4, control.listBoxFields.Items.Count);
	}

	[Test]
	public void PopulatingFieldsTabReplacesListContents()
	{
		MainControl control = new MainControl();
		control.PopulateFieldsTab(typeof(TestClass));
		control.PopulateFieldsTab(typeof(TestClass));
		Assert.AreEqual(4, control.listBoxFields.Items.Count);
	}

	[Test]
	public void PopulatingFieldsTabShouldResetSelectedMember()
	{
		MainControl control = new MainControl();
		control.PopulateFieldsTab(typeof(TestClass));
		control.listBoxFields.SetSelected(0, true);
		control.PopulateFieldsTab(typeof(string));
		Assert.IsNull(control.field_control.field_under_test);
	}

	[Test]
	public void PopulatingFieldsTabShouldResetArgumentTableAndControls()
	{
		MainControl control = new MainControl();
		control.PopulateFieldsTab(typeof(TestClass));
		control.listBoxFields.SetSelected(0, true);
		control.field_control.previewControl.textBoxToString.Text = "something";
		control.field_control.buttonDrillDown.Visible = true;
		control.PopulateFieldsTab(typeof(string));
		Assert.AreEqual(0, control.field_control.arguments_control.tableLayoutPanel.Controls.Count);
		Assert.IsFalse(control.field_control.buttonRead.Enabled);
		Assert.IsFalse(control.field_control.buttonWrite.Enabled);
		Assert.AreEqual(0, control.field_control.comboBoxMessageBar.Items.Count);
		Assert.AreEqual(0, control.field_control.previewControl.textBoxToString.Text.Length);
		Assert.IsFalse(control.field_control.buttonDrillDown.Visible);
	}

	[Test]
	public void PopulatingTabs()
	{
		MainControl control = new MainControl();
		control.PopulateMemberLists(typeof(TestClass), BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		Assert.AreEqual(3, control.listBoxConstructors.Items.Count);
		Assert.AreEqual(3, control.listBoxMethods.Items.Count);
		Assert.AreEqual(4, control.listBoxProperties.Items.Count);
		Assert.AreEqual(4, control.listBoxFields.Items.Count);
	}

	[Test]
	public void SelectingConstructorListItemShouldResetAverages()
	{
		MainControl control = new MainControl();
		control.PopulateConstructorsTab(typeof(TestClass));
		control.listBoxConstructors.SetSelected(0, true);
		control.listBoxConstructors_SelectedIndexChanged(null, null);
		control.constructor_control.OnCreateClick(null, null);
		control.constructor_control.OnCreateClick(null, null);
		control.constructor_control.OnCreateClick(null, null);
		control.listBoxConstructors_SelectedIndexChanged(null, null);
		control.constructor_control.OnCreateClick(null, null);
		control.constructor_control.OnCreateClick(null, null);
		control.constructor_control.OnCreateClick(null, null);
		string message = (string)control.constructor_control.comboBoxMessageBar.Items[0];
		Assert.AreEqual("over 2 invocations.", message.Substring(message.LastIndexOf("over")));
	}

	[Test]
	public void SelectingMethodsListItemShouldResetAverages()
	{
		MainControl control = new MainControl();
		control.PopulateMethodsTab(typeof(TestClass));
		control.listBoxMethods.SetSelected(0, true);
		control.listBoxMethods_SelectedIndexChanged(null, null);
		control.method_control.buttonCall_Click(null, null);
		control.method_control.buttonCall_Click(null, null);
		control.method_control.buttonCall_Click(null, null);
		control.method_control.buttonCall_Click(null, null);
		control.listBoxMethods_SelectedIndexChanged(null, null);
		control.method_control.buttonCall_Click(null, null);
		control.method_control.buttonCall_Click(null, null);
		control.method_control.buttonCall_Click(null, null);
		string message = (string)control.method_control.comboBoxMessageBar.Items[0];
		Assert.AreEqual("over 2 invocations.", message.Substring(message.LastIndexOf("over")));
	}

	[Test]
	public void SelectingPropertyListItemShouldResetAverages()
	{
		MainControl control = new MainControl();
		control.PopulatePropertiesTab(typeof(TestClass));
		control.listBoxProperties.SetSelected(0, true);
		control.listBoxProperties_SelectedIndexChanged(null, null);
		control.property_control.buttonGet_Click(null, null);
		control.property_control.buttonGet_Click(null, null);
		control.property_control.buttonSet_Click(null, null);
		control.property_control.buttonGet_Click(null, null);
		control.property_control.buttonSet_Click(null, null);
		control.property_control.buttonGet_Click(null, null);
		control.listBoxProperties_SelectedIndexChanged(null, null);
		control.property_control.buttonGet_Click(null, null);
		control.property_control.buttonSet_Click(null, null);
		control.property_control.buttonGet_Click(null, null);
		string message = (string)control.property_control.comboBoxMessageBar.Items[0];
		Assert.AreEqual("over 3 invocations.", message.Substring(message.LastIndexOf("over")));
	}

	[Test]
	public void SelectingFieldListItemShouldResetAverages()
	{
		MainControl control = new MainControl();
		control.PopulateFieldsTab(typeof(TestClass));
		control.listBoxFields.SetSelected(1, true);
		control.listBoxFields_SelectedIndexChanged(null, null);
		control.field_control.buttonRead_Click(null, null);
		control.field_control.buttonRead_Click(null, null);
		control.field_control.buttonWrite_Click(null, null);
		control.field_control.buttonWrite_Click(null, null);
		control.field_control.buttonWrite_Click(null, null);
		control.field_control.buttonRead_Click(null, null);
		control.field_control.buttonRead_Click(null, null);
		control.listBoxFields_SelectedIndexChanged(null, null);
		control.field_control.buttonRead_Click(null, null);
		control.field_control.buttonWrite_Click(null, null);
		control.field_control.buttonWrite_Click(null, null);
		control.field_control.buttonWrite_Click(null, null);
		control.field_control.buttonRead_Click(null, null);
		string message = (string)control.field_control.comboBoxMessageBar.Items[0];
		Assert.AreEqual("over 5 invocations.", message.Substring(message.LastIndexOf("over")));
	}
}
