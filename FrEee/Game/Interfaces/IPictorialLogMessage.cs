namespace FrEee.Game.Interfaces
{
	public interface IPictorialLogMessage<out T>
	{
		T Context { get; }
	}
}