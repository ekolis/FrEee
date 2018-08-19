﻿using FrEee.Utility.Extensions;
using Newtonsoft.Json;
using System;
using static FrEee.Utility.Extensions.CommonExtensions;

namespace FrEee.Utility
{
    public interface ISimpleDataObject : IDataObject
    {
        #region Public Properties

        ObjectGraphContext Context { get; set; }
        int ID { get; }

        SafeDictionary<string, IData> SimpleData
        {
            get; set;
        }

        object Value { get; }

        #endregion Public Properties

        #region Public Methods

        void InitializeData(ObjectGraphContext ctx = null);

        void InitializeValue(ObjectGraphContext ctx = null);

        #endregion Public Methods
    }

    /// <summary>
    /// A data object which breaks objects down into scalars and references.
    /// </summary>
    public class SimpleDataObject : MarshalByRefObject, ISimpleDataObject
    {
        #region Private Fields

        private object value;

        #endregion Private Fields

        #region Public Constructors

        public SimpleDataObject()
        {
            Context = new ObjectGraphContext();
            Type = typeof(object);
        }

        public SimpleDataObject(object o, ObjectGraphContext ctx = null)
        {
            Context = ctx ?? new ObjectGraphContext();
            if (o != null)
            {
                Type = o.GetType();
                if (Context.GetID(o) == null)
                    ID = Context.Add(o);
            }
            else
            {
                Type = null;
                Data = new SafeDictionary<string, object>();
                ID = -1;
            }
        }

        public SimpleDataObject(SafeDictionary<string, IData> simpleData, ObjectGraphContext ctx = null)
        {
            SimpleData = simpleData;
            Context = ctx ?? new ObjectGraphContext();
        }

        #endregion Public Constructors

        #region Public Properties

        [JsonIgnore]
        public ObjectGraphContext Context { get; set; }

        [JsonIgnore]
        public SafeDictionary<string, object> Data
        {
            get
            {
                var dict = new SafeDictionary<string, object>();
                foreach (var pname in SimpleData.Keys.ExceptSingle("!type").ExceptSingle("!id"))
                    dict[pname] = SimpleData[pname]?.Value;
                return dict;
            }
            set
            {
                var id = ID; // save it off so we don't forget
                SimpleData = new SafeDictionary<string, IData>();
                ID = id; // restore it
                SimpleData["!type"] = new DataScalar<string>(new SafeType(Type).Name);
                if (Context == null)
                    Context = new ObjectGraphContext();
                foreach (var pname in value.Keys)
                {
                    var pval = value[pname];
                    if (pval == null)
                        SimpleData[pname] = null;
                    else if (pval.GetType().IsScalar())
                        SimpleData[pname] = DataScalar.Create(pval);
                    else
                        SimpleData[pname] = (IData)typeof(DataReference<>).MakeGenericType(pval.GetType()).Instantiate(Context, pval);
                }
            }
        }

        [JsonIgnore]
        public int ID
        {
            get
            {
                if (SimpleData == null)
                    return 0;
                return (int?)SimpleData["!id"]?.Value ?? 0;
            }
            private set
            {
                if (SimpleData == null)
                    SimpleData = new SafeDictionary<string, IData>();
                SimpleData["!id"] = new DataScalar<int>(value);
            }
        }

        public SafeDictionary<string, IData> SimpleData
        {
            get; set;
        }

        public SafeType Type { get; private set; }

        [JsonIgnore]
        public object Value
        {
            get
            {
                if (value == null)
                {
                    var kos = Context.KnownObjects[Type];
                    if (kos != null && kos.Count > ID)
                        value = kos[ID];
                    else
                    {
                        var t = Type.Type.Instantiate();
                        Context.Add(t);
                        value = t;
                    }
                }
                return value;
            }
            set
            {
                Data = value.GetData(Context);
            }
        }

        object ISimpleDataObject.Value
        {
            get { return Value; }
        }

        #endregion Public Properties

        #region Public Methods

        public static ISimpleDataObject Create(object o, ObjectGraphContext ctx = null)
        {
            if (o == null)
                return null;
            var t = o.GetType();
            return new SimpleDataObject(o, ctx);
        }

        public static ISimpleDataObject Load(SafeDictionary<string, IData> simpleData, ObjectGraphContext ctx = null)
        {
            if (simpleData == null)
                return null;
            var t = new SafeType(simpleData["!type"].Value as string).Type;
            return new SimpleDataObject(simpleData, ctx);
        }

        public void InitializeData(ObjectGraphContext ctx = null)
        {
            Data = Value.GetData(ctx ?? Context);
        }

        public void InitializeValue(ObjectGraphContext ctx = null)
        {
            if (Context.GetID(Value) == null)
                Context.Add(Value);
            Value.SetData(Data, ctx ?? Context);
        }

        #endregion Public Methods
    }
}
