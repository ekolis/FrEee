using FrEee.Utility;

namespace FrEee.Objects.GameState
{
	public class GameReferenceList<T>
		: ReferenceList<GameReference<T>, T>
		where T : IReferrable
	{
	}
}