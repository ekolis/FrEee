using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility;

public class PopulateAttribute<T>
	: Attribute
	where T : IPopulator
{
}
