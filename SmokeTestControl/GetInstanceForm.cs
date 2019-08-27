using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// A Form that encapsulates a SmokeTestControl to get a new instance of a type.
	/// </summary>
	public partial class GetInstanceForm : Form
	{
		internal Object instance;
		internal MainControl smoketest_control;
		internal Type type_under_test;

		/// <summary>
		/// Initialize the form's controls for the given type-under-test.
		/// </summary>
		/// <param name="from_assembly">The assembly to search for Types that derive from type-under-test.</param>
		/// <param name="type_under_test"></param>
		private void Initialize(Assembly from_assembly, Type type_under_test)
		{
			Debug.Assert(from_assembly != null);
			Debug.Assert(type_under_test != null);
			this.treeTypes.Nodes.Clear();
			TreeNode node = AddTypeNode(this.treeTypes.Nodes, type_under_test);
			this.treeTypes.BeginUpdate();
			PopulateTypeTree(GetFilteredTypes(from_assembly.GetTypes(), type_under_test), node.Nodes, type_under_test);
			this.treeTypes.EndUpdate();
			node.Expand();
		}

		internal void OnObjectCreated(Object instance)
		{
			this.instance = instance;
		}

		internal void OnObjectAdded(Object sender, ObjectPool.ObjectAddedEventArgs e)
		{
			RepopulateObjectList(this.instance.GetType());
		}

		private TreeNode AddTypeNode(TreeNodeCollection nodes, Type type)
		{
			TreeNode node = nodes.Add(Utility.GetFriendlyTypeName(type));
			if (type.IsAbstract) node.ForeColor = System.Drawing.SystemColors.GrayText;
			node.Tag = type;
			return node;
		}

		/// <summary>
		/// Recursively populates the "nodes" with all "types" that derive from or implement the "base_type".
		/// </summary>
		private void PopulateTypeTree(List<Type> types, TreeNodeCollection nodes, Type base_type)
		{
			for (int i = 0; i < types.Count; ++i)
			{
				Type type = types[i];
				if (type.IsSubclassOf(base_type) || base_type.IsAssignableFrom(type))
				{
					types.Remove(type);
					--i;
					TreeNode node = AddTypeNode(nodes, type);
					PopulateTypeTree(types, node.Nodes, type);
				}
			}
		}

		/// <summary>
		/// Instantiate a form for a 'type' from the 'source' assembly.
		/// </summary>
		public GetInstanceForm(Assembly[] assemblies, Assembly source, Type type)
		{
			Debug.Assert(assemblies.Length > 0);
			Debug.Assert(type != null);
			Debug.Assert(source != null);
			InitializeComponent();
			this.type_under_test = type;
			this.treeTypes.Sorted = true;
			this.Text = "Get " + Utility.GetFriendlyTypeName(type_under_test) + " Instance";
			this.comboBoxAssembly.Enabled = (assemblies.Length > 1);
			this.comboBoxAssembly.DataSource = assemblies;
			this.comboBoxAssembly.DisplayMember = "FullName";
			smoketest_control = new MainControl(type_under_test);
			smoketest_control.Dock = DockStyle.Fill;
			this.tabPage1.Controls.Add(smoketest_control);
			smoketest_control.constructor_control.ObjectCreated += OnObjectCreated;
			smoketest_control.method_control.ObjectCreated += OnObjectCreated;
			smoketest_control.property_control.ObjectCreated += OnObjectCreated;
			smoketest_control.field_control.ObjectCreated += OnObjectCreated;
			this.comboBoxAssembly.SelectedItem = source;
		}

		internal static List<Type> GetFilteredTypes(Type[] types, Type type_under_test)
		{
			List<Type> filtered = new List<Type>(types.Length);
			foreach (Type type in types) if (type != type_under_test && !type.IsInterface && !type.IsGenericType) filtered.Add(type);
			return filtered;
		}

		public Object Instance
		{
			get { return ((this.instance != null) && this.type_under_test.IsAssignableFrom(this.instance.GetType())) ?this.instance :null; }
		}

		/// <summary>
		/// Helper class to display objects correctly in the Object Pool list box.
		/// </summary>
		class ListItemHelper
		{
			object instance;

			public ListItemHelper(object instance)
			{
				this.instance = instance;
			}

			public object Instance
			{
				get { return this.instance; }
			}

			public string Text
			{
				get { return instance.ToString(); }
			}
		}

		internal static void PopulateObjectList(ListBox list, System.Collections.ICollection objects, Type type)
		{
			Debug.Assert(objects != null);
			Debug.Assert(type != null);
			List<ListItemHelper> filtered = new List<ListItemHelper>();
			foreach (Object instance in objects) if (type.IsAssignableFrom(instance.GetType())) filtered.Add(new ListItemHelper(instance));
			list.DataSource = null;
			list.DisplayMember = "Text";
			list.ValueMember = "Instance";
			list.DataSource = filtered;
		}

		private void RepopulateObjectList(Type type)
		{
			this.buttonSelect.Enabled = false;
			PopulateObjectList(this.listBoxObjects, ObjectPool.pool, type);
		}

		internal void treeTypes_AfterSelect(object sender, TreeViewEventArgs e)
		{
			Type type = e.Node.Tag as Type;
			this.smoketest_control.InitializeType(type);
			RepopulateObjectList(type);
		}

		private void comboBoxAssembly_SelectedIndexChanged(object sender, EventArgs e)
		{
			Assembly assembly = this.comboBoxAssembly.SelectedItem as Assembly;
			Initialize(assembly, this.type_under_test);
		}

		internal void listBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.previewControl.Update(this.listBoxObjects.SelectedValue);
			this.buttonSelect.Enabled = true;
		}

		internal void buttonSelect_Click(object sender, EventArgs e)
		{
			this.instance = this.listBoxObjects.SelectedValue;
		}

		private void listBoxObjects_DoubleClick(object sender, EventArgs e)
		{
			if (this.listBoxObjects.SelectedValue != null)
			{
				EditInstanceForm form = new EditInstanceForm(this.listBoxObjects.SelectedValue);
				form.ShowDialog();
			}
		}

		private void GetInstanceForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			ObjectPool.ObjectAdded -= OnObjectAdded;
		}

		private void GetInstanceForm_Load(object sender, EventArgs e)
		{
			ObjectPool.ObjectAdded += OnObjectAdded;
		}
	}
}
