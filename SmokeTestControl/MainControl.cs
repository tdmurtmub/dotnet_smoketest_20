using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// A user control to "smoketest" a type or an instance of a type.
	/// </summary>
	public partial class MainControl : UserControl
	{
		class MemberPrettyPrint
		{
			internal MemberInfo member_info;

			public MemberPrettyPrint(MemberInfo info)
			{
				member_info = info;
			}

			public override string ToString()
			{
				string text = member_info.Name;
				if (member_info.MemberType == MemberTypes.Constructor)
				{
					text = member_info.DeclaringType.Name;
					text += String.Format("{0}", Utility.GetFriendlyParameterList((ConstructorInfo)member_info));
				}
				else if (member_info.MemberType == MemberTypes.Method)
				{
					text += String.Format("{0}:{1}", Utility.GetFriendlyParameterList((MethodInfo)member_info), Utility.GetFriendlyTypeName(((MethodInfo)member_info).ReturnType));
				}
				else if (member_info.MemberType == MemberTypes.Property)
				{
					text += ":" + Utility.GetFriendlyTypeName(((PropertyInfo)member_info).PropertyType);
				}
				else if (member_info.MemberType == MemberTypes.Field)
				{
					text += ":" + Utility.GetFriendlyTypeName(((FieldInfo)member_info).FieldType);
				}
				return text;
			}
		}

		internal ConstructorControlPanel constructor_control = new ConstructorControlPanel();
		internal FieldControlPanel field_control = new FieldControlPanel();
		internal MethodControlPanel method_control = new MethodControlPanel();
		internal Object instance_under_test;
		internal PropertyControlPanel property_control = new PropertyControlPanel();

		/// <summary>
		/// Returns true if the given type is 'smoke-testable'.
		/// </summary>
		public static bool IsSmokeTestable(Type type)
		{
			return type.IsEnum || type.IsValueType || (type.IsClass && !type.IsGenericType);
		}

		/// <summary>
		/// Instantiate the control for no specific Type or Object.
		/// </summary>
		public MainControl ()
		{
			InitializeComponent();
			Construct();
		}

		/// <summary>
		/// Instantiate the control for a Type.
		/// </summary>
		public MainControl(Type type)
		{
			InitializeComponent();
			Construct();
			InitializeType(type);
		}

		/// <summary>
		/// Instantiate the control for an object instance.
		/// </summary>
		public MainControl(Object instance)
		{
			InitializeComponent();
			Construct();
			this.instance_under_test = instance;
			PopulateMemberLists(instance.GetType(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.tabControlMembers.TabPages.RemoveByKey("constructors_TabPage");
		}

		/// <summary>
		/// Initialize controls for the given Type.
		/// </summary>
		public void InitializeType(Type type)
		{
			PopulateMemberLists(type, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>
		/// Populate the Constructors tab controls for the given type.
		/// </summary>
		internal void PopulateConstructorsTab(Type type)
		{
			Debug.Assert(type != null);
			this.constructor_control.Initialize();
			this.listBoxConstructors.Items.Clear();
			if (type == null) return;
			this.listBoxConstructors.BeginUpdate();
			foreach (ConstructorInfo member in type.GetConstructors(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance)) if (!member.Name.StartsWith(".cctor")) this.listBoxConstructors.Items.Add(new MemberPrettyPrint(member));
			this.listBoxConstructors.EndUpdate();
		}

		/// <summary>
		/// Populate the Methods tab controls for the given type.
		/// </summary>
		internal void PopulateMethodsTab(Type type)
		{
			Debug.Assert(type != null);
			PopulateMethodsTab(type, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>
		/// Populate the Properties tab controls for the given type.
		/// </summary>
		internal void PopulatePropertiesTab(Type type)
		{
			Debug.Assert(type != null);
			PopulatePropertiesTab(type, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>
		/// Populate the Methods tab controls for the given type and binding.
		/// </summary>
		private void PopulateMethodsTab(Type type, BindingFlags binding)
		{
			Debug.Assert(type != null);
			this.method_control.Initialize();
			this.listBoxMethods.Items.Clear();
			this.listBoxMethods.BeginUpdate();
			foreach (MethodInfo method in type.GetMethods(binding))
			{
				if (!method.IsGenericMethod && !method.Name.StartsWith("get_") && !method.Name.StartsWith("set_") && !method.Name.StartsWith("add_") && !method.Name.StartsWith("remove_") && !method.Name.StartsWith("<.cctor>")) this.listBoxMethods.Items.Add(new MemberPrettyPrint(method));
			}
			this.listBoxMethods.EndUpdate();
		}

		private void Construct()
		{
			this.constructor_control.Dock = DockStyle.Fill;
			this.method_control.Dock = DockStyle.Fill;
			this.property_control.Dock = DockStyle.Fill;
			this.field_control.Dock = DockStyle.Fill;
			this.splitContainerConstructors.Panel2.Controls.Add(constructor_control);
			this.splitContainerMethods.Panel2.Controls.Add(method_control);
			this.splitContainerProperties.Panel2.Controls.Add(property_control);
			this.splitContainerFields.Panel2.Controls.Add(field_control);
		}

		/// <summary>
		/// Populate the Properties tab controls for the given type and binding.
		/// </summary>
		private void PopulatePropertiesTab(Type type, BindingFlags binding)
		{
			Debug.Assert(type != null);
			this.property_control.Initialize();
			this.listBoxProperties.Items.Clear();
			this.listBoxProperties.BeginUpdate();
			foreach (PropertyInfo property in type.GetProperties(binding))
			{
				if (property.GetIndexParameters().Length == 0) this.listBoxProperties.Items.Add(new MemberPrettyPrint(property));
			}
			this.listBoxProperties.EndUpdate();
		}

		/// <summary>
		/// Populate the Fields tab controls for the given type and binding.
		/// </summary>
		private void PopulateFieldsTab(Type type, BindingFlags binding)
		{
			Debug.Assert(type != null);
			this.field_control.Initialize();
			this.listBoxFields.Items.Clear();
			this.listBoxFields.BeginUpdate();
			foreach (FieldInfo field in type.GetFields(binding))
			{
				if (!field.Name.StartsWith("CS$")) this.listBoxFields.Items.Add(new MemberPrettyPrint(field));
			}
			this.listBoxFields.EndUpdate();
		}

		/// <summary>
		/// Populate the Fields tab controls for the given type.
		/// </summary>
		internal void PopulateFieldsTab(Type type)
		{
			Debug.Assert(type != null);
			PopulateFieldsTab(type, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>
		/// Populate each member list control for the given type.
		/// </summary>
		internal void PopulateMemberLists (Type type, BindingFlags bindings)
		{
			Debug.Assert(type != null);
			PopulateConstructorsTab(type);
			PopulateMethodsTab(type, bindings);
			PopulatePropertiesTab(type, bindings);
			PopulateFieldsTab(type, bindings);
		}

		internal void listBoxProperties_SelectedIndexChanged(object sender, EventArgs e)
		{
			Object item = listBoxProperties.SelectedItem;
			if (item != null)
			{
				this.property_control.Initialize(this.instance_under_test, (PropertyInfo)((MemberPrettyPrint)item).member_info);
			}
		}

		internal void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
		{
			Object item = listBoxFields.SelectedItem;
			if (item != null)
			{
				this.field_control.Initialize(this.instance_under_test, (FieldInfo)((MemberPrettyPrint)item).member_info);
			}
		}

		internal void OnLoad(object sender, EventArgs e)
		{
		}

		internal void listBoxConstructors_SelectedIndexChanged(object sender, EventArgs e)
		{
			Object item = listBoxConstructors.SelectedItem;
			if (item != null)
			{
				this.constructor_control.Initialize((ConstructorInfo)((MemberPrettyPrint)item).member_info);
			}
		}

		internal void listBoxMethods_SelectedIndexChanged(object sender, EventArgs e)
		{
			Object item = listBoxMethods.SelectedItem;
			if (item != null)
			{
				this.method_control.Initialize(this.instance_under_test, (MethodInfo)((MemberPrettyPrint)item).member_info);
			}
		}
	}
}