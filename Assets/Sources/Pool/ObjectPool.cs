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

        [SerializeField] private int _expansionAmount = 4;
        [SerializeField] private List<T> _copies;
        [SerializeField] private Transform _customContainer;
        [SerializeField] private bool _isManualFill;
        [SerializeField] private bool _isStaticContainer;

        [Header("Core Settings")]
        [SerializeField] private int _size = 8;

        protected ObjectPool()
        {
            _objectsPool = new List<T>(_size);
        }

        public Transform Container => _customContainer;

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

        public T Enable()
        {
            T objectCopy = GetInactive();
            objectCopy.gameObject.SetActive(true);

            return objectCopy;
        }

        public T Enable<TFilter>()
        {
            T objectCopy = GetInactive<TFilter>();
            objectCopy.gameObject.SetActive(true);

            return objectCopy;
        }

        public T Enable(Vector3 position)
        {
            T objectCopy = GetInactive();
            GameObject gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.SetActive(true);

            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation)
        {
            T objectCopy = GetInactive();
            GameObject gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.transform.rotation = rotation;
            gameObjectCopy.SetActive(true);

            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation, Transform parent)
        {
            T objectCopy = GetInactive();
            GameObject gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.transform.rotation = rotation;
            gameObjectCopy.transform.parent = parent;
            gameObjectCopy.SetActive(true);

            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation, Func<T, bool> filter)
        {
            T objectCopy = GetInactive(filter);
            GameObject gameObjectCopy = objectCopy.gameObject;
            gameObjectCopy.transform.position = position;
            gameObjectCopy.transform.rotation = rotation;
            gameObjectCopy.SetActive(true);

            return objectCopy;
        }

        public T Enable(Vector3 position, Quaternion rotation, Transform parent, Func<T, bool> filter)
        {
            T objectCopy = GetInactive(filter);
            GameObject gameObjectCopy = objectCopy.gameObject;
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

        protected virtual void InitCopy(T copy)
        {
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
            GameObject objectCopy = Instantiate(copy.gameObject.gameObject, _customContainer);
            objectCopy.SetActive(false);
            T component = objectCopy.GetComponent<T>();
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
                IEnumerable<T> filteredCopies = _copies.Where(filter ?? (copy => true));

                foreach (object filteredCopy in (IEnumerable)filteredCopies)
                {
                    Add(GetNew((T)filteredCopy));
                }
            }
        }

        private void Fill(int size)
        {
            foreach (T copy in _copies)
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
            foreach (T copy in _objectsPool)
            {
                Remove(copy);
            }
        }
    }
}