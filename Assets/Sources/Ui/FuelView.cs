using Sources.Ui.Animations;
using Sources.Ui.Wrapper;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Ui
{
    public sealed class FuelView : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private GasTank _gasTank;
        [SerializeField] private bool _isSmoothly;
        [SerializeField] private LowFuelAnimation _lowFuelAnimation;
        [SerializeField] private SliderSetter _slider;

        private float _fuelLevel;

        private void Start()
        {
            _slider.SetBounds(0, _gasTank.MaxFuelLevelPercent);
            UpdateView();
        }

        private void Update()
        {
            if (_gasTank.IsEmpty)
            {
                return;
            }

            _fuelLevel = Mathf.Lerp(_fuelLevel, _gasTank.FuelLevelPercent, _duration * Time.deltaTime);
            UpdateView();
        }

        public void Init(GasTank gasTank)
        {
            _gasTank = gasTank;
            _lowFuelAnimation.Init(gasTank);
        }

        private void UpdateView()
        {
            _slider.SetValue(_fuelLevel, _isSmoothly);
        }
    }
}