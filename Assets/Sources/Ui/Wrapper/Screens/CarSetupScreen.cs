using Sources.Data;
using Sources.Player.Factory;
using Sources.Saves;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class CarSetupScreen : SetupScreen, ISavedProgressReader
    {
        [SerializeField] private CarStatsOperatorView _statOperatorView;

        private void OnEnable()
        {
            print("car on enable");
            PlayerFactory.CarChanged += OnCarChanged;
        }

        private void OnDisable()
        {
            PlayerFactory.CarChanged -= OnCarChanged;
        }

        private void OnCarChanged(CarId id)
        {
            OnChanged((int) id);
            _statOperatorView.SetStats(id);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LastUnlockedId = progress.LastUnlockedCarId;
        }
    }
}