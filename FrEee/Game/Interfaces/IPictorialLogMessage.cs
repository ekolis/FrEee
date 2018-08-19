namespace FrEee.Game.Interfaces
{
    public interface IPictorialLogMessage<out T> where T : IPictorial
    {
        #region Public Properties

        T Context { get; }

        #endregion Public Properties
    }
}
