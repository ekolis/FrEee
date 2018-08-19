using System;

namespace FrEee.Utility
{
    /// <summary>
    /// Provides a custom name to use that is the singular definitive name, not an alias.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class CanonicalNameAttribute : NameAttribute
    {
        #region Public Constructors

        public CanonicalNameAttribute(string name)
            : base(name)
        {
        }

        #endregion Public Constructors
    }

    /// <summary>
    /// Provides a custom name to use on a class, field, or property.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class NameAttribute : Attribute
    {
        #region Public Constructors

        public NameAttribute(string name)
        {
            Name = name;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The name to assign.
        /// </summary>
        public string Name { get; private set; }

        #endregion Public Properties
    }
}
