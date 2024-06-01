using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities.References;

namespace FrEee.Data.Entities;

/// <summary>
/// An object which can be loaded from a mod data file.
/// </summary>
public abstract class ModObject<TEntity>(ModIdentifier<TEntity> id)
	: IModObject<TEntity>
	where TEntity : IModObject<TEntity>
{
	/// <summary>
	/// The ID of the mod object.
	/// </summary>
	public virtual IIdentifier<string, TEntity> ID => id;

	/// <summary>
	/// General purpose serialized data to be sorted out in future iterations of the data model.
	/// </summary>
	public string SerializedData { get; set; } = "";
}