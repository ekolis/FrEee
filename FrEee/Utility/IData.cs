namespace FrEee.Utility
{
    /// <summary>
    /// Either a data object, or a scalar.
    /// </summary>
    public interface IData
    {
        #region Public Properties

        /// <summary>
        /// The data value, as a string which can be used to find the object or parse into the scalar.
        /// </summary>
        string Data { get; set; }

        [JsonIgnore]
        object Value { get; }

        #endregion Public Properties
    }
}
