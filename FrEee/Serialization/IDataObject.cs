using FrEee.Utility;
namespace FrEee.Serialization;

/// <summary>
/// Used for abstract classes that want to define serialization data without requiring inherited classes use said data.
/// </summary>
public interface IAbstractDataObject
{
	/// <summary>
	/// When retreived, pulls in any data needed to reconstitute this object.
	/// When set, reconstitutes the object from the data being assigned.
	/// </summary>
	[DoNotCopy(false)]
	SafeDictionary<string, object> Data { get; set; }
}

/// <summary>
/// Something which can get data about itself and reconstitute itself from said data.
/// It is probably best to implement this interface only on classes and not on interfaces,
/// so as to avoid forcing class authors to implement this interface if they don't want to.
/// As for abstract classes, you can use IAbstractDataObject to define base class data without
/// requiring the child class define its own data.
/// </summary>
public interface IDataObject : IAbstractDataObject
{
}
