using FrEee.Objects.Civilization;
using FrEee.Utility;
using FrEee.Serialization;
using FrEee.Extensions;
using System;
using FrEee.Objects.GameState;
using FrEee.Serialization;
using FrEee.Utility;
using FrEee.Ecs.Abilities.Utility;

namespace FrEee.Objects.Space;

/// <summary>
/// A star. Normally found at the center of a star system.
/// </summary>
[Serializable]
public class Star : StellarObject, ITemplate<Star>, IDataObject
{
	// TODO - do stars in SE4 have a size property?

	public override AbilityTargets AbilityTarget
	{
		get { return AbilityTargets.Star; }
	}

	/// <summary>
	/// The age of this star. (For flavor)
	/// </summary>
	public string Age { get; set; }

	/// <summary>
	/// The brightness of this star. (For flavor)
	/// </summary>
	public string Brightness { get; set; }

	public override bool CanBeObscured => false;

	/// <summary>
	/// The color of this star. (For flavor)
	/// </summary>
	public string Color { get; set; }

	public override SafeDictionary<string, object> Data
	{
		get
		{
			var dict = base.Data;
			dict[nameof(Brightness)] = Brightness;
			dict[nameof(Color)] = Color;
			dict[nameof(Age)] = Age;
			dict[nameof(IsDestroyed)] = IsDestroyed;
			return dict;
		}
		set
		{
			base.Data = value;
			Brightness = value[nameof(Brightness)].Default<string>();
			Color = value[nameof(Color)].Default<string>();
			Age = value[nameof(Age)].Default<string>();
			IsDestroyed = value[nameof(IsDestroyed)].Default<bool>();
		}
	}

	/// <summary>
	/// Is this a destroyed star?
	/// TODO - make sure destroyed stars don't provide supplies or resources from solar generation
	/// </summary>
	public bool IsDestroyed { get; set; }

	public Empire Owner
	{
		get
		{
			return null;
		}
	}

	/// <summary>
	/// Detonates this star. All space vehicles in the system will be destroyed and all planets will be converted to asteroid fields.
	/// </summary>
	public void Detonate()
	{
		foreach (var p in StarSystem.FindSpaceObjects<Planet>())
			p.ConvertToAsteroidField();
		foreach (var v in StarSystem.FindSpaceObjects<IMobileSpaceObject>())
			v.Dispose();
		Dispose();
	}

	/// <summary>
	/// Just copy the star's data.
	/// </summary>
	/// <returns>A copy of the star.</returns>
	public Star Instantiate()
	{
		var result = this.CopyAndAssignNewID();
		result.ModID = null;
		return result;
	}
}