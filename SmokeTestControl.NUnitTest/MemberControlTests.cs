using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class MemberControlTestFixture
{
	class TestableMemberControlPanel : MemberControlPanel
	{
		public string validation_error_message;

		public TestableMemberControlPanel() : base()
		{
			this.invoke_box = new ComboBox();
		}

		internal override void OnValidationError(string message)
		{
			validation_error_message = message;
		}
	}

	[Test]
	public void Defaults()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		Assert.AreEqual(1, control.InvokeCount);
	}

	[Test]
	public void ProcessLastresultUpdatesDrilldownButtonText()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		Button button = new Button();
		control.ProcessLastresult("foo", 10.9, button, new Button(), 100, 20, new ComboBox());
		Assert.AreEqual("Double...", button.Text);
	}

	[Test]
	public void ProcessLastresultStacksUpMessages()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		ComboBox combobox = new ComboBox();
		control.ProcessLastresult("first", 10.9, new Button(), new Button(), 100, 20, combobox);
		control.ProcessLastresult("next", 10.9, new Button(), new Button(), 100, 20, combobox);
		Assert.AreEqual("100 ticks (20 ms) to next.", combobox.Items[0]);
		Assert.AreEqual("100 ticks (20 ms) to first.", combobox.Items[1]);
	}

	[Test]
	public void ProcessResultEnablesStatsListingControlWhenMoreThanOneEntryAdded()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		ComboBox combobox = new ComboBox();
		control.ProcessLastresult("first", 10.9, new Button(), new Button(), 100, 20, combobox);
		Assert.IsFalse(combobox.Enabled);
		control.ProcessLastresult("second", 10.9, new Button(), new Button(), 100, 20, combobox);
		Assert.IsTrue(combobox.Enabled);
		control.ProcessLastresult("second", 10.9, new Button(), new Button(), 100, 20, combobox);
		control.ProcessLastresult("second", 10.9, new Button(), new Button(), 100, 20, combobox);
		control.ProcessLastresult("second", 10.9, new Button(), new Button(), 100, 20, combobox);
		Assert.IsTrue(combobox.Enabled);
	}

	[Test]
	public void ProcessLastResultIncludesAveragesAfterFirstInvoke()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		ComboBox combobox = new ComboBox();
		control.ProcessLastresult("JIT call", "object", new Button(), new Button(), 10000, 600, combobox);
		control.ProcessLastresult("Op", "object", new Button(), new Button(), 500, 20, combobox);
		Assert.AreEqual("500 ticks (20 ms) to Op.", combobox.Items[0]);
		control.ProcessLastresult("Op", "object", new Button(), new Button(), 600, 10, combobox);
		Assert.AreEqual("600 ticks (10 ms) to Op. Average 550 ticks (15 ms) over 2 invocations.", combobox.Items[0]);
	}

	[Test]
	public void ProcessLastResultIncludesInvokeCountWhenMoreThanOne()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.InvokeCount = 100;
		ComboBox combobox = new ComboBox();
		control.ProcessLastresult("Invoke", "object", new Button(), new Button(), 500, 20, combobox);
		Assert.AreEqual("500 ticks (20 ms) to Invoke 100 times.", combobox.Items[0]);
	}

	[Test]
	public void NullResultHidesDrillDownButtons()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		Button buttonDrilldown = new Button();
		Button buttonAddToPool = new Button();
		buttonDrilldown.Visible = true;
		control.ProcessLastresult("bar", null, buttonDrilldown, buttonAddToPool, 100, 20, new ComboBox());
		Assert.IsFalse(buttonDrilldown.Visible);
		Assert.IsFalse(buttonAddToPool.Visible);
	}

	class ConcreteClassWithNoDefaultConstructor
	{
		public ConcreteClassWithNoDefaultConstructor(int i)
		{
		}
	}

	class UnsupportedRefType<T> : Object
	{
	}

	enum EnumType { Jan, Feb, Mar }

	class ArgumentPanelTestClass
	{
		public void PrimitiveArgType(int i) { }
		public void EnumArgType(EnumType info) { }
		public void SupportedConcreteArgType(string s) { }
		public void AnyConcreteType(ArrayList a) { }
		public void UnsupportedArgType(UnsupportedRefType<int> info) { }
	}

	class MockMethodControl : MethodControlPanel
	{
		public MockMethodControl()
		{
			Show();
		}
	}

	[Test]
	public void PopulateArgumentRowForAnyType()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(int), "foo");
		Assert.AreSame(typeof(NullCheckBox), control.arguments_control.tableLayoutPanel.Controls[0].GetType());
		NullCheckBox box = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
	}

	[Test]
	public void PopulateArgumentRowForChar()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(char), "char_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		TextBox input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT] as TextBox;
		Assert.IsTrue(input.Enabled);
		Assert.AreEqual(1, input.MaxLength);
	}

	[Test]
	public void PopulateArgumentRowForByte()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(byte), "byte_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForSByte()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(sbyte), "sbyte_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForInt16()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(short), "int16_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForInt32()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(int), "int_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForInt64()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(long), "long_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForUInt16()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(ushort), "uint16_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForUInt32()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(uint), "uint32_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForUInt64()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(ulong), "ulong_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForDouble()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(double), "double_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
		Assert.AreEqual("0", input.Text);
	}

	[Test]
	public void PopulateArgumentRowForSingle()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(Single), "single_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
		Assert.AreEqual("0", input.Text);
	}

	[Test]
	public void PopulateArgumentRowForDecimal()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(decimal), "decimal_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsTrue(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForEnum()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(EnumType), "enum_arg");
		NullCheckBox nullBox = control.arguments_control.tableLayoutPanel.Controls[0] as NullCheckBox;
		Assert.IsFalse(nullBox.Enabled);
		Assert.IsTrue(nullBox.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForString()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(string), "string_arg");
		CheckBox box = control.arguments_control.tableLayoutPanel.Controls[0] as CheckBox;
		Assert.IsTrue(box.Enabled);
		Assert.IsTrue(box.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForDateTime()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(DateTime), "datetime_arg");
		CheckBox box = control.arguments_control.tableLayoutPanel.Controls[0] as CheckBox;
		Assert.IsTrue(box.Enabled);
		Assert.IsTrue(box.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForReferenceTypeWithDefaultConstructor()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(ArrayList), "array_list");
		CheckBox box = control.arguments_control.tableLayoutPanel.Controls[0] as CheckBox;
		Assert.IsTrue(box.Enabled);
		Assert.IsTrue(box.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsTrue(input.Enabled);
	}

	class WithNoDefaultConstructor
	{
		public WithNoDefaultConstructor(int i) { }
	}

	[Test]
	public void PopulateArgumentRowForReferenceTypeWithNoDefaultConstructor()
	{
		MockMethodControl control = new MockMethodControl();
		control.PopulateNextArgumentRow(typeof(WithNoDefaultConstructor), "no_default_constructor");
		CheckBox box = control.arguments_control.tableLayoutPanel.Controls[0] as CheckBox;
		Assert.IsTrue(box.Enabled);
		Assert.IsTrue(box.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsFalse(input.Enabled);
	}

	[Test]
	public void PopulateArgumentRowForUnsupportedTypes()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.Show();
		control.PopulateNextArgumentRow(typeof(UnsupportedRefType<int>), "dontcare");
		CheckBox box = control.arguments_control.tableLayoutPanel.Controls[0] as CheckBox;
		Assert.IsFalse(box.Enabled);
		Assert.IsFalse(box.Checked);
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsFalse(input.Enabled);
		control.Hide();
	}

	[Test]
	public void PopulateArgumentRowStoresInstanceInControlTag ()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.Show();
		control.PopulateNextArgumentRow(typeof(ArrayList), "arr");
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Assert.IsInstanceOf(typeof(ArrayList), input.Tag);
		control.Hide();
	}

	[Test]
	public void GetBoolArgumentType()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.Show();
		control.PopulateNextArgumentRow(typeof(bool), "bool");
		CheckBox input = (CheckBox)control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		Object[] arguments = control.GetArguments();
		Assert.IsFalse((Boolean)arguments[0]);
		input.Checked = true;
		arguments = control.GetArguments();
		Assert.IsTrue((Boolean)arguments[0]);
		control.Hide();
	}

	[Test]
	public void GetPrimitiveArgumentType ()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.Show();
		control.PopulateNextArgumentRow(typeof(int), "int");
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		input.Text = "-456789";
		Object[] arguments = control.GetArguments();
		Assert.AreEqual(-456789, (int)arguments[0]);
		control.Hide();
	}

	[Test]
	public void GetEnumArgumentType()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.Show();
		control.PopulateNextArgumentRow(typeof(EnumType), "enum");
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		input.Text = "Feb";
		Object[] arguments = control.GetArguments();
		Assert.AreEqual(EnumType.Feb, (EnumType)arguments[0]);
		control.Hide();
	}

	[Test]
	public void GetStringArgumentType()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.Show();
		control.PopulateNextArgumentRow(typeof(string), "enum");
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		input.Text = "Hello World!";
		Object[] arguments = control.GetArguments();
		Assert.AreEqual("Hello World!", (string)arguments[0]);
		control.Hide();
	}

	[Test]
	public void GetDateTimeArgumentType()
	{
		MethodControlPanel control = new MethodControlPanel();
		control.Show();
		control.PopulateNextArgumentRow(typeof(DateTime), "datetime");
		Control input = control.arguments_control.tableLayoutPanel.Controls[ArgumentsControl.Control.Index.INPUT];
		input.Text = "2009-12-25";
		Object[] arguments = control.GetArguments();
		Assert.AreEqual(new DateTime(2009, 12, 25, 00, 00, 00), (DateTime)arguments[0]);
		control.Hide();
	}

	Object object_created;

	void OnObjectCreated(Object instance)
	{
		this.object_created = instance;
	}

	[Test]
	public void AssignToLastresultFiresEvent()
	{
	    TestableMemberControlPanel control = new TestableMemberControlPanel();
	    control.ObjectCreated += OnObjectCreated;
		control.Lastresult = "hello world";
		Assert.AreSame("hello world", object_created);
	}

	[Test]
	public void AssignNullToLastresultDoesNotFireEvent()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.ObjectCreated += OnObjectCreated;
		this.object_created = "something";
		control.Lastresult = null;
		Assert.AreSame("something", object_created);
	}

	[Test]
	public void OnlyLogMessageIfInstanceWasAddedToPool()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.Lastresult = "hello world";
		ComboBox messages = new ComboBox();
		control.AddInstanceToPool(messages);
		control.AddInstanceToPool(messages);
		Assert.AreEqual(1, messages.Items.Count);
	}

	[Test]
	public void Reset()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.average_count = 100;
		control.average_ms = 1000;
		control.average_ticks = 2000;
		control.Reset();
		Assert.AreEqual(0, control.average_count);
		Assert.AreEqual(0, control.average_ms);
		Assert.AreEqual(0, control.average_ticks);
	}

	[Test]
	public void ValidationErrorWhenInvokeCountNotNumber()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.InvokeCount = 222;
		control.invoke_box.Text = "not a number";
		control.invoke_box_Validating(null, null);
		Assert.AreEqual("Enter a valid whole number greater than 0.", control.validation_error_message);
		Assert.AreEqual(222, control.InvokeCount);
	}

	[Test]
	public void UpdateInvokeCountWhenInputValid()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.invoke_box.Text = uint.MaxValue.ToString();
		control.invoke_box_Validating(null, null);
		Assert.AreEqual(uint.MaxValue, control.InvokeCount);
	}

	[Test]
	public void ValidationErrorWhenInvokeCountZero()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.InvokeCount = 777;
		control.invoke_box.Text = "0";
		control.invoke_box_Validating(null, null);
		Assert.AreEqual("Enter a valid whole number greater than 0.", control.validation_error_message);
		Assert.AreEqual(777, control.InvokeCount);
	}

	[Test]
	public void ValidationErrorWhenInvokeCountNegative()
	{
		TestableMemberControlPanel control = new TestableMemberControlPanel();
		control.InvokeCount = 888;
		control.invoke_box.Text = "-1";
		control.invoke_box_Validating(null, null);
		Assert.AreEqual("Enter a valid whole number greater than 0.", control.validation_error_message);
		Assert.AreEqual(888, control.InvokeCount);
	}
}
