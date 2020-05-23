namespace FrEee.Modding
{
	/// <summary>
	/// A type of script.
	/// </summary>
	public interface IScript
	{
		/// <summary>
		/// The name of this script module. This should be a valid Python module name.
		/// </summary>
		string ModuleName { get; set; }

		/// <summary>
		/// The script text.
		/// </summary>
		string Text { get; set; }
	}
}
