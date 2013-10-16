using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrEee.Modding;

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
			int index = 0;
			Assert.AreEqual(1, recs.Where(r => r.GetString("Name", ref index) == "Nuclear Missile III S4").Count());
		}
	}
}
