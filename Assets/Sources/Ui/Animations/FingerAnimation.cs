using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Sources.Ui.Animations
{
    public sealed class FingerAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _moveOffset = 50f;
        [SerializeField] private RectTransform _finger;

        private TweenerCore<Vector2, Vector2, VectorOptions> _animation;

        private void Awake()
        {
            _finger.anchoredPosition = new Vector2(-_moveOffset, _finger.anchoredPosition.y);
            _animation = _finger.DOAnchorPosX(_moveOffset, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        private void OnEnable()
        {
            _animation.Restart();
        }

        private void OnDisable()
        {
            _animation.Pause();
        }
    }
}