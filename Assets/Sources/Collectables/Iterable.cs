using System.Collections;
using System.Collections.Generic;

namespace Sources.Collectables
{
    public class Iterable<T> : IEnumerable
    {
        private readonly List<T> _iterableObjects;

        protected int Index;

        public Iterable(List<T> iterableObjects, int index)
        {
            _iterableObjects = iterableObjects;
            Index = index;
        }

        public T Current => _iterableObjects[Index];
        public int CurrentIndex => Index;
        public IEnumerator GetEnumerator() => _iterableObjects.GetEnumerator();

        public T Next()
        {
            Index++;
            ClampNext();
            return _iterableObjects[Index];
        }

        public T Previous()
        {
            Index--;
            ClampPrevious();
            return _iterableObjects[Index];
        }

        public bool Reset(T iterable)
        {
            if (_iterableObjects.Contains(iterable) == false)
            {
                return false;
            }

            Index = _iterableObjects.IndexOf(iterable);
            return true;
        }

        protected virtual void ClampNext()
        {
            if (Index > _iterableObjects.Count - 1)
            {
                Index = _iterableObjects.Count - 1;
            }
        }

        protected virtual void ClampPrevious()
        {
            if (Index < 0)
            {
                Index = 0;
            }
        }
    }
}