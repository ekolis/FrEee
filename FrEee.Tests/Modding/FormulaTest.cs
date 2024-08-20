using FrEee.Objects.Civilization;
using FrEee.Processes.Combat;
using FrEee.Objects.Technology;
using FrEee.Objects.Vehicles;
using FrEee.Modding.Templates;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Modding.Loaders;

namespace FrEee.Modding;

/// <summary>
/// Tests data file formulas.
/// </summary>
public class FormulaTest
{
	/// <summary>
	/// Tests dynamic formulas.
	/// </summary>
	[Test]
	public void DynamicFormula()
	{
		var gal = new Galaxy();
		Empire emp = new Empire();
		Mod.Current = new Mod();
		var armor = new ComponentTemplate();
		armor.Name = armor.ModID = "Armor";
		armor.Size = 10;
		armor.Durability = new ComputedFormula<int>("self.Size * 3", armor, true);
		Mod.Current.ComponentTemplates.Add(armor);
		var mount = new Mount();
		mount.ModID = mount.Name = "Scale Mount";
		mount.DurabilityPercent = 200;
		mount.SizePercent = new ComputedFormula<int>("design.Hull.Size", mount, true);
		Mod.Current.Mounts.Add(mount);
		var hull = new Hull<Ship>();
		hull.ModID = hull.Name = "Generic Hull";
		hull.Size = 150;
		Mod.Current.Hulls.Add(hull);

		var design = new Design<Ship>();
		Galaxy.Current.AssignID(design);
		var mct = new MountedComponentTemplate(design, armor, mount);
		design.Hull = hull;
		design.Components.Add(mct);
		mct.Container = design;
		design.Owner = emp;
		Assert.AreEqual(30, armor.Durability.Value); // 10 * 3
		Assert.AreEqual(mct.Durability, 60); // 30 * 200%
		Assert.AreEqual(15, mct.Size); // 10 * 150%
	}

	/// <summary>
	/// Tests dynamic formulas.
	/// </summary>
	[Test]
	public void DynamicFormulaWithParameters()
	{
		var gal = new Galaxy();
		Empire emp = new Empire();
		Mod.Current = new ModLoader().Load("DynamicFormulaWithParameters");
		var ct = Mod.Current.ComponentTemplates.First();
		Assert.AreEqual(1, ct.WeaponInfo.GetDamage(new Shot(null, new Component(null, new MountedComponentTemplate(null, ct, null)), null, 1)));
	}

	/// <summary>
	/// Makes sure that meta records with no parameters still generate records.
	/// </summary>
	[Test]
	public void NoFormula()
	{
		var data = "Name := Capital Ship Missile I";
		var metarec = new MetaRecord(data.Split('\n'));
		Assert.AreEqual(0, metarec.Parameters.Count());
		var recs = metarec.Instantiate();
		Assert.AreEqual(1, recs.Count());
		Assert.AreEqual("Capital Ship Missile I", recs.First().Get<string>("Name", null).Value);
	}

	/// <summary>
	/// Tests static formulas.
	/// </summary>
	[Test]
	public void StaticFormula()
	{
		var data =
@"Parameter Name := speed
Parameter Minimum := 3
Parameter Maximum := 5
Parameter Name := warhead
Parameter Maximum := 5
Name := ='Nuclear Missile ' + warhead.ToRomanNumeral() + ' S' + speed.ToString()";

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
	/// Tests string interpolation dynamic formulas.
	/// </summary>
	[Test]
	public void StringInterpolationDynamic()
	{
		var field = new Field("Test := Test Case {{42 + 69}}");
		var formula = field.CreateFormula<string>(null);
		Assert.AreEqual($"Test Case {42 + 69}", formula.Value);
		Assert.IsTrue(formula.IsDynamic);
		Assert.IsFalse(formula.IsLiteral);
	}

	/// <summary>
	/// Tests string interpolation mixed formulas.
	/// Mixed formulas should be treated as dynamic.
	/// </summary>
	[Test]
	public void StringInterpolationMixed()
	{
		var field = new Field("Test := Test Case {42} {{69}}");
		var formula = field.CreateFormula<string>(null);
		Assert.AreEqual($"Test Case {42} {69}", formula.Value);
		Assert.IsTrue(formula.IsDynamic);
		Assert.IsFalse(formula.IsLiteral);
	}

	/// <summary>
	/// Tests string interpolation static formulas.
	/// </summary>
	[Test]
	public void StringInterpolationStatic()
	{
		var field = new Field("Test := Test Case {42 + 69}");
		var formula = field.CreateFormula<string>(null);
		Assert.AreEqual($"Test Case {42 + 69}", formula.Value);
		Assert.IsFalse(formula.IsDynamic);
		Assert.IsFalse(formula.IsLiteral);
	}

	/// <summary>
	/// Tests SE4/SE5 style replacement strings.
	/// </summary>
	[Test]
	public void ReplacementStrings()
	{
		var field = new Field("Test := [%Amount1] [%Amount2%]");
		var ctx = new Dictionary<string, object>()
		{
			{"Amount1", 42 },
			{"Amount2", 69 },
		};
		var formula = field.CreateFormula<string>(ctx);
		Assert.AreEqual("42 69", formula.Evaluate(ctx));
		Assert.IsTrue(formula.IsDynamic);
		Assert.IsFalse(formula.IsLiteral);
	}
}
