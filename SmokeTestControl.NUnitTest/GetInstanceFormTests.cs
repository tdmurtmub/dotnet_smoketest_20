using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using net.wesleysteiner.SmokeTestControl;

class Parent { }
class Daughter : Parent { }
class Son : Parent { }
class B21 : Son { }

class SortTestBase { }
class Z : SortTestBase { }
class A : SortTestBase { }

class AbstractTestBase { }
abstract class A1 : AbstractTestBase { }
class A11 : A1 { }

interface IInterface { }
class MyInterface : IInterface { }
class MyMyInterface1 : MyInterface { }
class MyMyInterface2 : MyInterface { }
class MyMyInterface3 : MyInterface { }

[TestFixture]
public class GetInstanceFormTestFixture
{
	[SetUp]
	public void SetUp()
	{
		ObjectPool.pool.Clear();
	}

	class TestableGetInstanceForm : GetInstanceForm
	{
		public TestableGetInstanceForm(Type type)
			: base(new Assembly[] { type.Assembly }, type.Assembly, type)
		{
		}
	}

	class ConcreteClass
	{
		public static ConcreteClass CreateMe() { return new ConcreteClass(); }
		public static ConcreteClass GetMe { get { return new ConcreteClass(); } }
		public static ConcreteClass Me = new ConcreteClass();
	}

	[Test]
	public void ConstructWithConcreteType()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(String));
		Assert.AreEqual(DockStyle.Fill, form.Controls[0].Dock);
		Assert.AreEqual("String", form.treeTypes.Nodes[0].Text);
		Assert.AreEqual(typeof(String), form.treeTypes.Nodes[0].Tag as Type);
		Assert.AreEqual("Get String Instance", form.Text);
		Assert.IsFalse(form.buttonSelect.Enabled);
	}

	[Test]
	public void SelectAssemblyArgumentInComboBox()
	{
		Assembly source_assembly = typeof(Parent).Assembly;
		Assembly[] assemblies = { source_assembly, typeof(Object).Assembly };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(Object));
		Assert.AreEqual(typeof(Parent).Assembly.FullName, form.comboBoxAssembly.Text);
	}

	[Test]
	public void ExpandTypeFromAssemblyArgument()
	{
		Assembly source_assembly = typeof(Parent).Assembly;
		Assembly[] assemblies = { source_assembly, typeof(Object).Assembly };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(Object));
		Assert.AreEqual(source_assembly.FullName, form.comboBoxAssembly.Text);
	}

	[Test]
	public void DisableAssemblyComboBoxForSingleMatch()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(Parent));
		Assert.IsFalse(form.comboBoxAssembly.Enabled);
	}

	[Test]
	public void ComboBoxMustBeSortedAscendingByAssemblyName()
	{
		Assembly[] assemblies = { typeof(Parent).Assembly, typeof(String).Assembly };
		GetInstanceForm form = new GetInstanceForm(assemblies, typeof(Parent).Assembly, typeof(Parent));
		Assert.AreEqual(2, form.comboBoxAssembly.Items.Count);
		Assert.AreEqual(assemblies[1], form.comboBoxAssembly.Items[0] as Assembly);
		Assert.AreEqual(assemblies[0], form.comboBoxAssembly.Items[1] as Assembly);
	}

	[Test]
	public void ConstructorPopulatesTreeWithTypesDerivedFromBaseType()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(Parent));
		Assert.AreEqual("Parent", form.treeTypes.Nodes[0].Text);
		Assert.AreEqual(2, form.treeTypes.Nodes[0].Nodes.Count);
		Assert.AreEqual("Son", form.treeTypes.Nodes[0].Nodes[1].Text);
		Assert.AreEqual("B21", form.treeTypes.Nodes[0].Nodes[1].Nodes[0].Text);
	}

	[Test]
	public void ConstructorPopulatesTreeWithTypesThatImplementInterface()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(IInterface));
		Assert.AreEqual("IInterface", form.treeTypes.Nodes[0].Text);
		Assert.AreEqual(3, form.treeTypes.Nodes[0].Nodes[0].Nodes.Count);
	}

	[Test]
	public void TypeTreeIsSortedAscending()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(SortTestBase));
		Assert.AreEqual("A", form.treeTypes.Nodes[0].Nodes[0].Text);
	}

	[Test]
	public void AbstractTypeTextColoringInTreeView()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(AbstractTestBase));
		Assert.AreEqual(System.Drawing.SystemColors.GrayText, form.treeTypes.Nodes[0].Nodes[0].ForeColor);
	}

	[Test]
	public void TypeIsCachedInNodeTagProperty()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(Parent));
		Assert.AreEqual(typeof(Son), form.treeTypes.Nodes[0].Nodes[1].Tag as Type);
	}

	[Test]
	public void InstanceReturnsObjectCreatedViaConstructor()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(ConcreteClass));
		form.smoketest_control.listBoxConstructors.SelectedIndex = 0;
		form.smoketest_control.constructor_control.OnCreateClick(null, null);
		Assert.IsInstanceOf<ConcreteClass>(form.Instance);
	}

	[Test]
	public void InstanceReturnsObjectCreatedViaStaticMethod()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(ConcreteClass));
		form.smoketest_control.listBoxMethods.SelectedIndex = 0;
		form.smoketest_control.method_control.buttonCall_Click(null, null);
		Assert.IsInstanceOf<ConcreteClass>(form.Instance);
	}

	[Test]
	public void InstanceReturnsObjectCreatedViaStaticProperty()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(ConcreteClass));
		form.smoketest_control.listBoxProperties.SelectedIndex = 0;
		form.smoketest_control.property_control.buttonGet_Click(null, null);
		Assert.IsInstanceOf<ConcreteClass>(form.Instance);
	}

	[Test]
	public void InstanceReturnsObjectFromStaticField()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(ConcreteClass));
		form.smoketest_control.listBoxFields.SelectedIndex = 0;
		form.smoketest_control.field_control.buttonRead_Click(null, null);
		Assert.IsInstanceOf<ConcreteClass>(form.Instance);
	}

	[Test]
	public void InstanceReturnsNullForNoObject()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(ConcreteClass));
		Assert.IsNull(form.Instance);
	}

	class OtherTypeOfObject { }

	[Test]
	public void InstanceReturnsNullForOtherObjectTypes()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(ConcreteClass));
		form.OnObjectCreated(new OtherTypeOfObject());
		Assert.IsNull(form.Instance);
	}

	class DerivedClass : ConcreteClass
	{
	}

	[Test]
	public void InstanceReturnsDerivedTypes()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(ConcreteClass));
		DerivedClass derived = new DerivedClass();
		form.OnObjectCreated(derived);
		Assert.AreSame(derived, form.Instance);
	}

	[Test]
	public void InstanceReturnsTypesThatImplementAnInterface()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(IInterface));
		MyInterface implementor = new MyInterface();
		form.OnObjectCreated(implementor);
		Assert.AreSame(implementor, form.Instance);
	}

	[Test]
	public void FilterTypeUnderTestFromAssemblyTypes()
	{
		Type type_under_test = typeof(Son);
		System.Collections.Generic.List<Type> filtered = GetInstanceForm.GetFilteredTypes(type_under_test.Assembly.GetTypes(), type_under_test);
		Assert.IsFalse(filtered.Contains(type_under_test));
	}

	[Test]
	public void FilterInterfacesFromAssemblyTypes()
	{
		Type type_under_test = typeof(System.Collections.ICollection);
		System.Collections.Generic.List<Type> filtered = GetInstanceForm.GetFilteredTypes(type_under_test.Assembly.GetTypes(), type_under_test);
		Assert.IsFalse(filtered.Contains(type_under_test));
	}

	[Test]
	public void SelectingTreeNodePopulatesObjectListWithObjectsThatAreOrDeriveFromType()
	{
		Assembly source_assembly = typeof(Parent).Assembly;
		Assembly[] assemblies = { source_assembly, typeof(Object).Assembly };
		ObjectPool.pool = new ArrayList() { new B21(), "not this one", new Daughter(), "nor this one", new Son() };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(Parent));
		TreeViewEventArgs ev = new TreeViewEventArgs(form.treeTypes.Nodes[0]);
		ev.Node.Tag = typeof(Son);
		form.treeTypes_AfterSelect(null, ev);
		Assert.AreEqual(2, form.listBoxObjects.Items.Count);
	}

	[Test]
	public void SelectingTreeNodePopulatesObjectListWithObjectsThatImplementAnInterface()
	{
		Assembly source_assembly = typeof(IInterface).Assembly;
		Assembly[] assemblies = { source_assembly, typeof(Object).Assembly };
		ObjectPool.pool = new ArrayList() { new MyMyInterface1(), new MyInterface(), new MyMyInterface3() };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(IInterface));
		TreeViewEventArgs ev = new TreeViewEventArgs(form.treeTypes.Nodes[0]);
		ev.Node.Tag = typeof(IInterface);
		form.treeTypes_AfterSelect(null, ev);
		Assert.AreEqual(3, form.listBoxObjects.Items.Count);
	}

	[Test]
	public void SelectingTreeNodeClearsObjectListBeforePopulating()
	{
		Assembly source_assembly = typeof(Parent).Assembly;
		Assembly[] assemblies = { source_assembly, typeof(Object).Assembly };
		ObjectPool.pool = new ArrayList() { };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(Parent));
		form.listBoxObjects.Items.Add("something to the list to simulate data from last populate");
		TreeViewEventArgs ev = new TreeViewEventArgs(form.treeTypes.Nodes[0]);
		ev.Node.Tag = typeof(Parent);
		form.treeTypes_AfterSelect(null, ev);
		Assert.AreEqual(0, form.listBoxObjects.Items.Count);
	}

	[Test]
	public void SelectingTreeNodeDisablesObjectSelectButton()
	{
		Assembly source_assembly = typeof(Parent).Assembly;
		Assembly[] assemblies = { source_assembly, typeof(Object).Assembly };
		ObjectPool.pool = new ArrayList() { };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(Parent));
		form.buttonSelect.Enabled = true;
		TreeViewEventArgs ev = new TreeViewEventArgs(form.treeTypes.Nodes[0]);
		ev.Node.Tag = typeof(Parent);
		form.treeTypes_AfterSelect(null, ev);
		Assert.IsFalse(form.buttonSelect.Enabled);
	}

	[Test]
	public void HighlightSelectButtonOnObjectListSelection()
	{
		Assembly source_assembly = typeof(String).Assembly;
		Assembly[] assemblies = { source_assembly, typeof(Object).Assembly };
		ObjectPool.pool = new ArrayList() { "Object 1", "Object 2" };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(Parent));
		form.listBoxObjects.Items.Add("Object 1");
		form.listBoxObjects.SelectedIndex = 0;
		form.listBoxObjects_SelectedIndexChanged(null, null);
		Assert.IsTrue(form.buttonSelect.Enabled);
	}

	[Test]
	public void SelectButtonCachesHighlightedObjectAsInstance()
	{
		TestableGetInstanceForm form = new TestableGetInstanceForm(typeof(String));
		ObjectPool.pool = new ArrayList() { "A", "B", "C" };
		GetInstanceForm.PopulateObjectList(form.listBoxObjects, ObjectPool.pool, typeof(String));
		form.listBoxObjects.SelectedIndex = 1;
		form.buttonSelect_Click(null, null);
		Assert.AreEqual("B", form.Instance);
	}

	[Test]
	public void AddingObjectToPoolRefreshesObjectList()
	{
		Assembly source_assembly = typeof(Parent).Assembly;
		Assembly[] assemblies = { source_assembly };
		ObjectPool.pool = new ArrayList() { };
		GetInstanceForm form = new GetInstanceForm(assemblies, source_assembly, typeof(Parent));
		form.Show();
		form.instance = new Son();
		ObjectPool.Add(form.instance);
		Assert.AreEqual(1, form.listBoxObjects.Items.Count);
		form.Close();
	}
}
