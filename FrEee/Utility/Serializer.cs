using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace FrEee.Utility
{
    /// <summary>
    /// Serializes and deserializes objects.
    /// </summary>
    public static class Serializer
    {
        #region Private Fields

        private const bool EnableJsonSerializer = false;

        #endregion Private Fields

        #region Public Methods

        public static T Deserialize<T>(Stream str)
        {
            return (T)Deserialize(str);
        }

        public static object Deserialize(Stream str)
        {
            try
            {
                var sr = new StreamReader(str);
                var s = sr.ReadToEnd();
                return DeserializeFromString(s);
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine("Could not deserialize using JSON serializer. Attempting to use legacy serializer. Error dump follows:");
                Console.Error.WriteLine(ex);
                try
                {
                    return LegacySerializer.Deserialize<object>(str);
                }
                catch (SerializationException ex2)
                {
                    Console.Error.WriteLine("Could not deserialize using legacy serializer. Error dump follows:");
                    Console.Error.WriteLine(ex2);
                }
                throw new Exception("Unable to deserialize. Please check stderr.txt for details.");
            }
        }

        public static T DeserializeFromString<T>(string s)
        {
            return (T)DeserializeFromString(s);
        }

        public static object DeserializeFromString(string s)
        {
            if (EnableJsonSerializer)
            {
                var js = new JsonSerializer();
                return js.DeserializeFromString(s);
            }
            else
            {
                var sr = new StringReader(s);
                return LegacySerializer.Deserialize<object>(sr);
            }
        }

        public static void Serialize(object o, TextWriter w)
        {
            var s = SerializeToString(o);
            w.Write(s);
            w.Flush();
        }

        public static void Serialize(object o, Stream s)
        {
            var sw = new StreamWriter(s);
            Serialize(o, sw);
            sw.Flush();
        }

        public static string SerializeToString(object o)
        {
            if (EnableJsonSerializer)
            {
                var js = new JsonSerializer();
                return js.SerializeToString(o);
            }
            else
            {
                var sw = new StringWriter();
                LegacySerializer.Serialize(o, sw, typeof(object));
                return sw.ToString();
            }
        }

        #endregion Public Methods
    }

    /// <summary>
    /// Prevents a property from being serialized, or copied when the containing object is copied.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DoNotSerializeAttribute : DoNotCopyAttribute
    {
        #region Public Constructors

        public DoNotSerializeAttribute(bool allowSafeCopy = true)
            : base(allowSafeCopy)
        {
        }

        #endregion Public Constructors
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class SerializationPriorityAttribute : Attribute
    {
        #region Public Constructors

        public SerializationPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        #endregion Public Constructors

        #region Public Properties

        public int Priority { get; private set; }

        #endregion Public Properties
    }
}
