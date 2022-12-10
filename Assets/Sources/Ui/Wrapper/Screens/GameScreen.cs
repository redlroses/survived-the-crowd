using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public sealed class GameScreen : Screen
    {
        [SerializeField] private HealthView _healthView;
        [SerializeField] private FuelView _fuelView;

        protected override void OnShow()
        {
            _healthView.enabled = true;
            _fuelView.enabled = true;
        }

        protected override void OnHide()
        {
            _healthView.enabled = false;
            _fuelView.enabled = false;
        }
    }
}
