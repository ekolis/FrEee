namespace FrEee.Objects.LogMessages;

public interface IPictorialLogMessage<out T>
{
    T Context { get; }
}