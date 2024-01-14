namespace FrEee.WinForms.Interfaces;

/// <summary>
/// Something which can be bound to data.
/// </summary>
public interface IBindable
{
	/// <summary>
	/// Rebinds the control to the current data, updating it to reflect changes in the data.
	/// </summary>
	void Bind();
}

/// <summary>
/// Something which can be bound to a specific type of data.
/// </summary>
public interface IBindable<in T> : IBindable
{
	/// <summary>
	/// Binds the control to new data.
	/// </summary>
	/// <param name="data"></param>
	void Bind(T data);
}