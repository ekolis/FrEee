using System.Linq;

namespace FrEee.Extensions
{
	public static class StringHandlingExtensions
	{
		public static string CamelCase(this string s)
		{
			return s[0].ToString().ToLowerInvariant() + s.Substring(1);
		}

		public static string Capitalize(this string s)
		{
			if (s == null)
				return null;
			if (s.Length == 0)
				return s;
			return s[0].ToString().ToUpper() + s.Substring(1);
		}

		public static string EscapeBackslashes(this string s)
		{
			return s.Replace("\\", "\\\\");
		}

		public static string EscapeNewlines(this string s)
		{
			return s.Replace("\r", "\\r").Replace("\n", "\\n");
		}

		public static string EscapeQuotes(this string s)
		{
			return s.Replace("'", "\\'").Replace("\"", "\\\"");
		}

		/// <summary>
		/// More concise way of doing string.Format(format, args)
		/// </summary>
		/// <param name="format"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public static string F(this string format, params object[] args)
		{
			return string.Format(format, args);
		}

		public static string LastWord(this string s)
		{
			if (s == null)
				return null;
			return s.Split(' ').LastOrDefault();
		}

		/// <summary>
		/// Gets a possessive form of a noun or pronoun.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="isStart">For "I", is this the first word? For "you" and "she", is this in the subject of the sentence?</param>
		/// <returns></returns>
		public static string Possessive(this string s, bool isStart = false)
		{
			if (s == "I")
				return isStart ? "My" : "my";
			if (s == "we")
				return "our";
			if (s == "We")
				return "Our";
			if (s == "you")
				return isStart ? "your" : "yours";
			if (s == "You")
				return isStart ? "Your" : "Yours";
			if (s == "he" || s == "him")
				return "his";
			if (s == "He")
				return "His";
			if (s == "she" || s == "her")
				return isStart ? "her" : "hers";
			if (s == "She")
				return "Her";
			if (s == "it")
				return "its";
			if (s == "they")
				return "their";
			if (s == "They")
				return "Their";
			if (s == "them")
				return "theirs";

			if (s.EndsWith("s"))
				return s + "'";
			return s + "'s";
		}
	}
}