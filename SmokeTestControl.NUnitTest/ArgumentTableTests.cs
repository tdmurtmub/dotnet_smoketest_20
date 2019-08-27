using System;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;
using System.Reflection;

[TestFixture]
class UnsupportedArgumentControlTestFixture
{
	class ConcreteClass { }

	[Test]
	public void Construction()
	{
		UnsupportedArgumentControl control = new UnsupportedArgumentControl(typeof(ConcreteClass));
		Assert.AreEqual("(null)", control.Text);
	}
}
		