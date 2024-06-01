using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities;
using FrEee.Data.Entities.References;

namespace FrEee.Data.Entities.References
{
	/// <summary>
	/// An identifier for an <see cref="IModObject"/>.
	/// </summary>
	public record ModID(string ID)
		: Identifier<string>(ID)
	{
	}

	/// <summary>
	/// An identifier for an <see cref="IModObject"/>.
	/// </summary>
	public record ModIdentifier<TEntity>(string ID)
		: ModID(ID), IIdentifier<string, TEntity>
		where TEntity : IModObject, IEntity<string>
	{
	}
}
