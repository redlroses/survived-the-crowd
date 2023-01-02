using Sources.Data;
using Sources.Player.Factory;
using Sources.Saves;

namespace Sources.Ui.Wrapper.Screens
{
    public class WeaponSetupScreen : SetupScreen, ISavedProgressReader
    {
        private void OnEnable()
        {
            PlayerFactory.WeaponChanged += OnWeaponChanged;
        }

        private void OnDisable()
        {
            PlayerFactory.WeaponChanged -= OnWeaponChanged;
        }

        private void OnWeaponChanged(WeaponId id)
        {
            OnChanged((int) id);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LastUnlockedId = progress.LastUnlockedWeaponId;
        }
    }
}