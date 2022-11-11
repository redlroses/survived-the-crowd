using System;
using System.Collections;
using Sources.Pool;
using UnityEngine;

namespace Sources.ShotEffects
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class ShotTrailView : MonoBehaviour, IPoolable<ShotTrailView>
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private float _lifeTime;

        private WaitForSeconds _waitForDisable;

        public event Action<ShotTrailView> Destroyed;

        private void Awake()
        {
            _waitForDisable = new WaitForSeconds(_lifeTime);
            _line ??= GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            StartCoroutine(LifeTime());
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void Create(Vector3 from, Vector3 to)
        {
            _line.SetPosition(0, from);
            _line.SetPosition(1, to);
        }

        private IEnumerator LifeTime()
        {
            yield return _waitForDisable;

            Disable();
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}