using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FrEee.Utility
{
	/// <summary>
	/// A reference to a data type which can be safely serialized and cross-version matched.
	/// </summary>
	public class SafeType
	{
		static SafeType()
		{
			RegisterAssembly(Assembly.GetEntryAssembly());
			RegisterAssembly(Assembly.GetExecutingAssembly());

			/*ReferencedAssemblies = LoadReferencedAssemblies().ToDictionary(a => a.GetName().Name);
			ReferencedTypes = new Dictionary<Tuple<Assembly, string>, Type>();
			foreach (var a in ReferencedAssemblies.Values)
			{
				foreach (var t in a.GetTypes())
					ReferencedTypes.Add(Tuple.Create(a, t.FullName), t);
			}*/
		}

		public SafeType(string name)
		{
			Name = name;
		}

		public SafeType(Type type)
		{
			Type = type;
		}

		public string Name { get; set; }

		[DoNotSerialize]
		[JsonIgnore]
		public Type Type
		{
			get
			{
				return Type.GetType(Name,
					assemblyName => FindAssembly(assemblyName.FullName),
					(assembly, typeName, caseInsensitive) =>
					{
						if (caseInsensitive)
							throw new NotSupportedException("Case insensitive type search is not supported.");
						else
						{
							var t = FindType(assembly, typeName);
							if (t == null)
								throw new InvalidOperationException("Type '" + typeName + "' in assembly '" + assembly.FullName + "' was not found. Perhaps this SafeType is referring to an incompatible version of the assembly?");
							return t;
						}
					});
			}
			set
			{
				Name = GetShortTypeName(value);
			}
		}

		internal static string GetShortTypeName(Type t)
		{
			var tname = t.AssemblyQualifiedName;
			tname = Regex.Replace(tname, @", Version=.*, Culture=.*, PublicKeyToken=.*?\],\[", "],[");
			tname = Regex.Replace(tname, @", Version=.*, Culture=.*, PublicKeyToken=.*?\]\]", "]]");
			tname = Regex.Replace(tname, @", Version=.*, Culture=.*, PublicKeyToken=.*?\]", "]");
			tname = Regex.Replace(tname, @", Version=.*, Culture=.*, PublicKeyToken=.*", "");
			return tname;
		}

		private static SafeDictionary<string, Assembly> ReferencedAssemblies { get; set; } = new SafeDictionary<string, Assembly>();

		private static SafeDictionary<Assembly, SafeDictionary<string, Type>> ReferencedTypes { get; set; } = new SafeDictionary<Assembly, SafeDictionary<string, Type>>(true);

		public static void ForceLoadType(Type t)
		{
			RegisterAssembly(t.Assembly);
			FindType(t.Assembly, t.AssemblyQualifiedName);
		}

		public static implicit operator SafeType(Type t)
		{
			return new SafeType(t);
		}

		public static implicit operator Type(SafeType t)
		{
			return t.Type;
		}

		public override string ToString()
		{
			return Name;
		}

		private static Assembly FindAssembly(string n)
		{
			// FrEee, Version=0.0.8.0, Culture=neutral, PublicKeyToken=null
			// ignore all but the assembly's base name since the version etc. could change
			var regex = new Regex($"{n.Substring(0, n.IndexOf(","))}, Version=(.*), Culture=(.*), PublicKeyToken=(.*)");
			foreach (var kvp in ReferencedAssemblies)
			{
				if (regex.IsMatch(kvp.Value.FullName))
					return kvp.Value;
			}

			// no such assembly? find any referenced assemblies and keep on searching
			try
			{
				return FindMoreAssemblies(a => a, a => a.FullName == n);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Could not find assembly named " + n + ".", ex);
			}
		}

		private static T FindMoreAssemblies<T>(Func<Assembly, T> resultor, Func<Assembly, bool> foundit = null)
		{
			bool more = false;
			var done = new List<string>(); // full names of scanned assemblies
			do
			{
				more = false;
				foreach (var a in ReferencedAssemblies.Values.Where(x => !done.Contains(x.FullName)).ToArray())
				{
					if (LoadReferencedAssemblies(a))
						more = true;
					foreach (var a2 in ReferencedAssemblies.Values)
					{
						if (foundit != null)
						{
							// use foundit condition
							if (foundit(a2))
								return resultor(a2);
						}
						else
						{
							// use result being not null as the condition
							var result = resultor(a2);
							if (result != null)
								return result;
						}
						done.Add(a2.FullName);
					}
				}
			} while (more);

			throw new Exception("No assemblies matched the criteria.");
		}

		private static Type FindType(string name)
		{
			// do we already know about it?
			foreach (var a in ReferencedAssemblies.Values)
			{
				var t = FindType(a, name);
				if (t != null)
					return t;
			}

			// scan for new assemblies containing the type
			try
			{
				return FindMoreAssemblies(a => FindType(a, name));
			}
			catch (Exception ex)
			{
				throw new ArgumentException($"Could not find type named {name}.", ex);
			}
		}

		private static Type FindType(Assembly a, string name)
		{
			if (!ReferencedTypes[a].Any())
			{
				foreach (var t in a.GetTypes())
					ReferencedTypes[a][t.FullName] = t;
			}
			var type = ReferencedTypes[a][name];
			return type;
		}

		private static bool LoadReferencedAssemblies(Assembly a)
		{
			bool more = false;
			foreach (var n2 in a.GetReferencedAssemblies().Select(a2 => a2.FullName).Except(ReferencedAssemblies.Values.Select(x => x.FullName)))
			{
				more = true; // discovered assemblies
				var a2 = Assembly.Load(n2);
				RegisterAssembly(a2);
				FindAssembly(n2);
			}
			return more;
		}

		private static void RegisterAssembly(Assembly a)
		{
			if (a != null)
				ReferencedAssemblies[a.GetName().Name] = a;
		}

		/*/// <summary>
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
		}*/
	}
}