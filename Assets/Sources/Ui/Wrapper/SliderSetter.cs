using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Wrapper
{
    public sealed class SliderSetter : MonoBehaviour
    {
        [SerializeField] private float _animationDuration;
        [SerializeField] private Slider _slider;

        private Tweener _tweener;

        public void SetValue(float progress, bool isSmoothly = false)
        {
            _tweener?.Kill();

            if (isSmoothly)
            {
                _tweener = DOTween.To(Apply, _slider.value, progress, _animationDuration).Play();
            }
            else
            {
                Apply(progress);
            }
        }

        public void SetBounds(float min, float max)
        {
            _slider.minValue = min;
            _slider.maxValue = max;
        }

        private void Apply(float progress)
        {
            if (_slider.wholeNumbers)
            {
                _slider.value = Mathf.RoundToInt(progress);
            }
            else
            {
                _slider.value = progress;
            }
        }
    }
}