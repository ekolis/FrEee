using FrEee.Interfaces;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding
{
	/// <summary>
	/// A record which takes parameters and is capable of generating multiple records using formulas.
	/// </summary>
	public class MetaRecord : Record, ITemplate<IEnumerable<Record>>
	{
		public MetaRecord()
			: base()
		{ }

		/// <summary>
		/// Creates a record by parsing some string data.
		/// </summary>
		/// <param name="lines"></param>
		public MetaRecord(IEnumerable<string> lines)
			: base(lines)
		{ }

		public string Filename { get; set; }

		public IEnumerable<MetaRecordParameter> Parameters
		{
			get
			{
				string expecting = "Name";
				var p = new MetaRecordParameter();
				foreach (var f in Fields.Where(f => f.Name.StartsWith("Parameter ")))
				{
					var what = f.Name.Substring("Parameter ".Length);
					if (expecting == "Name")
					{
						if (what == "Name")
						{
							p.Name = f.Value;
							expecting = "Minimum";
						}
						else
						{
							Mod.Errors.Add(new DataParsingException("Expected Parameter Name, found Parameter " + what + ".", Filename, this, f));
							yield break;
						}
					}
					else if (expecting == "Minimum")
					{
						if (what == "Minimum")
						{
							p.Minimum = f.CreateFormula<int>(p);
							expecting = "Maximum";
						}
						else if (what == "Maximum")
						{
							// it's OK, just set minimum to 1
							p.Minimum = 1;
							p.Maximum = f.CreateFormula<int>(p);
							expecting = "Name";
							yield return p;
							p = new MetaRecordParameter();
						}
						else
						{
							Mod.Errors.Add(new DataParsingException("Expected Parameter Minimum or Parameter Maximum, found Parameter " + what + ".", Filename, this, f));
							yield break;
						}
					}
					else if (expecting == "Maximum")
					{
						if (what == "Maximum")
						{
							p.Maximum = f.CreateFormula<int>(p);
							expecting = "Name";
							yield return p;
							p = new MetaRecordParameter();
						}
						else
						{
							Mod.Errors.Add(new DataParsingException("Expected Parameter Maximum, found Parameter " + what + ".", Filename, this, f));
							yield break;
						}
					}
				}
				if (expecting != "Name")
					Mod.Errors.Add(new DataParsingException("Expected Parameter Minimum or Parameter Maximum but no more parameter fields were found.", Filename, this));
			}
		}

		public IEnumerable<Record> Instantiate(Game game)
		{
			var parms = Parameters.ToArray();
			if (!parms.Any())
			{
				var rec = new Record();
				foreach (var f in Fields)
					rec.Fields.Add(f.Copy());
				yield return rec;
				yield break;
			}
			IList<IDictionary<string, int>> permutations = null;
			foreach (var parm in parms)
				permutations = CreatePermutations(parm, permutations);
			foreach (var permutation in permutations)
			{
				var rec = new Record();
				rec.Parameters = new Dictionary<string, object>();
				foreach (var kvp in permutation)
					rec.Parameters.Add(kvp.Key, kvp.Value);
				foreach (var f in Fields)
				{
					if (!f.Name.StartsWith("Parameter "))
					{
						if (f.Value.StartsWith("=="))
						{
							// dynamic formula field will be evaluated later
							rec.Fields.Add(f.Copy());
						}
						else if (f.Value.StartsWith("="))
						{
							// static formula field
							rec.Fields.Add(CreateStaticFormulaField(f, permutation));
						}
						else if (f.Value.Contains("{") && f.Value.Substring(f.Value.IndexOf("{")).Contains("}"))
						{
							// string interpolation formula
							var isDynamic = f.Value.Contains("{{") && f.Value.Substring(f.Value.IndexOf("{{")).Contains("}}");
							var replacedText = f.Value;
							if (isDynamic)
								replacedText = "=='" + replacedText + "'"; // make it a string
							else
								replacedText = "='" + replacedText + "'"; // make it a string
							replacedText = replacedText.Replace("{{", "' + str(");
							replacedText = replacedText.Replace("}}", ") + '");
							replacedText = replacedText.Replace("{", "' + str(");
							replacedText = replacedText.Replace("}", ") + '");
							f.Value = replacedText;
							if (isDynamic)
								throw new NotImplementedException("Dynamic inline formulas are not yet supported.");
							else
								rec.Fields.Add(CreateStaticFormulaField(f, permutation));
						}
						else
						{
							// plain old field
							rec.Fields.Add(f.Copy());
						}
					}
				}
				yield return rec;
			}
		}

		private static Field CreateStaticFormulaField(Field f, IDictionary<string, int> args)
		{
			var variables = args.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
			var result = PythonScriptEngine.EvaluateExpression<IConvertible>(f.Value.TrimStart('='), variables).ToStringInvariant();
			var field = new Field();
			field.Name = f.Name;
			field.Value = result;
			return field;
		}

		private IList<IDictionary<string, int>> CreatePermutations(MetaRecordParameter parm, IList<IDictionary<string, int>> previous = null)
		{
			var cur = new List<IDictionary<string, int>>();
			if (previous == null || previous.Count == 0)
			{
				for (int i = parm.Minimum; i <= parm.Maximum; i++)
				{
					var newdict = new Dictionary<string, int>();
					newdict.Add(parm.Name, i);
					cur.Add(newdict);
				}
			}
			else
			{
				foreach (var dict in previous)
				{
					for (int i = parm.Minimum; i <= parm.Maximum; i++)
					{
						var newdict = new Dictionary<string, int>();
						foreach (var kvp in dict)
							newdict.Add(kvp.Key, kvp.Value);
						newdict.Add(parm.Name, i);
						cur.Add(newdict);
					}
				}
			}
			return cur;
		}
	}
}
