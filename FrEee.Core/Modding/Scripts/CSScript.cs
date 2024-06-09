using FrEee.Modding.Scripts;
using FrEee.Utility;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Immutable;
using System.IO;
using FrEee.Utility;

namespace FrEee.Modding;

[Serializable]
public class CSScript : IScript
	{
		public string ModuleName { get; set; }
		public string Text { get; set; }

	/// <summary>
	/// The directory that this script is found in. 
	/// </summary>
	public string Directory { get; set; }



	public CSScript(string moduleName, string text, string directory)
	{
		ModuleName = moduleName;
		Text = text;
		Directory = directory;
	}

	/// <summary>
	/// Loads the CS Script from the files. 
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static CSScript Load(string path)
	{
		var scriptFilename = Path.Combine(path + ".script");
		var pythonFilename = Path.Combine(path + ".csx");
		var moduleName = Path.GetFileName(path);
		if (!File.Exists(pythonFilename))
			return null;
		var text = File.ReadAllText(pythonFilename);
		
		return new CSScript(moduleName, text, Path.GetDirectoryName(path));
	}



	 
	/// <summary>
	/// Creates a delegate from which the script can be repeatably called. 
	/// </summary>
	/// <typeparam name="TGlobalType">The type that will be passed into the script</typeparam>
	/// <typeparam name="TReturn">What the script will return</typeparam>
	/// <returns></returns>
	public Microsoft.CodeAnalysis.Scripting.ScriptRunner<TReturn> CreateScriptObject<TGlobalType, TReturn>()
	{
		//the imports here are a list of usings that the script will have access to by default. 
		//The script can suggest additional usings and will work fine, as long as they are either system, or in CSScript.Assembly. 
		//The source file resolver is where it looks for additional scripts, loaded by putting #load <filename> in the script file. 
		//By defauly, the resolver looks in the same directory as the script. 
		var options = ScriptOptions.Default.AddImports("System", "System.Linq", "FrEee", "FrEee.Objects.AI",
			"FrEee.Objects.Civilization", "FrEee.Objects.Space", "FrEee.Utility", "FrEee.Objects.Commands",
			"FrEee.Modding.Templates", "FrEee.Modding")
			.WithReferences(typeof(CSScript).Assembly).
			WithSourceResolver(new SourceFileResolver(ImmutableArray<string>.Empty, Directory));
		return CSharpScript.Create<TReturn>(Text, options, typeof(TGlobalType)).CreateDelegate();
	}



	public static bool operator !=(CSScript s1, CSScript s2)
	{
		return !(s1 == s2);
	}

	public static bool operator ==(CSScript s1, CSScript s2)
	{
		if (s1 is null && s2 is null)
			return true;
		if (s1 is null || s2 is null)
			return false;
		return s1.ModuleName == s2.ModuleName && s1.Text == s2.Text;// && s1.ExternalScripts.SafeSequenceEqual(s2.ExternalScripts);
	}

	public override bool Equals(object? obj)
	{
		if (obj is CSScript)
		{
			var s = (CSScript)obj;
			return s == this;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return HashCodeMasher.Mash(ModuleName, Text);//, HashCodeMasher.MashList(ExternalScripts));
	}

	public override string ToString()
	{
		return ModuleName;
	}
}
