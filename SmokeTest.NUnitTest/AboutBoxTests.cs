using System;
using NUnit.Framework;
using SmokeTest;
using System.Reflection;

[TestFixture]
class AboutBoxTestFixture
{
	[Test]
	public void CopyrightYearIsCurrent()
	{
		Assert.IsTrue(AboutBox.AssemblyCopyright.Contains(DateTime.Now.Year.ToString()));
	}
}
