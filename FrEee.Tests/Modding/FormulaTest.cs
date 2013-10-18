using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Modding.Loaders;
using FrEee.Game.Objects.Technology;

namespace FrEee.Tests.Modding
{
	/// <summary>
	/// Tests data file formulas.
	/// </summary>
	[TestClass]
	public class FormulaTest
	{
		/// <summary>
		/// Tests static formulas.
		/// </summary>
		[TestMethod]
		public void StaticFormula()
		{
			var data =
@"Parameter Name := speed
Parameter Minimum := 3
Parameter Maximum := 5
Parameter Name := warhead
Parameter Maximum := 5
Name := ='Nuclear Missile ' + warhead.ToRomanNumeral() + ' S' + str(speed)";

			var metarec = new MetaRecord(data.Split('\n'));

			Assert.AreEqual(2, metarec.Parameters.Count());
			Assert.AreEqual(3, metarec.Parameters.First().Minimum);
			Assert.AreEqual(5, metarec.Parameters.First().Maximum);
			Assert.AreEqual(1, metarec.Parameters.Last().Minimum);
			Assert.AreEqual(5, metarec.Parameters.Last().Maximum);

			var recs = metarec.Instantiate();
			Assert.AreEqual(15, recs.Count());
			Assert.AreEqual(1, recs.Where(r => r.Get<string>("Name", null) == "Nuclear Missile III S4").Count());
		}

		/// <summary>
		/// Makes sure that meta records with no parameters still generate records.
		/// </summary>
		[TestMethod]
		public void NoFormula()
		{
			var data = "Name := Capital Ship Missile I";
			var metarec = new MetaRecord(data.Split('\n'));
			Assert.AreEqual(0, metarec.Parameters.Count());
			var recs = metarec.Instantiate();
			Assert.AreEqual(1, recs.Count());
			Assert.AreEqual<string>("Capital Ship Missile I", recs.First().Get<string>("Name", null));
		}

		/// <summary>
		/// Tests dynamic formulas.
		/// </summary>
		[TestMethod]
		public void DynamicFormula()
		{
			var mod = new Mod();
			var armor = new ComponentTemplate();
			armor.Size = 10;
			armor.Durability = new Formula<int>(armor, "self.Size * 3", FormulaType.Dynamic);
			Assert.AreEqual<int>(30, armor.Durability);
		}
	}
}
