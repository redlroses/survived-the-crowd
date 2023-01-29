using DG.Tweening;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration;

        private Tween _tween;

        public bool IsActive => _canvasGroup.interactable;

        public void Show(bool isSmoothly)
        {
            SetAlpha(1f, isSmoothly);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            OnShow();
        }

        public void Hide(bool isSmoothly)
        {
            SetAlpha(0, isSmoothly);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            OnHide();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        private void SetAlpha(float alpha, bool isSmoothly)
        {
            _tween?.Kill();

            if (isSmoothly)
            {
                _tween = _canvasGroup.DOFade(alpha, _duration).Play();
            }
            else
            {
                _canvasGroup.alpha = alpha;
            }
        }
    }
}
