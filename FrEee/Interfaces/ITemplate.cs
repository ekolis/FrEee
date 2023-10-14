namespace FrEee.Interfaces
{
	/// <summary>
	/// A template for instantiating in-game objects.
	/// </summary>
	/// <typeparam name="T">The type of object which can be instantiated.</typeparam>
	public interface ITemplate<out T>
	{
		/// <summary>
		/// Instantiates an object.
		/// </summary>
		/// <param name="game">The game, for context.</param>
		/// <returns>The new object.</returns>
		T Instantiate(Game game);
	}
}