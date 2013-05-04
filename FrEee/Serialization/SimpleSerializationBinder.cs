using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace FrEee.Serialization
{
	/// <summary>
	/// Simplistic serialization binder which only considers two "assemblies": System, Collections, and FrEee.
	/// System corresponds to the classes in the System namespace in the System assembly.
	/// Collections corresponds to the classes in the System.Collections.Generic namespace in the System assembly.
	/// FrEee corresponds to the classes in the FrEee.Game namespace in the FrEee assembly.
	/// Everything else is not supported.
	/// </summary>
	public class SimpleSerializationBinder : SerializationBinder
	{
		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			if (serializedType.Assembly == Assembly.GetExecutingAssembly() && serializedType.Namespace == "FrEee.Game")
				assemblyName = "FrEee";
			else if (serializedType.Assembly == Assembly.GetAssembly(typeof(object)) && serializedType.Namespace == "System")
				assemblyName = "System";
			else if (serializedType.Assembly == Assembly.GetAssembly(typeof(object)) && serializedType.Namespace == "System.Collections.Generic")
				assemblyName = "Collections";
			else
				throw new NotSupportedException("Cannot bind " + serializedType.Name + " for serialization; it does not belong to the System or FrEee namespaces.");
			if (serializedType.GetGenericArguments().Length > 0)
			{
				typeName = serializedType.Name.Substring(0, serializedType.Name.IndexOf("`"));
				typeName += "[";
				var parms = new List<Tuple<string, string>>();
				foreach (var parm in serializedType.GetGenericArguments())
				{
					string parmasm, parmname;
					BindToName(parm, out parmasm, out parmname);
					parms.Add(Tuple.Create(parmname, parmasm));
				}
				typeName += string.Join(", ", parms.Select(parm => "[" + parm.Item1 + ", " + parm.Item2 + "]"));
				typeName += "]";
			}
			else
				typeName = serializedType.Name;
		}

		public override Type BindToType(string assemblyName, string typeName)
		{
			Type type;
			if (assemblyName == "FrEee")
				type = Assembly.GetExecutingAssembly().GetType(typeName);
			else if (assemblyName == "System" || assemblyName == "Collections")
				type = Assembly.GetAssembly(typeof(object)).GetType(typeName);
			else
				throw new NotSupportedException("Cannot bind to types in assembly " + assemblyName + ".");

			if (typeName.Contains("["))
			{
				var parms = new List<Type>();
				foreach (var parm in typeName.Substring(typeName.IndexOf("[")).Split(new string[]{"], ["}, StringSplitOptions.None).Select(p => p.Trim(']', '[')))
				{
					var parmtype = parm.Substring(0, parm.IndexOf(", "));
					var parmasm = parm.Substring(parm.IndexOf(", " + 2));
					parms.Add(BindToType(parmasm, parmtype));
				}
				type = type.MakeGenericType(parms.ToArray());
			}

			return type;
		}
	}
}
