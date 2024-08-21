using System.Linq;
using FrEee.Modding;
using FrEee.Modding.Loaders;
using FrEee.Objects.GameState;
using FrEee.Processes.Combat;
using NUnit.Framework;

namespace FrEee.Objects.Technology;

/// <summary>
/// Tests weapon info.
/// </summary>
public class WeaponInfoTest
{
	private static Game gal = new Game();

	private static Mod mod;

	[OneTimeSetUp]
	public static void ClassInit()
	{
		mod = new ModLoader().Load("WeaponInfoTest");
	}

	/// <summary>
	/// Tests formula damage values.
	/// </summary>
	[Test]
	public void FormulaDamage()
	{
		var ct = mod.ComponentTemplates.Single(x => x.Name == "Formula Weapon");
		var comp = ct.Instantiate();
		Assert.AreEqual(3, ct.WeaponInfo.MinRange.Value);
		Assert.AreEqual(5, ct.WeaponInfo.MaxRange.Value);
		Assert.AreEqual(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
		Assert.AreEqual(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
		Assert.AreEqual(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
	}

	/// <summary>
	/// Tests non-formula damage values.
	/// </summary>
	[Test]
	public void NonFormulaDamage()
	{
		var ct = mod.ComponentTemplates.Single(x => x.Name == "Non-Formula Weapon");
		var comp = ct.Instantiate();
		Assert.AreEqual(3, ct.WeaponInfo.MinRange.Value);
		Assert.AreEqual(5, ct.WeaponInfo.MaxRange.Value);
		Assert.AreEqual(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 2)));
		Assert.AreEqual(20, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 4)));
		Assert.AreEqual(0, ct.WeaponInfo.GetDamage(new Shot(null, comp, null, 6)));
	}
}
