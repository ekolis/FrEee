namespace FrEee.Game.Interfaces
{
	public interface IPictorialLogMessage<out T> where T : IPictorial
	{
		T Context { get; }
	}
}