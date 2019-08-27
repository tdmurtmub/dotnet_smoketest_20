using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class LiteralArgumentControlTestFixture
{
	[Test]
	public void Construction()
	{
		LiteralValueControl<int> target = new LiteralValueControl<int>("123");
		Assert.AreEqual("123", target.Text);
	}
}