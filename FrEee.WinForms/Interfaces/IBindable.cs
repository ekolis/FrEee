namespace FrEee.WinForms.Interfaces
{
    /// <summary>
    /// Something which can be bound to data.
    /// </summary>
    public interface IBindable
    {
        #region Public Methods

        /// <summary>
        /// Rebinds the control to the current data, updating it to reflect changes in the data.
        /// </summary>
        void Bind();

        #endregion Public Methods
    }

    /// <summary>
    /// Something which can be bound to a specific type of data.
    /// </summary>
    public interface IBindable<in T> : IBindable
    {
        #region Public Methods

        /// <summary>
        /// Binds the control to new data.
        /// </summary>
        /// <param name="data"></param>
        void Bind(T data);

        #endregion Public Methods
    }
}
