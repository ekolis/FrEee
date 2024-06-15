using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Serialization;

namespace FrEee.Objects.GameState
{
    public class GalaxyReferenceList<T>
		: ReferenceList<GalaxyReference<T>, T>
		where T : IReferrable
	{
	}
}