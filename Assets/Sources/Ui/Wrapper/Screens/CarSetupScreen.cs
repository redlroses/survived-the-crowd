using Sources.Data;
using Sources.Player.Factory;
using Sources.Saves;

namespace Sources.Ui.Wrapper.Screens
{
    public class CarSetupScreen : SetupScreen, ISavedProgressReader
    {
        private void OnEnable()
        {
            PlayerFactory.CarChanged += OnCarChanged;
        }

        private void OnDisable()
        {
            PlayerFactory.CarChanged -= OnCarChanged;
        }

        private void OnCarChanged(CarId id)
        {
            OnChanged((int) id);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LastUnlockedId = progress.LastUnlockedCarId;
        }
    }
}