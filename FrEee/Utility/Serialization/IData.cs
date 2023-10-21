using Newtonsoft.Json;

namespace FrEee.Utility.Serialization
{
	/// <summary>
	/// Either a data object, or a scalar.
	/// </summary>
	public interface IData
	{
		/// <summary>
		/// The data value, as a string which can be used to find the object or parse into the scalar.
		/// </summary>
		string Data { get; set; }

		[JsonIgnore]
		object Value { get; }
	}
}