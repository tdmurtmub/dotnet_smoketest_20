using System;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

class TestableArgumentTypeControl : ArgumentTypeControl
{
	public TestableArgumentTypeControl(Type type) : base(type.Assembly, type, new Button()) 
	{ 
	}

	public TestableArgumentTypeControl(Type type, Control target) : base(type.Assembly, type, target) 
	{ 
	}
}

[TestFixture]
class ArgumentTypeControlTestFixture
{

	[Test]
	public void ConstructorSavesTypeInTag()
	{
		TestableArgumentTypeControl control = new TestableArgumentTypeControl(typeof(string));
		Assert.AreEqual(typeof(String), control.Tag);
	}

	class AnySupportedType { }

	[Test]
	public void EnabledByDefaultForSupportedTypes()
	{
		TestableArgumentTypeControl control = new TestableArgumentTypeControl(typeof(AnySupportedType));
		Assert.IsTrue(control.Enabled);
	}

	interface AnyUnsupportedType { }

	[Test]
	public void DisabledForUnsupportedTypes()
	{
		TestableArgumentTypeControl control = new TestableArgumentTypeControl(typeof(double));
		Assert.IsFalse(control.Enabled);
	}

	[Test]
	public void NewObjectInstanceIsSavedInTargetControlTagIfNotNull()
	{
		Button target = new Button();
		TestableArgumentTypeControl control = new TestableArgumentTypeControl(typeof(double), target);
		AnySupportedType test_instance = new AnySupportedType();		
		control.ProcessOnClickResult(test_instance);
		Assert.AreSame(test_instance, target.Tag);
		control.ProcessOnClickResult(null);
		Assert.AreSame(test_instance, target.Tag);
	}

	[Test]
	public void TargetControlIsEnableIfInstanceIsNotNull()
	{
		Button target = new Button();
		TestableArgumentTypeControl control = new TestableArgumentTypeControl(typeof(double), target);
		control.ProcessOnClickResult(3.141);
		Assert.IsTrue(target.Enabled);
		Assert.AreEqual("3.141", target.Text);
	}
}