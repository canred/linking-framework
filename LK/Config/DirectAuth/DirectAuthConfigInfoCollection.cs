using System;
using System.Collections;

namespace LK.Config.DirectAuth
{
    [Serializable]
    public class DirectAuthConfigInfoCollection : CollectionBase
    {
        public DirectAuthConfigInfoCollection()
        {
        }
        public DirectAuthConfigInfoCollection(DirectAuthConfigInfoCollection value)
        {
            AddRange(value);
        }
        public DirectAuthConfigInfoCollection(DirectAuthConfigInfo[] value)
        {
            AddRange(value);
        }
        public DirectAuthConfigInfo this[int index]
        {
            get { return ((DirectAuthConfigInfo)(List[index])); }
        }
        public int Add(DirectAuthConfigInfo value)
        {
            return List.Add(value);
        }
        public void AddRange(DirectAuthConfigInfo[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                Add(value[i]);
            }
        }
        public void AddRange(DirectAuthConfigInfoCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add((DirectAuthConfigInfo)value.List[i]);
            }
        }
        public bool Contains(DirectAuthConfigInfo value)
        {
            return List.Contains(value);
        }
        public void CopyTo(DirectAuthConfigInfo[] array, int index)
        {
            List.CopyTo(array, index);
        }
        public int IndexOf(DirectAuthConfigInfo value)
        {
            return List.IndexOf(value);
        }
        public void Insert(int index, DirectAuthConfigInfo value)
        {
            List.Insert(index, value);
        }
        public void Remove(DirectAuthConfigInfo value)
        {
            List.Remove(value);
        }
        public new DirectAuthConfigInfoCollectionEnumerator GetEnumerator()
        {
            return new DirectAuthConfigInfoCollectionEnumerator(this);
        }
        #region Nested type: DirectAuthConfigInfoCollectionEnumerator
        public class DirectAuthConfigInfoCollectionEnumerator : IEnumerator
        {
            private readonly IEnumerator _enumerator;
            private readonly IEnumerable _temp;
            public DirectAuthConfigInfoCollectionEnumerator(DirectAuthConfigInfoCollection mappings)
            {
                _temp = ((mappings));
                _enumerator = _temp.GetEnumerator();
            }
            public DirectAuthConfigInfo Current
            {
                get { return ((DirectAuthConfigInfo)(_enumerator.Current)); }
            }
            #region IEnumerator Members
            object IEnumerator.Current
            {
                get { return _enumerator.Current; }
            }
            bool IEnumerator.MoveNext()
            {
                return _enumerator.MoveNext();
            }
            void IEnumerator.Reset()
            {
                _enumerator.Reset();
            }
            #endregion
            public bool MoveNext()
            {
                return _enumerator.MoveNext();
            }
            public void Reset()
            {
                _enumerator.Reset();
            }
        }
        #endregion
    }
}