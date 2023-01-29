using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace Sources.Pool
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component, IPoolable<T>
    {
        private const string NoCopies = "No copies or some copies is null";

        private readonly List<T> _objectsPool;

        [Header("Core Settings")]
        [SerializeField] private int _size = 8;
        [SerializeField] private int _expansionAmount = 4;
        [SerializeField] private Transform _customContainer;
        [SerializeField] private bool _isStaticContainer;
        [SerializeField] private bool _isManualFill;
        [SerializeField] private List<T> _copies;

        public Transform Container => _customContainer;

        protected ObjectPool()
        {
            _objectsPool = new List<T>(_size);
        }

        protected virtual void Awake()
        {
            if (_customContainer == null)
            {
                CreateContainer();
            }

            ValidateCopies();

            if (_isManualFill)
            {
                return;
            }

            Fill(_size);
        }

        protected virtual void InitCopy(T copy)
        {
        }

        public T Enable()
        {
            var objectCopy = GetInactive();
            objectCopy.gameObject.SetActive(true);
            return objectCopy;
        }

        public T Enable<TFilter>()
        {
            var objectCopy = GetInactive<TFilter>();
            objectCopy.gameObject.SetActive(true);
            return objectCopy;
        }

        public T Enable(Vector3 position)
        {
            var objectCopy = GetInactive();
            var gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.SetActive(true);
            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation)
        {
            var objectCopy = GetInactive();
            var gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.transform.rotation = rotation;
            gameObjectCopy.SetActive(true);
            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation, Transform parent)
        {
            var objectCopy = GetInactive();
            var gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.transform.rotation = rotation;
            gameObjectCopy.transform.parent = parent;
            gameObjectCopy.SetActive(true);
            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation, Func<T, bool> filter)
        {
            var objectCopy = GetInactive(filter);
            var gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.transform.rotation = rotation;
            gameObjectCopy.SetActive(true);
            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation, Transform parent, Func<T, bool> filter)
        {
            var objectCopy = GetInactive(filter);
            var gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.transform.rotation = rotation;
            gameObjectCopy.transform.parent = parent;
            gameObjectCopy.SetActive(true);
            return objectCopy;
        }

        public IEnumerable<T> GetActiveObjects()
            => _objectsPool.Where(copy => copy.gameObject.activeSelf);

        public IEnumerable<T> GetReadOnlyCopies()
            => _objectsPool;

        protected void FillPool()
        {
            if (_isManualFill)
            {
                Clear();
            }

            Fill(_size);
        }

        private void CreateContainer()
        {
            _customContainer = new GameObject($"{typeof(T).Name} container").transform;

            if (_isStaticContainer)
            {
                return;
            }

            _customContainer.parent = transform;
        }

        private T GetNew(T copy)
        {
            var objectCopy = Instantiate(copy.gameObject.gameObject, _customContainer);
            objectCopy.SetActive(false);
            var component = objectCopy.GetComponent<T>();
            InitCopy(component);
            return component;
        }

        private T GetInactive(Func<T, bool> filter = null)
        {
            if (GetInactiveCount(filter) <= 0)
            {
                Expand(_expansionAmount, filter);
            }

            return _objectsPool
                .Where(filter ?? (copy => true))
                .First(copy => copy.gameObject.activeSelf == false);
        }

        private T GetInactive<TFilter>(Func<T, bool> filter = null)
        {
            if (GetInactiveCount(filter) <= 0)
            {
                Expand(_expansionAmount, filter);
            }

            return _objectsPool
                .Where(filter ?? (copy => true))
                .Where(copy => copy is TFilter)
                .First(copy => copy.gameObject.activeSelf == false);
        }

        private int GetInactiveCount(Func<T, bool> filter = null)
        {
            int count = _objectsPool
                .Where(filter ?? (copy => true))
                .Count(copy => copy.gameObject.activeSelf == false);

            return count;
        }

        private void Expand(int amount, Func<T, bool> filter = null)
        {
            _size += amount;

            for (int i = 0; i < amount; i++)
            {
                var filteredCopies = _copies.Where(filter ?? (copy => true));

                foreach (var filteredCopy in (IEnumerable) filteredCopies)
                {
                    Add(GetNew((T) filteredCopy));
                }
            }
        }

        private void Fill(int size)
        {
            foreach (var copy in _copies)
            {
                for (int i = _objectsPool.Count / _copies.Count; i < size; i++)
                {
                    Add(GetNew(copy));
                }
            }
        }

        private void Add(T copy)
        {
            _objectsPool.Add(copy);
            copy.Destroyed += Remove;
        }

        private void Remove(T copy)
        {
            copy.Destroyed -= Remove;
            _objectsPool.Remove(copy);
        }

        private void ValidateCopies()
        {
            if (_copies.Count == 0 || _copies.Any(copy => copy == null))
            {
                throw new NullReferenceException(NoCopies);
            }
        }

        private void Clear()
        {
            foreach (var copy in _objectsPool)
            {
                Remove(copy);
            }
        }
    }
}

