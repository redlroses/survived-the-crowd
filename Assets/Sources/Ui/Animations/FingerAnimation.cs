using DG.Tweening;
using UnityEngine;

namespace Sources.Ui.Animations
{
    public sealed class FingerAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _finger;
        [SerializeField] private float _moveOffset = 50f;
        [SerializeField] private float _duration = 1f;

        private void Start()
        {
            _finger.anchoredPosition = new Vector2(-_moveOffset, _finger.anchoredPosition.y);
            _finger.DOAnchorPosX(_moveOffset, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).Play();
        }
    }
}
