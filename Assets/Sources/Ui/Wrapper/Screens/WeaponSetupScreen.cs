using Sources.Data;
using Sources.Player.Factory;
using Sources.Saves;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class WeaponSetupScreen : SetupScreen, ISavedProgressReader
    {
        [SerializeField] private WeaponStatsOperatorView _statOperatorView;

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
            _statOperatorView.SetStats(id);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LastUnlockedId = progress.LastUnlockedWeaponId;
            OnWeaponChanged(progress.LastChosenWeapon);
        }
    }
}