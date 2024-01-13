namespace FrEee.Interfaces
{
	public interface IPictorialLogMessage<out T>
	{
		T Context { get; }
	}
}