using FrEee.Modding;
using FrEee.Modding.Abilities;
using FrEee.Serialization;

namespace FrEee.Objects.Space;

public interface IStellarObject : ISpaceObject, IReferrable, IModObject, IAbilityContainer
{
    /// <summary>
    /// A description of this stellar object.
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// Used for naming.
    /// </summary>
    int Index { get; set; }

    /// <summary>
    /// Used for naming.
    /// </summary>
    bool IsUnique { get; set; }

    /// <summary>
    /// The name of this stellar object.
    /// </summary>
    new string Name { get; set; }

    /// <summary>
    /// Name of the picture used to represent this stellar object, excluding the file extension.
    /// PNG files will be searched first, then BMP.
    /// </summary>
    string PictureName { get; set; }

    /// <summary>
    /// The stellar size of this object.
    /// </summary>
    StellarSize StellarSize { get; }
}