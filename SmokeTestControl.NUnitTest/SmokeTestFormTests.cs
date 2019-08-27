using System;
using System.Windows.Forms;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
public class EditInstanceFormTestFixture
{
	class ConcreteClass
	{
		public static ConcreteClass CreateMe() { return new ConcreteClass(); }
		public static ConcreteClass GetMe { get { return new ConcreteClass(); } }
		public static ConcreteClass Me = new ConcreteClass();
	}

	class SampleClass
	{
	}

	[Test]
	public void ConstructForObject ()
	{
		EditInstanceForm form = new EditInstanceForm(new System.Collections.ArrayList());
		Assert.AreEqual(DockStyle.Fill, form.Controls[0].Dock);
		Assert.AreEqual("System.Collections.ArrayList", form.Text);
	}
}
