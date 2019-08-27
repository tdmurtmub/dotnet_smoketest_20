using System;
using System.Reflection;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

[TestFixture]
class UtilityTestFixture
{
	[Test]
	public void AlwaysIncludeDefiningAssemblyWhenFiltering()
	{
		Assembly[] sources = AppDomain.CurrentDomain.GetAssemblies();
		Type type_under_test = typeof(String);
		Assembly[] targets = Utility.FilterAssemblies(sources, type_under_test);
		Assert.AreEqual(1, targets.Length);
		Assert.AreSame(type_under_test.Assembly, targets[0]);
	}

	[Test]
	public void AssemblyTypeFilteringUniqueToOneAssembly()
	{
		Assembly[] sources = AppDomain.CurrentDomain.GetAssemblies();
		Type type_under_test = typeof(UtilityTestFixture);
		Assembly[] targets = Utility.FilterAssemblies(sources, type_under_test);
		Assert.AreEqual(1, targets.Length);
		Assert.AreSame(type_under_test.Assembly, targets[0]);
	}

	[Test]
	public void AssemblyTypeFilteringThatSpansAssemblies()
	{
		Assembly[] sources = { typeof(String).Assembly, typeof(Utility).Assembly };
		Type type_under_test = typeof(Object);
		Assembly[] targets = Utility.FilterAssemblies(sources, type_under_test);
		Assert.AreEqual(2, targets.Length);
		Assert.AreSame(type_under_test.Assembly, targets[0]);
	}
}
