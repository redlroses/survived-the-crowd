using Sources.Vehicle;
using UnityEngine;

namespace Sources.Ui.Wrapper
{
    public sealed class FuelView : MonoBehaviour
    {
        [SerializeField] private GasTank _gasTank;
        [SerializeField] private SliderSetter _slider;
        [SerializeField] private bool _isSmoothly = false;
        [SerializeField] private float _duration;

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

        private void UpdateView()
        {
            _slider.SetValue(_fuelLevel, _isSmoothly);
        }
    }
}
