using Sources.Data;
using Sources.Player.Factory;
using Sources.Saves;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class CarSetupScreen : SetupScreen, ISavedProgressReader
    {
        [SerializeField] private CarStatsOperatorView _statOperatorView;

        protected override void OnEnable()
        {
            base.OnEnable();
            PlayerFactory.CarChanged += OnCarChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PlayerFactory.CarChanged -= OnCarChanged;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LastUnlockedId = (int)progress.LastUnlockedCar;
            OnCarChanged(progress.LastChosenCar);
        }

        protected override void UpdateUnlock()
        {
            LastUnlockedId = (int)UnlockProvider.UnlockedCar;
        }

        private void OnCarChanged(CarId id)
        {
            OnChanged((int)id);
            _statOperatorView.SetStats(id);
        }
    }
}