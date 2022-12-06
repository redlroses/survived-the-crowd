using DG.Tweening;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration;

        public void Show(bool isSmoothly)
        {
            SetAlpha(1f, isSmoothly);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide(bool isSmoothly)
        {
            SetAlpha(0, isSmoothly);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void SetAlpha(float alpha, bool isSmoothly)
        {
            if (isSmoothly)
            {
                _canvasGroup.DOFade(alpha, _duration).Play();
            }
            else
            {
                _canvasGroup.alpha = alpha;
            }
        }
    }
}
