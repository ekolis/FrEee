using FrEee.Data.Entities;
using FrEee.Objects.GameState;
using System.Collections.Generic;

namespace FrEee.Modding;

/// <summary>
/// An object which can be stored in mod files.
/// </summary>
public interface IModObject : INamed
{
    bool IsDisposed { get; }

    /// <summary>
    /// An ID used to represent this mod object.
    /// </summary>
    string ModID { get; set; }

    /// <summary>
    /// Parameters from the mod meta templates.
    /// </summary>
    IDictionary<string, object> TemplateParameters { get; set; }
}