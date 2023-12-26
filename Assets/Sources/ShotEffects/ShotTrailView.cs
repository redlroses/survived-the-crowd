using System;
using System.Collections;
using DG.Tweening;
using Sources.Pool;
using UnityEngine;

namespace Sources.ShotEffects
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class ShotTrailView : MonoBehaviour, IPoolable<ShotTrailView>
    {
        [SerializeField] private float _lifeTime;
        [SerializeField] private LineRenderer _line;

        private Tweener _fadeTween;
        private float _startWidth;
        private WaitForSeconds _waitForDisable;

        public event Action<ShotTrailView> Destroyed;

        private void Awake()
        {
            _startWidth = _line.startWidth;
            _waitForDisable = new WaitForSeconds(_lifeTime);
            _line ??= GetComponent<LineRenderer>();
            _fadeTween = DOTween.To(width => _line.startWidth = width, _line.startWidth, 0, _lifeTime);
        }

        private void OnEnable()
        {
            Init();
            StartCoroutine(LifeTime());
            StartFading();
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

        private void Init()
        {
            _line.startWidth = _startWidth;
        }

        private void StartFading()
        {
            _fadeTween.Restart();
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