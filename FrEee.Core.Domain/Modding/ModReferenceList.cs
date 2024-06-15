using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Serialization;
using FrEee.Objects.GameState;

namespace FrEee.Modding
{
	public class ModReferenceList<T>
		: ReferenceList<ModReference<T>, T>
		where T : IModObject
	{
	}
}
