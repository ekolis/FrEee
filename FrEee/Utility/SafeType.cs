using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace FrEee.Utility
{
	/// <summary>
	/// A reference to a data type which can be safely serialized and cross-version matched.
	/// </summary>
	public class SafeType
	{
		public SafeType(string name)
		{
			Name = name;
		}

		public SafeType(Type type)
		{
			Type = type;
		}

		#region private static initializers
		static SafeType()
		{
			ReferencedAssemblies = LoadReferencedAssemblies().ToDictionary(a => a.GetName().Name);
			ReferencedTypes = new Dictionary<Tuple<Assembly, string>, Type>();
			foreach (var a in ReferencedAssemblies.Values)
			{
				foreach (var t in a.GetTypes())
					ReferencedTypes.Add(Tuple.Create(a, t.FullName), t);
			}
		}

		public static void ForceLoadType(Type t)
		{
			var an = t.Assembly.GetName().Name;
			if (!ReferencedAssemblies.ContainsKey(an))
				ReferencedAssemblies.Add(an, t.Assembly);
			var tuple = Tuple.Create(t.Assembly, t.FullName);
			if (!ReferencedTypes.ContainsKey(tuple))
				ReferencedTypes.Add(tuple, t);
		}

		/// <summary>
		/// Finds and loads all referenced assemblies from a given root assembly, recursively.
		/// </summary>
		/// <param name="rootAssembly">The root assembly. If not specified, Assembly.GetEntryAssembly() and Assembly.GetExecutingAssembly() will be used.</param>
		/// <param name="alreadyLoaded">Any already-loaded assemblies. Apparently built-in CLR assemblies are allowed to have circular references?</param>
		/// <returns></returns>
		private static IEnumerable<Assembly> LoadReferencedAssemblies(Assembly rootAssembly = null, ISet<Assembly> alreadyLoaded = null)
		{
			if (alreadyLoaded == null)
				alreadyLoaded = new HashSet<Assembly>();
			if (rootAssembly == null)
			{
				if (Assembly.GetEntryAssembly() != null) // python scripts don't have an entry assembly I guess?
					LoadReferencedAssemblies(Assembly.GetEntryAssembly(), alreadyLoaded);
				LoadReferencedAssemblies(Assembly.GetExecutingAssembly(), alreadyLoaded);
			}
			else
			{
				alreadyLoaded.Add(rootAssembly);
				foreach (var subAssemblyName in rootAssembly.GetReferencedAssemblies())
				{
					var subAssembly = Assembly.Load(subAssemblyName);
					if (!alreadyLoaded.Contains(subAssembly))
						LoadReferencedAssemblies(subAssembly, alreadyLoaded);
				}
			}
			return alreadyLoaded;
		}
		#endregion

		private static IDictionary<string, Assembly> ReferencedAssemblies { get; set; }
		private static IDictionary<Tuple<Assembly, string>, Type> ReferencedTypes { get; set; }

		public string Name { get; set; }

		[DoNotSerialize]
		[JsonIgnore]
		public Type Type
		{
			get
			{
				return Type.GetType(Name,
					assemblyName => ReferencedAssemblies[assemblyName.Name],
					(assembly, typeName, caseInsensitive) =>
					{
						if (caseInsensitive)
							throw new NotSupportedException("Case insensitive type search is not supported.");
						else
						{
							try
							{
								return ReferencedTypes[Tuple.Create(assembly, typeName)];
							}
							catch (KeyNotFoundException ex)
							{
								throw new InvalidOperationException("Type '" + typeName + "' in assembly '" + assembly.FullName + "' was not found. Perhaps this SafeType is referring to an incompatible version of the assembly?", ex);
							}
						}
					});
			}
			set
			{
				Name = value.AssemblyQualifiedName;
			}
		}

		public static implicit operator Type(SafeType t)
		{
			return t.Type;
		}

		public static implicit operator SafeType(Type t)
		{
			return new SafeType(t);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
