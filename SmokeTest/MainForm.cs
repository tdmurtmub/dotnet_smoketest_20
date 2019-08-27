using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using SmokeTest.Properties;
using net.wesleysteiner.SmokeTestControl;

namespace SmokeTest
{
	delegate void ExceptionHandler(Exception exception);

	public partial class MainForm : Form
	{
		const BindingFlags membersOfInterest = BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		string path;

		private void Construct()
		{
			InitializeComponent();
            this.Text = String.Format("{0} ({1}) {2}", Application.ProductName, Application.ProductVersion, AboutBox.AssemblyCopyright);
			this.treeViewInternalAssemblies.Sorted = true;
			this.treeViewExternalAssemblies.Sorted = true;
			exception_handler = OnException;
			instance = new MainControl();
			instance.Dock = DockStyle.Fill;
			splitContainer.Panel2.Controls.Add(instance);
		}

		private void OnLoad(object sender, EventArgs e)
		{
			this.Size = Settings.Default.WindowSize;
			foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()) AddAssembly(this.treeViewInternalAssemblies, a, false);
			if (this.path != null) LoadAssemblyPath(this.path, true);
		}

		private void OpenAssemblyFile()
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Assembly Files|*.dll;*.exe|All Files|*.*";
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK) LoadAssemblyPath(dialog.FileName, true);
		}

		internal ExceptionHandler exception_handler;
		internal MainControl instance;

		internal interface IProxy
		{
			void LaunchURL(string url);
		}

		private class Proxy : IProxy
		{
			public void LaunchURL(string url)
			{
				System.Diagnostics.Process.Start(url);
			}
		}

		internal IProxy proxy = new Proxy();

		internal static void OnException(Exception exception)
		{
			MessageBox.Show(exception.Message, exception.GetType().ToString());
		}

		public MainForm()
		{
			Construct();
		}

		public MainForm (string path)
		{
			Construct();
			this.path = path;
		}

		public void LoadAssemblyPath(string path)
		{
			LoadAssemblyPath(path, false);
		}

		public void LoadAssemblyPath(string path, bool selectAfterLoad)
		{
			try
			{
				AddAssembly(this.treeViewExternalAssemblies, Assembly.LoadFrom(path), selectAfterLoad);
				if (selectAfterLoad) this.tabControlAssemblies.SelectTab(1);
			}
			catch (Exception exception)
			{
				exception_handler(exception);
			}
		}

		/// <summary>
		/// Returns true if the type is 'smoketestable'.
		/// </summary>
		public static bool IsSmokeTestable(Type type)
		{
			if (type.Namespace != null && type.Namespace == String.Empty) return false;
			return MainControl.IsSmokeTestable(type);
		}

		internal TreeNode GetNamespaceNode(TreeNode node, string ns)
		{
			if (ns == null) ns = "<null>";
			foreach (TreeNode tn in node.Nodes) if (tn.Text == ns) return tn;
			return node.Nodes.Add(ns);
		}

		/// <summary>
		/// Get (and add if not available) the root node for an assembly to the tree and return the node.
		/// </summary>
		internal TreeNode GetRootNode(TreeView tree, Assembly assembly)
		{
			Debug.Assert(tree != null);
			Debug.Assert(assembly != null);
			TreeNode result = null;
			foreach (TreeNode tn in tree.Nodes) if ((Assembly)tn.Tag == assembly)
			{
				result = tn;
				result.Nodes.Clear();
				break;
			}
			if (result == null) result = tree.Nodes.Add(Utility.GetFriendlyAssemblyName(assembly), Utility.GetFriendlyAssemblyName(assembly));
			result.Tag = assembly;
			result.ToolTipText = assembly.FullName;
			return result;
		}

		internal TreeNode AddAssembly(TreeView tree, Assembly assembly, bool select_node)
		{
			Debug.Assert(tree != null);
			Debug.Assert(assembly != null);
			TreeNode root = GetRootNode(tree, assembly);
			tree.BeginUpdate();
			foreach (Type type in assembly.GetTypes())
			{
				if (IsSmokeTestable(type))
				{
					TreeNode tn = GetNamespaceNode(root, type.Namespace).Nodes.Add(type.IsNested ? type.DeclaringType.Name + '+' + type.Name :type.Name);
					if (type.IsAbstract) tn.ForeColor = System.Drawing.SystemColors.GrayText;
					tn.Tag = type;
				}
			}
			tree.EndUpdate();
			if (select_node) tree.SelectedNode = root;
			return root;
		}

		internal void OnInternalAssembyNodeSelected(object sender, TreeViewEventArgs e)
		{
			OnTreeNodeSelect(e);
		}

		internal void OnExternalAssembyNodeSelected (object sender, TreeViewEventArgs e)
		{
			OnTreeNodeSelect(e);
		}

		private void OnTreeNodeSelect(TreeViewEventArgs e)
		{
			Type t = e.Node.Tag as Type;
			if (t != null)
			{
				Assembly assembly = e.Node.Parent.Parent.Tag as Assembly;
				instance.PopulateMemberLists(assembly.GetType(e.Node.Tag.ToString()), membersOfInterest);
			}
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenAssemblyFile();
		}

		private void informationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();
			about.ShowDialog();
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			OpenAssemblyFile();
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (string file in files) LoadAssemblyPath(file, true);
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		}

		internal void blogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				proxy.LaunchURL("http://netsmoketest.blogspot.com/");
			}
			catch (Exception exception)
			{
				exception_handler(exception);
			}
		}

		private void OnSizeChanged(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Settings.Default.WindowSize = this.Size;
				Settings.Default.Save();
			}
		}
	}
}
