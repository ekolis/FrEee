using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;

namespace FrEee.Ecs;

/// <summary>
/// Something which can have abilities and be owned.
/// </summary>
[Obsolete("Just use IOwnable or IEntity as needed.")]
public interface IOwnableEntity : IOwnable, IEntity
{
}