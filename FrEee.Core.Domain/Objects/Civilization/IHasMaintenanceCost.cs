using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Utility;

namespace FrEee.Objects.Civilization;

/// <summary>
/// Something that has a per-turn maintenance cost.
/// </summary>
public interface IHasMaintenanceCost
{
	/// <summary>
	/// Cost to maintain this object per turn.
	/// </summary>
	ResourceQuantity MaintenanceCost { get; }
}
