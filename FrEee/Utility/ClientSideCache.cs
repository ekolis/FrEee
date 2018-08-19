using FrEee.Game.Objects.Civilization;
using System;

namespace FrEee.Utility
{
    /// <summary>
    /// A client side cache for data.
    /// Can also cache data server side when a flag is enabled.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClientSideCache<T>
    {
        #region Private Fields

        private Func<T> compute;

        private bool isServerSideCacheEnabled;

        private T value;

        #endregion Private Fields

        #region Public Constructors

        public ClientSideCache(Func<T> compute)
        {
            this.compute = compute;
            IsDirty = true;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsCacheEnabled
        {
            get
            {
                return IsServerSideCacheEnabled || Empire.Current != null;
            }
        }

        public bool IsDirty { get; private set; }

        public bool IsServerSideCacheEnabled
        {
            get
            {
                return isServerSideCacheEnabled;
            }
            set
            {
                isServerSideCacheEnabled = value;
                if (!isServerSideCacheEnabled)
                    IsDirty = true;
            }
        }

        public T Value
        {
            get
            {
                if (IsCacheEnabled)
                {
                    if (!IsDirty)
                        return value;
                    else
                    {
                        value = compute();
                        IsDirty = false;
                        return value;
                    }
                }
                else
                    return compute();
            }
        }

        #endregion Public Properties

        #region Public Methods

        public static implicit operator T(ClientSideCache<T> cache)
        {
            return cache.Value;
        }

        #endregion Public Methods
    }
}
