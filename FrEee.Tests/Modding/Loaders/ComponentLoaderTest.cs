﻿using FrEee.Extensions;
using NUnit.Framework;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Summary description for ComponentLoaderTest
/// </summary>
public class ComponentLoaderTest
{
	[OneTimeSetUp]
	public static void ClassInit()
	{
		new ModLoader().Load("ComponentLoaderTest", includeGuiPlugins: false);
	}

	/// <summary>
	/// Makes sure standard weapons have sane values for their minimum/maximum range.
	/// </summary>
	[Test]
	public void WeaponRange()
	{
		var w = Mod.Current.ComponentTemplates.FindByName("Range 2 Weapon");
		Assert.AreEqual(0, w.WeaponInfo.MinRange.Value);
		Assert.AreEqual(2, w.WeaponInfo.MaxRange.Value);
	}

	/// <summary>
	/// Makes sure weapons with blind spots have sane values for their minimum/maximum range.
	/// </summary>
	[Test]
	public void WeaponRangeBlindSpot()
	{
		var w = Mod.Current.ComponentTemplates.FindByName("Blind Spot Weapon");
		Assert.AreEqual(2, w.WeaponInfo.MinRange.Value);
		Assert.AreEqual(3, w.WeaponInfo.MaxRange.Value);
	}
}
