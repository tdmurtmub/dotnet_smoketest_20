using net.wesleysteiner.SmokeTestControl;
using NUnit.Framework;

[TestFixture]
class PreviewControlTestFixture
{
	[Test]
	public void UpdateToStringTab()
	{	
		PreviewControl control = new PreviewControl();
		control.Update("to string result");
		Assert.AreEqual("to string result", control.textBoxToString.Text);
	}
}
