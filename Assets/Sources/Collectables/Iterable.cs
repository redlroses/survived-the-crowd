using System.Collections;
using System.Collections.Generic;

namespace Sources.Collectables
{
    public class Iterable<T> : IEnumerable
    {
        private readonly List<T> _iterableObjects;

        private int _index;

        public Iterable(List<T> iterableObjects, int index)
        {
            _iterableObjects = iterableObjects;
            _index = index;
        }

        public T Current => _iterableObjects[_index];

        public IEnumerator GetEnumerator() => _iterableObjects.GetEnumerator();

        public T Next()
        {
            _index++;
            ClampNext();

            return _iterableObjects[_index];
        }

        public T Previous()
        {
            _index--;
            ClampPrevious();

            return _iterableObjects[_index];
        }

        public bool Reset(T iterable)
        {
            if (_iterableObjects.Contains(iterable) == false)
            {
                return false;
            }

            _index = _iterableObjects.IndexOf(iterable);

            return true;
        }

        protected virtual void ClampNext()
        {
            if (_index > _iterableObjects.Count - 1)
            {
                _index = _iterableObjects.Count - 1;
            }
        }

        protected virtual void ClampPrevious()
        {
            if (_index < 0)
            {
                _index = 0;
            }
        }
    }
}