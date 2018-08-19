using FrEee.Utility.Extensions;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace FrEee.Utility
{
    public interface IDataScalar : IData
    {
    }

    public static class DataScalar
    {
        #region Public Methods

        public static IDataScalar Create<T>(T o)
        {
            if (o == null)
                return null;
            var scalarType = typeof(DataScalar<>).MakeGenericType(o.GetType());
            var scalar = scalarType.Instantiate(o);
            return (IDataScalar)scalar;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// A scalar which can be converted easily to and from a string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataScalar<T> : IDataScalar
    {
        #region Public Constructors

        public DataScalar(T value = default(T))
        {
            Value = value;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Data
        {
            get
            {
                return Type.Name + ":" + Convert.ToString(Value, CultureInfo.InvariantCulture);
            }
            set
            {
                var data = value.Substring(value.IndexOf(":") + 1);
                if (typeof(T).IsEnum)
                    Value = (T)Enum.Parse(typeof(T), data);
                else
                    Value = (T)Convert.ChangeType(data, typeof(T), CultureInfo.InvariantCulture);
            }
        }

        [JsonIgnore]
        public T Value { get; set; }

        object IData.Value { get { return Value; } }

        #endregion Public Properties

        #region Private Properties

        private SafeType Type
        {
            get
            {
                return new SafeType(typeof(T));
            }
        }

        #endregion Private Properties
    }
}
