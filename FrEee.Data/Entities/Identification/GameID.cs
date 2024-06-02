using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data.Entities;
using FrEee.Data.Entities.Identification;

namespace FrEee.Data.Entities.Identification
{
    /// <summary>
    /// An identifier for an <see cref="IGameObject"/>.
    /// </summary>
    public record GameID(long ID)
        : Identifier<long>(ID)
    {
    }

    /// <summary>
    /// An identifier for an <see cref="IGameObject"/>.
    /// </summary>
    public record GameIdentifier<TEntity>(long ID)
        : GameID(ID), IIdentifier<long, TEntity>
        where TEntity : IGameObject, IEntity<long>
    {
    }
}
