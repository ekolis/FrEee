using System;

namespace FrEee.Utility;

public class PopulateAttribute<T>
	: Attribute
	where T : IPopulator
{
}
