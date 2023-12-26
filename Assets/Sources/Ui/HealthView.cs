using Sources.HealthLogic;
using Sources.Player;
using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Ui
{
    public sealed class HealthView : MonoBehaviour
    {
        [SerializeField] private readonly bool _isSmoothly = true;
        [SerializeField] private Health _health;
        [SerializeField] private SliderSetter _slider;
        [SerializeField] private TextSetter _text;

        private void OnEnable()
        {
            _health.Changed += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            _health.Changed -= UpdateView;
        }

        public void Init(PlayerHealth health)
        {
            _health = health;
            _slider.SetBounds(0, _health.Max);
        }

        private void UpdateView()
        {
            _slider.SetValue(_health.Current, _isSmoothly);
            _text.Set($"{_health.Current}/{_health.Max}");
        }
    }
}