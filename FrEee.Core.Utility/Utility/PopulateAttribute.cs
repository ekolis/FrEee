using System;
using FrEee.Utility;
using FrEee.Utility;

namespace FrEee.Utility;

public class PopulateAttribute<T>
	: Attribute
	where T : IPopulator
{
}
