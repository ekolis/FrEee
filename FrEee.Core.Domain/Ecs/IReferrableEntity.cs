using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.GameState;

namespace FrEee.Ecs
{
	/// <summary>
	/// A referrable entity which can have abilities.
	/// </summary>
	[Obsolete("Just use IReferrable or IEntity as needed.")]
	public interface IReferrableEntity : IReferrable, IEntity { }
}
