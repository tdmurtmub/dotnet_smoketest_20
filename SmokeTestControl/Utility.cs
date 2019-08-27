using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using net.wesleysteiner.utility;

namespace net.wesleysteiner.SmokeTestControl
{
	internal class Utility
	{
		internal static bool IsSpecialReferenceType(Type type)
		{
			return (type == typeof(String)) || (type == typeof(DateTime));
		}

		/// <summary>
		/// Returns in-place editability of a Type.
		/// </summary>
		/// <param name="type">Type under test.</param>
		/// <returns>True if type is editable in-place (text box).</returns>
		internal static bool IsInplaceEditable(Type type)
		{
			return type.IsPrimitive || type.IsEnum || type == typeof(Decimal) || Utility.IsSpecialReferenceType(type);
		}

		internal static bool IsConstructable(Type type)
		{
			return !(type.IsEnum || type.IsPrimitive || type.IsGenericType);
		}

		internal static bool IsEditable(Type type, Object instance)
		{
			return IsInplaceEditable(type) || (instance != null);
		}

		public static String GetFriendlyTypeName(Type type)
		{
			return type.Name;
		}

		public static string CreateEditableInstanceText(Object instance)
		{
			if (IsInplaceEditable(instance.GetType())) return instance.ToString(); 
			else return '(' + Utility.GetFriendlyTypeName(instance.GetType()) + ')';
		}

		public static string GetFriendlyParameterList(ConstructorInfo method)
		{
			return String.Format("({0})", String.Join(",", CollectionConverter<ParameterInfo, string>.ToArray(method.GetParameters(), delegate(ParameterInfo info) { return Utility.GetFriendlyTypeName(info.ParameterType); })));
		}

		public static string GetFriendlyParameterList(MethodInfo method)
		{
			return String.Format("({0})", String.Join(",", CollectionConverter<ParameterInfo, string>.ToArray(method.GetParameters(), delegate(ParameterInfo info) { return Utility.GetFriendlyTypeName(info.ParameterType); })));
		}

		public static string GetFriendlyAssemblyName(Assembly assembly)
		{
			return assembly.GetName().Name;
		}

		public static Assembly[] FilterAssemblies(Assembly[] assemblies, Type type)
		{
			Debug.Assert(assemblies != null);
			Debug.Assert(type != null);
			List<Assembly> results = new List<Assembly>();
			foreach (Assembly assembly in assemblies)
			{
				foreach (Type t in assembly.GetTypes())
				{
					if ((t == type) || t.IsSubclassOf(type))
					{
						results.Add(assembly);
						break;
					}
				}
			}
			return results.ToArray();
		}
	}
}
