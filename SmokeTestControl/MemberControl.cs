using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace net.wesleysteiner.SmokeTestControl
{
	/// <summary>
	/// The base type of each member control type (Contructors, Methods, etc.) to share common functionality.
	/// </summary>
	internal partial class MemberControlPanel : UserControl
	{
		internal const string NULL_TEXT = "(null)";

		private Object lastresult;

		private static string CreateDrilldownButtonText(string text)
		{
			return text + "...";
		}

		internal long average_count = 0;
		internal long average_ms = 0;
		internal long average_ticks = 0;

		internal protected void Reset()
		{
			this.average_count = 0;
			this.average_ms = 0;
			this.average_ticks = 0;
		}

		internal void AddInstanceToPool(ComboBox messages)
		{
			Debug.Assert(messages != null);
			if (ObjectPool.Add(Lastresult)) LogMessage(messages, Utility.GetFriendlyTypeName(Lastresult.GetType()) + " added to Object pool.");
		}

		internal ArgumentsControl arguments_control = new ArgumentsControl();
		internal ComboBox invoke_box;
		internal delegate void ObjectCreatedHandler(Object instance);
		internal event ObjectCreatedHandler ObjectCreated = delegate(Object instance) { };

		internal virtual void OnValidationError(string message)
		{
			MessageBox.Show(message, "Input Error");
			this.invoke_box.Focus();
		}

		protected void ResetCounters(ComboBox messages)
		{
			Reset();
			messages.Items.Clear();
			messages.Enabled = false;
		}

		public uint InvokeCount = 1;

		/// <summary>
		/// Initialize all common subclass controls.
		/// </summary>
		public void InitializeCommonControls(SplitterPanel argument_panel, ComboBox invoke_box)
		{
			this.invoke_box = invoke_box;
			this.invoke_box.Text = Convert.ToString(this.InvokeCount);
			this.invoke_box.Validating += new System.ComponentModel.CancelEventHandler(invoke_box_Validating);
			argument_panel.Controls.Add(this.arguments_control);
		}

		internal void invoke_box_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			uint temp;
			if (uint.TryParse(this.invoke_box.Text, out temp) && (temp > 0)) this.InvokeCount = temp;
			else
			{
				OnValidationError("Enter a valid whole number greater than 0.");
			}
		}

		/// <summary>
		/// Return the last instance object created using this member control.
		/// </summary>
		public Object Lastresult
		{
			get 
			{ 
				return this.lastresult; 
			}
			internal set 
			{ 
				this.lastresult = value;
				if (value != null)
				{
					ObjectCreated(this.lastresult);
				}
			}
		}

		internal static void LogMessage(ComboBox messages, string message)
		{
			Debug.Assert(messages != null);
			Debug.Assert(message != null);
			messages.Items.Insert(0, message);
			messages.SelectedIndex = 0;
			messages.Enabled = (messages.Items.Count > 1);
		}

		/// <summary>
		/// Update the controls for a given instance object.
		/// </summary>
		public Object ProcessLastresult(String operation, Object instance, Button drilldown, Button add_to_pool, long ticks, long milliseconds, ComboBox messages)
		{
			++average_count;
			string message = String.Format("{0} ticks ({1} ms) to {2}", ticks, milliseconds, operation);
			if (this.InvokeCount > 1) message += String.Format(" {0} times", this.InvokeCount);
			message += '.';
			if (average_count > 1)
			{
				average_ticks += ticks;
				average_ms += milliseconds;
				if (average_count > 2)
				{
					message += String.Format(" Average {0} ticks ({1} ms) over {2} invocations.", average_ticks / (average_count - 1), average_ms / (average_count - 1), average_count - 1);
				}
			}
			LogMessage(messages, message);
			if (instance == null) drilldown.Hide();
			else
			{
				drilldown.Show();
				drilldown.Text = CreateDrilldownButtonText(Utility.GetFriendlyTypeName(instance.GetType()));
			}
			add_to_pool.Visible = drilldown.Visible;
			return instance;
		}

		/// <summary>
		/// Populate the next row of the argument table panel with controls for the given type.
		/// </summary>
		public void PopulateNextArgumentRow(Assembly assembly, Type type, string name)
		{
			TableLayoutPanel panel = this.arguments_control.tableLayoutPanel;
			Button name_button = new Button();
			name_button.FlatStyle = FlatStyle.Popup;
			name_button.BackColor = System.Drawing.Color.WhiteSmoke;
			name_button.Text = name + ':';
			name_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			name_button.Enabled = false;
			name_button.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			Control input_control = null;
			if (type == typeof(Char))
			{
				input_control = new LiteralValueControl<Char>("A", 1);
			}
			else if (type == typeof(Byte))
			{
				input_control = new LiteralValueControl<Byte>("0");
			}
			else if (type == typeof(SByte))
			{
				input_control = new LiteralValueControl<SByte>("0");
			}
			else if (type == typeof(Int16))
			{
				input_control = new LiteralValueControl<Int16>("0");
			}
			else if (type == typeof(Int32))
			{
				input_control = new LiteralValueControl<Int32>("0");
			}
			else if (type == typeof(Int64))
			{
				input_control = new LiteralValueControl<Int64>("0");
			}
			else if (type == typeof(UInt16))
			{
				input_control = new LiteralValueControl<UInt16>("0");
			}
			else if (type == typeof(UInt32))
			{
				input_control = new LiteralValueControl<UInt32>("0");
			}
			else if (type == typeof(UInt64))
			{
				input_control = new LiteralValueControl<UInt64>("0");
			}
			else if (type == typeof(double))
			{
				input_control = new LiteralValueControl<Double>("0");
			}
			else if (type == typeof(decimal))
			{
				input_control = new LiteralValueControl<Decimal>("0");
			}
			else if (type == typeof(Single))
			{
				input_control = new LiteralValueControl<Single>("0");
			}
			else if (type == typeof(String))
			{
				input_control = new StringValueControl(String.Empty);
			}
			else if (type == typeof(DateTime))
			{
				input_control = new LiteralValueControl<DateTime>(DateTime.Today.ToShortDateString());
			}
			else if (type.IsEnum)
			{
				input_control = new EnumValueControl(type);
				input_control.Tag = Enum.Parse(type, input_control.Text, true);
			}
			else if (type == typeof(Boolean))
			{
				input_control = new BooleanValueControl();
			}
			else if (Utility.IsConstructable(type))
			{
			    input_control = new ReferenceValueControl(type);
				input_control.Text = NULL_TEXT;
				if (HasDefaultConstructor(type))
				{
					input_control.Tag = Activator.CreateInstance(type);
					input_control.Text = Utility.CreateEditableInstanceText(input_control.Tag);
				}
			}
			else
			{
				input_control = new UnsupportedArgumentControl(type);
			}
			ArgumentTypeControl type_control = new ArgumentTypeControl(assembly, type, input_control);
			NullCheckBox null_check_box = new NullCheckBox(input_control, type_control, Utility.IsSpecialReferenceType(type) || Utility.IsConstructable(type));
			null_check_box.Checked = null_check_box.Enabled || IsLiteralArgument(type) || type.IsEnum;
			panel.Controls.Add(null_check_box);
			panel.Controls.Add(name_button);
			input_control.Enabled = Utility.IsEditable(type, input_control.Tag);
			input_control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			input_control.AutoSize = true;
			panel.Controls.Add(input_control);
			panel.Controls.Add(type_control);
		}

		private static bool HasDefaultConstructor(Type type)
		{
			return type.GetConstructor(Type.EmptyTypes) != null;
		}

		internal void PopulateNextArgumentRow(Type type, string name)
		{
			PopulateNextArgumentRow(type.Assembly, type, name);
		}

		void OnNameButtonClick(object sender, EventArgs e)
		{
			EditInstanceForm form = new EditInstanceForm(((Control)sender).Tag);
			form.ShowDialog();
		}

		private static bool IsLiteralArgument(Type type)
		{
			return type.IsPrimitive || type == typeof(Decimal);
		}

		public void PopulateArgumentTable(Assembly assembly, ParameterInfo[] parameters)
		{
			this.arguments_control.tableLayoutPanel.Controls.Clear();
			foreach (ParameterInfo parameter in parameters) PopulateNextArgumentRow(assembly, parameter.ParameterType, parameter.Name);
		}

		public Object[] GetArguments()
		{
			TableLayoutPanel panel = this.arguments_control.tableLayoutPanel;
			int n_args = panel.Controls.Count / panel.ColumnCount;
			Object[] arguments = new Object[n_args];
			for (int row = 0; row < n_args; ++row)
			{
				CheckBox checkbox = (panel.Controls[row * panel.ColumnCount]) as CheckBox;
				if ((checkbox != null) && !checkbox.Checked)
				{
					arguments[row] = null;
				}
				else
				{
					Control control = (Control)panel.Controls[row * panel.ColumnCount + ArgumentsControl.Control.Index.INPUT];
					Button argument_type = (Button)panel.Controls[row * panel.ColumnCount + ArgumentsControl.Control.Index.TYPE];
					if (IsLiteralArgument((Type)argument_type.Tag) || Utility.IsSpecialReferenceType((Type)argument_type.Tag)) arguments[row] = Convert.ChangeType(control.Text, (Type)argument_type.Tag);
					else arguments[row] = control.Tag;
				}
			}
			return arguments;
		}
	}
}