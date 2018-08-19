namespace FrEee.Modding
{
    /// <summary>
    /// A requirement that a script return true.
    /// WARNING: script execution is rather slow; use sparingly!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ScriptRequirement<T> : Requirement<T>
    {
        #region Public Constructors

        public ScriptRequirement(Formula<bool> formula, Formula<string> description)
            : base(description)
        {
            Formula = formula;
        }

        #endregion Public Constructors

        #region Public Properties

        public Formula<bool> Formula { get; set; }

        public override bool IsStrict
        {
            get { return false; }
        }

        #endregion Public Properties

        #region Public Methods

        public override bool IsMetBy(T obj)
        {
            return Formula.Evaluate(obj);
        }

        #endregion Public Methods
    }
}
