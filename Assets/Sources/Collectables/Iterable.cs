using System.Collections.Generic;
using Sources.Vehicle;

namespace Sources.Player.Factory
{
    public class Iterable<T>
    {
        private readonly List<T> _iterableObjects;

        protected int _currentIndex;

        public Iterable(List<T> iterableObjects, int currentIndex)
        {
            _iterableObjects = iterableObjects;
            _currentIndex = currentIndex;
        }

        public T Current => _iterableObjects[_currentIndex];

        public T Next()
        {
            _currentIndex++;
            ClampNext();
            return _iterableObjects[_currentIndex];
        }

        public T Previous()
        {
            _currentIndex--;
            ClampPrevious();
            return _iterableObjects[_currentIndex];
        }

        protected void ClampNext()
        {
            if (_currentIndex > _iterableObjects.Count - 1)
            {
                _currentIndex = _iterableObjects.Count - 1;
            }
        }

        protected void ClampPrevious()
        {
            if (_currentIndex < 0)
            {
                _currentIndex = 0;
            }
        }
    }
}