using FrEee.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FrEee.Utility;

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
		tpy
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
			if (Name.Contains("Version="))
				return Type.GetType(Name); // legacy junk
			if (Name.Contains("[["))
			{
				var regex = new Regex(@"(.*?)(\[\[.*\]\]), (.*)");
				var match = regex.Match(Name);
				var tname = match.Groups[1].Captures[0].Value.Trim(); // type name
				var gtparmnames = match.Groups[2].Captures[0].Value.Trim(); // generic type parameter names
				var aname = match.Groups[3].Captures[0].Value.Trim(); // assembly name
				if (aname.Contains(","))
					aname = aname.Substring(0, aname.IndexOf(",")); // strip legacy junk
																	// check plugins
				try
				{
					// try to load plugin assembly
					var assembly = Assembly.LoadFile(Path.GetFullPath(Path.Combine("Plugins", aname + ".dll")));
					ReferencedAssemblies.Add(assembly.GetName().Name, assembly);
				}
				catch
				{
					// not a plugin assembly
				}

				var t = FindType(tname);
				var gtparmnameslist = gtparmnames.Trim('[', ']').Split(',');
				var gtparmnameslist2 = new List<string>();
				var brackets = 0;
				foreach (var gtparmname in gtparmnameslist)
				{
					var gtparmname2 = gtparmname.Trim();
					if (gtparmname2.StartsWith("Version") || gtparmname2.StartsWith("Culture") || gtparmname2.StartsWith("PublicKeyToken"))
						continue; // legacy junk
					if (brackets == 0)
						gtparmnameslist2.Add(gtparmname2);
					else
						gtparmnameslist2[gtparmnameslist2.Count - 1] += ", " + gtparmname2;
					if (gtparmname2.Contains("[["))
						brackets++;
					if (gtparmname2.Contains("]]"))
						brackets--;
				}
				var gtparmnameslist3 = new List<string>();
				for (var i = 0; i < gtparmnameslist2.Count; i++)
				{
					if (i % 2 == 0)
						gtparmnameslist3.Add(gtparmnameslist2[i]);
					else
						gtparmnameslist3[gtparmnameslist3.Count - 1] += ", " + gtparmnameslist2[i];
				}
				for (var i = 0; i < gtparmnameslist3.Count; i++)
				{
					gtparmnameslist3[i] = gtparmnameslist3[i].Trim('[', ']');
				}
				if (gtparmnameslist3.Last().Contains("[[") && !gtparmnameslist3.Last().Contains("]]"))
					gtparmnameslist3[gtparmnameslist3.Count - 1] += "]], " + aname;
				else if (!gtparmnameslist3.Last().Contains(","))
					gtparmnameslist3[gtparmnameslist3.Count - 1] += ", " + aname;
				var gtparms = gtparmnameslist3.Select(x => new SafeType(x).Type).ToArray();
				return t.MakeGenericType(gtparms.Where(x => x != null).ToArray()); // HACK - nulls are getting in due to assembly names appearing twice or something
			}
			else
			{
				var regex = new Regex(@"(.*?), (.*)");
				var match = regex.Match(Name);
				var tname = match.Groups[1].Captures[0].Value.Trim(); // type name
				var aname = match.Groups[2].Captures[0].Value.Trim().Trim('[', ']'); // assembly name
				var t = FindType(FindAssembly(aname), tname);
				return t;
			}
		}
		set
		{
			Name = GetShortTypeName(value);
		}
	}

	private static List<(Regex, string)> shortTypeNameRegexes { get; } = new List<(Regex, string)>
	{
		(new Regex(@"(.*?), (.*?), Version=.*?, Culture=.*?, PublicKeyToken=.*?\],\[", RegexOptions.Compiled),  "$1, $2],["),
		(new Regex(@"(.*?), (.*?), Version=.*?, Culture=.*?, PublicKeyToken=.*?\]\]", RegexOptions.Compiled),  "$1, $2]]"),
		(new Regex(@"(.*?), (.*?), Version=.*?, Culture=.*?, PublicKeyToken=.*?\]", RegexOptions.Compiled),  "$1, $2]"),
		(new Regex(@"(.*?), (.*?), Version=.*?, Culture=.*?, PublicKeyToken=.*\z", RegexOptions.Compiled),  "$1, $2"),
	};

	public static string GetShortTypeName(Type t)
	{
		if (!ShortTypeNames.ContainsKey(t))
		{
			var tname = t.AssemblyQualifiedName;
			foreach (var r in shortTypeNameRegexes)
				tname = r.Item1.Replace(tname, r.Item2);
			ShortTypeNames[t] = tname;
		}
		return ShortTypeNames[t];
	}

	private static SafeDictionary<Type, string> ShortTypeNames { get; set; } = new SafeDictionary<Type, string>();

	private static SafeDictionary<string, Assembly> ReferencedAssemblies { get; set; } = new SafeDictionary<string, Assembly>();

	private static SafeDictionary<Assembly, SafeDictionary<string, Type>> ReferencedTypes { get; set; } = new SafeDictionary<Assembly, SafeDictionary<string, Type>>(true);

	public static void ForceLoadType(Type t)
	{
		RegisterAssembly(t.Assembly);
		FindType(t.Assembly, GetShortTypeName(t));
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
		var whereIsComma = n.IndexOf(",");
		if (whereIsComma < 0)
			whereIsComma = n.Length;
		var regex = new Regex($"{n.Substring(0, whereIsComma)}, Version=(.*), Culture=(.*), PublicKeyToken=(.*)");
		foreach (var kvp in ReferencedAssemblies)
		{
			if (regex.IsMatch(kvp.Value.FullName))
				return kvp.Value;
		}

		// no such assembly? find any referenced assemblies and keep on searching
		try
		{
			return FindMoreAssemblies(a => a, a => a.FullName.Substring(0, a.FullName.IndexOf(",")) == n);
		}
		catch (Exception ex)
		{
			// check plugins
			var assembly = Assembly.LoadFile(Path.GetFullPath(Path.Combine("Plugins", n + ".dll")));
			ReferencedAssemblies.Add(assembly.GetName().Name, assembly);
			return assembly;
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
		int arrayDim = 0;
		if (name.EndsWith("[]"))
			arrayDim = 1;
		else if (name.EndsWith("[,]"))
			arrayDim = 2;

		var realname = name.TrimEnd('[', ',', ']');

		// do we already know about it?
		if (!ReferencedTypes[a].Any())
		{
			foreach (var t in a.GetTypes())
				ReferencedTypes[a][t.FullName] = t;
		}
		var type = ReferencedTypes[a][realname];
		if (arrayDim == 0)
			return type;
		else if (arrayDim == 1)
			return type.MakeArrayType();
		else
			return type.MakeArrayType(arrayDim);
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
