using Sources.HealthLogic;
using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Ui
{
    public sealed class HealthView : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private SliderSetter _slider;
        [SerializeField] private TextSetter _text;
        [SerializeField] private bool _isSmoothly = true;

        private void Start()
        {
            _slider.SetBounds(0, _health.Max);
        }

        private void OnEnable()
        {
            _health.Changed += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            _health.Changed -= UpdateView;
        }

        private void UpdateView()
        {
            _slider.SetValue(_health.Current, _isSmoothly);
            _text.Set($"{_health.Current}/{_health.Max}");
        }
    }
}