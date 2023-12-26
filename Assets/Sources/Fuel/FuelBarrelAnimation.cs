using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Sources.Fuel
{
    public sealed class FuelBarrelAnimation : MonoBehaviour
    {
        private readonly int _infinity = -1;
        private readonly float _moveDuration = 1f;
        private readonly float _moveHeight = 1f;
        private readonly float _rotateDuration = 2f;
        private readonly Vector3 _rotateVector = new Vector3(0, 360, 0);

        private TweenerCore<Vector3, Vector3, VectorOptions> _moveTween;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _rotateTween;

        private void Awake()
        {
            _moveTween = transform.DOMoveY(_moveHeight, _moveDuration).SetLoops(_infinity, LoopType.Yoyo);

            _rotateTween = transform.DORotate(_rotateVector, _rotateDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(_infinity);

            _moveTween.Pause();
            _rotateTween.Pause();
        }

        private void OnEnable()
        {
            _moveTween.Play();
            _rotateTween.Play();
        }

        private void OnDisable()
        {
            _moveTween.Pause();
            _rotateTween.Pause();
        }
    }
}