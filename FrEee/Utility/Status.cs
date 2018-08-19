using System;

namespace FrEee.Utility
{
    /// <summary>
    /// Status for an operation.
    /// </summary>
    public class Status
    {
        #region Private Fields

        private Exception exception;
        private string message;
        private double progress;

        #endregion Private Fields

        #region Public Delegates

        public delegate void ChangedDelegate();

        #endregion Public Delegates

        #region Public Events

        public event ChangedDelegate Changed;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Any exception that may have occurred.
        /// </summary>
        public Exception Exception
        {
            get { return exception; }
            set
            {
                exception = value;
                if (Changed != null)
                    Changed();
            }
        }

        /// <summary>
        /// Message indicating current sub-operation.
        /// </summary>
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                if (Changed != null)
                    Changed();
            }
        }

        /// <summary>
        /// Progress to completion.
        /// 0 = just started.
        /// 1 = done.
        /// </summary>
        public double Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                if (Changed != null)
                    Changed();
            }
        }

        #endregion Public Properties
    }
}
