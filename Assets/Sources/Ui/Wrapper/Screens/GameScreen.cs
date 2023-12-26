using Sources.Timer;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public sealed class GameScreen : Screen
    {
        [SerializeField] private CarUnlockView _carUnlockView;
        [SerializeField] private FuelView _fuelView;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private TimerView _timerView;

        protected override void OnShow()
        {
            _healthView.enabled = true;
            _fuelView.enabled = true;
            _carUnlockView.enabled = true;
            _timerView.enabled = true;
        }

        protected override void OnHide()
        {
            _healthView.enabled = false;
            _fuelView.enabled = false;
            _carUnlockView.enabled = false;
            _timerView.enabled = false;
        }
    }
}