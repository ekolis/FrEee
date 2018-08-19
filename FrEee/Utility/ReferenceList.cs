using FrEee.Game.Interfaces;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Utility
{
    public class GalaxyReferenceList<T> : ReferenceList<GalaxyReference<T>, T>
    {
    }

    public class ModReferenceList<T> : ReferenceList<ModReference<T>, T>
        where T : IModObject
    {
    }

    public class ReferenceList<TRef, T> : IList<T>, IReferenceEnumerable, IPromotable
                where TRef : IReference<T>
    {
        #region Public Constructors

        public ReferenceList()
        {
            list = new List<TRef>();
        }

        #endregion Public Constructors

        #region Public Properties

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        #endregion Public Properties

        #region Private Properties

        private IList<TRef> list { get; set; }

        #endregion Private Properties

        #region Public Indexers

        public T this[int index]
        {
            get
            {
                return list[index].Value;
            }
            set
            {
                list[index] = MakeReference(value);
            }
        }

        #endregion Public Indexers

        #region Public Methods

        public void Add(T item)
        {
            list.Add(MakeReference(item));
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Any(r => r.Value.Equals(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.Select(x => x.Value).ToList().CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.Select(x => x.Value).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            if (!list.Any(r => r.Value.Equals(item)))
                return -1;
            return list.Select((x, i) => new { Item = x, Index = i }).First(x => x.Item.Value.Equals(item)).Index;
        }

        public void Insert(int index, T item)
        {
            list.Insert(index, MakeReference(item));
        }

        public bool Remove(T item)
        {
            var i = IndexOf(item);
            if (i >= 0)
            {
                list.RemoveAt(i);
                return true;
            }
            else
                return false;
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            if (!done.Contains(this))
            {
                done.Add(this);
                foreach (var r in list)
                {
                    if (r is IPromotable)
                        (r as IPromotable).ReplaceClientIDs(idmap, done);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private static TRef MakeReference(T item)
        {
            return (TRef)typeof(TRef).Instantiate(item);
        }

        #endregion Private Methods
    }
}
