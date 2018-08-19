using System;

namespace FrEee.Game.Objects.LogMessages
{
    /// <summary>
    /// A generic text log message with no picture.
    /// </summary>
    [Serializable]
    public class GenericLogMessage : LogMessage
    {
        #region Public Constructors

        public GenericLogMessage(string text)
            : base(text)
        {
        }

        public GenericLogMessage(string text, int turn) : base(text, turn)
        { }

        #endregion Public Constructors

        #region Public Properties

        public override System.Drawing.Image Picture
        {
            get { return null; }
        }

        #endregion Public Properties
    }
}
