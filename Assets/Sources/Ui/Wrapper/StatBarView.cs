using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Wrapper
{
    public class StatBarView : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private RectTransform _framesContainer;

        public void SetStat(float normalizedValue)
        {
            float frameSize = 1f / _framesContainer.childCount;
            float roundedValue = Mathf.RoundToInt(normalizedValue / frameSize);
            _fill.fillAmount = roundedValue * frameSize;
        }
    }
}
