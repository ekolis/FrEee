namespace FrEee.Interfaces
{
	/// <summary>
	/// For classes that need extra processing after being copied or whatnot.
	/// </summary>
	public interface ICleanable
	{
		void Clean();
	}
}