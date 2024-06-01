using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities.References;

namespace FrEee.Data.Entities;

/// <summary>
/// A general purpose game object.
/// </summary>
public abstract class GameObject<TEntity>(GameIdentifier<TEntity> id)
	: IGameObject<TEntity>
	where TEntity : IGameObject<TEntity>
{
	/// <summary>
	/// The ID of the game object.
	/// </summary>
	public virtual IIdentifier<long, TEntity> ID => id;

	/// <summary>
	/// General purpose serialized data to be sorted out in future iterations of the data model.
	/// </summary>
	public string SerializedData { get; set; } = "";
}