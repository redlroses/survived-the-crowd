using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Sources
{
    public sealed class GateAnimation : MonoBehaviour
    {
        private readonly Vector3 _rotatedVector = new Vector3(0, 0, -90);
        private readonly Vector3 _defaultVector = new Vector3(0, 90, 0);

        [SerializeField] private Transform _barrier;
        [SerializeField] private float _duration;

        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _openTween;

        public void Reset()
        {
            _barrier.transform.rotation = Quaternion.Euler(_defaultVector);
        }

        private void Awake()
        {
            _openTween = _barrier.DORotate(_rotatedVector, _duration);
        }

        public void Open()
        {
            _openTween.Restart();
        }
    }
}