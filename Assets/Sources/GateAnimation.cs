using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Sources
{
    public sealed class GateAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Transform _barrier;

        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _openTween;

        private void Awake()
        {
            _openTween = _barrier.DORotate(new Vector3(0, 0, -90), _duration);
        }

        public void Open()
        {
            _openTween.Restart();
        }

        public void Reset()
        {
            _barrier.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }
}
