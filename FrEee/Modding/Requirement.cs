namespace FrEee.Modding
{
    /// <summary>
    /// A requirement for some condition.
    /// </summary>
    /// <typeparam name="T">The object which needs to meet the requirement.</typeparam>
    public abstract class Requirement<T>
    {
        #region Public Constructors

        public Requirement(Formula<string> description)
        {
            Description = description;
        }

        #endregion Public Constructors

        #region Public Properties

        public Formula<string> Description { get; set; }

        /// <summary>
        /// Is this a "strict" requirement?
        /// Non-strict requirements are not counted as prerequisites for purposes of finding the "root" items of the tech tree.
        /// </summary>
        public abstract bool IsStrict { get; }

        #endregion Public Properties

        #region Public Methods

        public abstract bool IsMetBy(T obj);

        #endregion Public Methods
    }
}
