using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using SmokeTest;
using SmokeTest.Properties;
using NUnit.Framework;

[TestFixture]
public class MainFormTestFixture
{
	[SetUp]
	public void Setup()
	{
	}

	[TearDown]
	public void Teardown()
	{
	}

	class ClassInBlankNamespace { }

	enum EnumType { a, b, c };
	class ClassType { }
	static class StaticClassType { }

	internal class Proxy : MainForm.IProxy
	{
		public string url;

		public virtual void LaunchURL(string url)
		{
			this.url = url;
		}
	}

	class Testable_MainForm : MainForm
	{
		public Testable_MainForm(string path = null) : base(path)
		{
            AllowDrop = false; // causes exception in unit test framework when true
			proxy = new Proxy();
		}
	}

	[Test]
	public void SupportedTypes()
	{
		Assert.IsTrue(MainForm.IsSmokeTestable(typeof(EnumType)));
		Assert.IsTrue(MainForm.IsSmokeTestable(typeof(ClassType)));
		Assert.IsTrue(MainForm.IsSmokeTestable(typeof(StaticClassType)));
		Assert.IsTrue(MainForm.IsSmokeTestable(typeof(ValueType)));
	}

	[Test]
	public void Construction()
	{
		Testable_MainForm form = new Testable_MainForm();
		Assert.IsTrue(form.treeViewInternalAssemblies.Sorted);
		Assert.IsTrue(form.treeViewExternalAssemblies.Sorted);
	}

	[Test]
	public void UseNamespaceNameInTreeNode()
	{
		Testable_MainForm form = new Testable_MainForm();
		TreeNode tn = form.treeViewInternalAssemblies.Nodes.Add("Assembly2");
		TreeNode tn1 = form.GetNamespaceNode(tn, "a_namespace");
		Assert.AreEqual("a_namespace", tn1.Text);
	}

	[Test]
	public void SubstituteNullNamespaceName()
	{
		Testable_MainForm form = new Testable_MainForm();
		TreeNode tn = form.treeViewInternalAssemblies.Nodes.Add("Assembly2");
		TreeNode tn1 = form.GetNamespaceNode(tn, null);
		Assert.AreEqual("<null>", tn1.Text);
	}

	[Test]
	public void GetNamespaceNodeAddsNamespaceIfNew()
	{
		Testable_MainForm form = new Testable_MainForm();
		TreeNode tn = form.treeViewInternalAssemblies.Nodes.Add("Assembly1");
		TreeNode tn1 = form.GetNamespaceNode(tn, "NS1");
		Assert.AreEqual(1, tn.Nodes.Count);
		Assert.AreSame(tn1, form.GetNamespaceNode(tn, "NS1"));
	}

	[Test]
	public void AddRootNodeAddsAssemblyToTree()
	{
		Testable_MainForm form = new Testable_MainForm();
		form.GetRootNode(form.treeViewInternalAssemblies, AppDomain.CurrentDomain.GetAssemblies()[0]);
		Assert.AreEqual(1, form.treeViewInternalAssemblies.Nodes.Count);
		Assert.AreEqual("mscorlib", form.treeViewInternalAssemblies.Nodes[0].Text);
		Assert.AreEqual("mscorlib", form.treeViewInternalAssemblies.Nodes[0].Name);
	}

	[Test]
	public void HighlightNodeOnLoad ()
	{
		Testable_MainForm form = new Testable_MainForm();
		Assembly a = AppDomain.CurrentDomain.GetAssemblies()[0];
		TreeNode tn = form.AddAssembly(form.treeViewInternalAssemblies, a, true);
		Assert.AreSame(tn, form.treeViewInternalAssemblies.SelectedNode);
	}

	[Test]
	public void DoNotHighlightNodeOnLoad()
	{
		Testable_MainForm form = new Testable_MainForm();
		Assembly a = AppDomain.CurrentDomain.GetAssemblies()[0];
		TreeNode tn = form.AddAssembly(form.treeViewInternalAssemblies, a, false);
		Assert.IsNull(form.treeViewInternalAssemblies.SelectedNode);
	}

	[Test]
	public void ReplaceDuplicateAssembly ()
	{
		Testable_MainForm form = new Testable_MainForm();
		Assembly a = AppDomain.CurrentDomain.GetAssemblies()[0];
		TreeNode tna = form.GetRootNode(form.treeViewInternalAssemblies, a);
		tna.Nodes.Add("all child nodes must be cleared by the duplicate add to follow");
		Assembly b = AppDomain.CurrentDomain.GetAssemblies()[0];
		TreeNode tnb = form.GetRootNode(form.treeViewInternalAssemblies, b);
		Assert.AreSame(tna, tnb);
		Assert.AreEqual(0, tnb.Nodes.Count);
	}

	bool exception_handler_was_invoked;

	void OnException(Exception exception)
	{
		exception_handler_was_invoked = true;
	}

	[Test]
	public void LoadAssemblyPath_handles_exceptions()
	{
		Testable_MainForm form = new Testable_MainForm();
		exception_handler_was_invoked = false;
		form.exception_handler = new ExceptionHandler(OnException);
		form.LoadAssemblyPath("path that throws.bad");
		Assert.IsTrue(exception_handler_was_invoked);
	}

	[Test]
	public void user_loaded_assembly_are_added_to_external_TreeView()
	{
		Testable_MainForm form = new Testable_MainForm();
		form.Show();
		form.LoadAssemblyPath(@"..\..\..\TestAssembly\bin\smoketest.examples.dll");
		Assert.IsTrue(form.treeViewExternalAssemblies.Nodes.ContainsKey("SmokeTest.examples"));
		form.Close();
	}	

	[Test]
	public void External_tab_selected_on_user_load_when_select_is_true ()
	{
		Testable_MainForm form = new Testable_MainForm();
		form.Show();
		form.LoadAssemblyPath(@"..\..\..\TestAssembly\bin\smoketest.examples.dll", true);
		Assert.AreSame(form.tabControlAssemblies.TabPages[1], form.tabControlAssemblies.SelectedTab);
		form.Close();
	}

	[Test]
	public void External_tab_not_selected_on_user_load_when_select_is_false()
	{
		Testable_MainForm form = new Testable_MainForm();
		form.Show();
		form.LoadAssemblyPath(@"..\..\..\TestAssembly\bin\smoketest.examples.dll", false);
		Assert.AreSame(form.tabControlAssemblies.TabPages[0], form.tabControlAssemblies.SelectedTab);
		form.Close();
	}

	[Test]
	public void NullPathInConstructorIsIgnored()
	{
		Testable_MainForm form = new Testable_MainForm();
		Assert.AreEqual(0, form.treeViewInternalAssemblies.Nodes.Count);
	}

	[Test]
	public void External_tree_node_selection_initializes_SmokeTestControl_for_type ()
	{
		Testable_MainForm form = new Testable_MainForm();
		form.Show();
		form.LoadAssemblyPath(@"..\..\..\TestAssembly\bin\smoketest.examples.dll");
		TreeNode ns = form.treeViewExternalAssemblies.Nodes[0].Nodes[0];
		TreeNode found = null;
		foreach (TreeNode node in ns.Nodes)
		{
			found = node;
			if (node.Text == "PublicClass") break;
		}
		TreeViewEventArgs e = new TreeViewEventArgs(found);
		form.OnExternalAssembyNodeSelected(null, e);
		Assert.AreEqual("PublicClass()", form.instance.listBoxConstructors.Items[0].ToString());
		form.Close();
	}

	[Test]
	public void construct_with_path_loads_assembly()
	{
        Testable_MainForm form = new Testable_MainForm(@"..\..\..\TestAssembly\bin\smoketest.examples.dll");
		form.Show();
		Assert.IsTrue(form.treeViewExternalAssemblies.Nodes.ContainsKey("SmokeTest.examples"));
		form.Close();
	}

	[Test]
	public void display_full_nested_typename_in_TreeNode()
	{
		Testable_MainForm form = new Testable_MainForm(@"..\..\..\TestAssembly\bin\smoketest.examples.dll");
		form.Show();
		TreeNode asm_node = form.treeViewInternalAssemblies.Nodes.Find("SmokeTest.examples", true)[0];
		TreeNode ns_nodes = asm_node.Nodes[0];
		bool found = false;
		foreach (TreeNode node in ns_nodes.Nodes)
		{
			found = (node.Text == "PublicStruct+NestedPublicClass");
			if (found) break;
		}
		Assert.IsTrue(found);
		form.Close();
	}

	//[Test]
	//public void persist_window_size()
	//{
	//    Testable_MainForm form = new Testable_MainForm();
	//    form.Show();
	//    form.Size = new System.Drawing.Size(300, 400);
	//    form.Close();
	//    Assert.AreEqual(300, Settings.Default.WindowSize.Width);
	//}

	[Test]
	public void blog_menuitem_selection_launches_URL()
	{
		Testable_MainForm form = new Testable_MainForm();
		form.blogToolStripMenuItem_Click(null, null);
		Assert.AreEqual("http://netsmoketest.blogspot.com/", ((Proxy)form.proxy).url);
	}

	class ProxyThatThrows : Proxy
	{
		public override void LaunchURL(string url)
		{
			throw new Exception("exception");
		}
	}

	[Test]
	public void blog_menuitem_selection_handles_exceptions()
	{
		Testable_MainForm form = new Testable_MainForm();
		exception_handler_was_invoked = false;
		form.proxy = new ProxyThatThrows();
		form.exception_handler = new ExceptionHandler(OnException);
		form.blogToolStripMenuItem_Click(null, null);
		Assert.IsTrue(exception_handler_was_invoked);
	}
}
