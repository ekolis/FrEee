﻿using FrEee.Game.Interfaces;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding
{
    /// <summary>
    /// A lightweight reference to some object in the current mod.
    /// Can be passed around on the network as a surrogate for said object.
    /// This class should be used when referencing a mod object from the actual game.
    /// It is not necessary to use ModReference when referring to mod objects from other mod objects.
    /// Only mod objects can be referenced via mod references.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ModReference<T> : IReference<string, T> where T : IModObject
    {
        #region Private Fields

        private ClientSideCache<T> cache;

        #endregion Private Fields

        #region Public Constructors

        public ModReference()
        {
            cache = new ClientSideCache<T>(() =>
            {
                if (string.IsNullOrWhiteSpace(ID))
                    return default(T);
                var obj = (T)Mod.Current.Find(ID);
                if (obj == null)
                    return default(T);
                if (obj.IsDisposed)
                    return default(T);
                return obj;
            })
            { IsServerSideCacheEnabled = true };
        }

        public ModReference(T t)
            : this()
        {
            var mobj = (IModObject)t;
            if (Mod.Current == null)
                throw new ReferenceException<int, T>("Can't create a reference to an IModObject without a mod.");
            else if (t == null)
                ID = ""; // dictionaries don't like null keys
            else if (mobj.ModID != null)
                ID = mobj.ModID;
            else
                throw new ReferenceException<string, T>("Can't create a reference to {0} because it has no ID in the current mod.".F(mobj));
            if (!HasValue)
                throw new ArgumentException("{0} does not exist in the current mod so it cannot be referenced.".F(t));
        }

        public ModReference(string id)
            : this()
        {
            if (Mod.Current == null)
                throw new ReferenceException<int, T>("Can't create a reference to an IModObject without a mod.");
            else if (Mod.Current.Find(id) is T)
                ID = id;
            else
                throw new Exception("Object with ID " + id + " is not a " + typeof(T) + ".");
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Does the reference have a valid value?
        /// </summary>
        public bool HasValue
        {
            get
            {
                // blank ID's are null objects, which are perfectly cromulent
                return string.IsNullOrWhiteSpace(ID) || Value != null;
            }
        }

        public string ID { get; private set; }

        /// <summary>
        /// Resolves the reference.
        /// </summary>
        /// <returns></returns>
        public T Value
        {
            get
            {
                var val = cache.Value;
                if (val == null)
                    return default(T);
                return val;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public static implicit operator ModReference<T>(T t)
        {
            if (t == null)
                return null;
            if (t.ModID == null)
            {
                // HACK - why are my items not registered in the mod? does it have something to do with formulas and leveled templates?
                Mod.Current.AssignID(t, Mod.Current.Objects.Select(q => q.ModID).ToList());
                Mod.Current.Register(t);
            }
            return new ModReference<T>(t);
        }

        public static implicit operator T(ModReference<T> r)
        {
            if (r == null)
                return default(T);
            return r.Value;
        }

        public override bool Equals(object obj)
        {
            var mref = obj as ModReference<T>;
            if (mref == null)
                return false;
            return ID == mref.ID;
        }

        public override int GetHashCode()
        {
            return ID == null || ID == "" ? 0 : ID.GetHashCode();
        }

        public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            if (!done.Contains(this))
            {
                done.Add(this);
                if (HasValue && Value is IPromotable)
                    ((IPromotable)Value).ReplaceClientIDs(idmap, done);
            }
        }

        public override string ToString()
        {
            return "Mod ID=" + ID + ", Value=" + Value;
        }

        #endregion Public Methods
    }
}
