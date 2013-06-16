using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads data from plain text.
	/// </summary>
	public class TextLoader : ILoader
	{
		public TextLoader(string[] text, Func<Mod, ICollection<string>> destinationGetter)
		{
			Text = text;
			DestinationGetter = destinationGetter;
		}

		public void Load(Mod mod)
		{
			var dest = DestinationGetter(mod);
			foreach (var s in Text)
				dest.Add(s);
		}

		public string[] Text { get; set; }

		/// <summary>
		/// How Load knows where the text goes in the mod.
		/// </summary>
		public Func<Mod, ICollection<string>> DestinationGetter { get; set; }
	}
}
