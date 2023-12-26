using Sources.Data;
using Sources.Player.Factory;
using Sources.Saves;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class WeaponSetupScreen : SetupScreen, ISavedProgressReader
    {
        [SerializeField] private WeaponStatsOperatorView _statOperatorView;

        protected override void OnEnable()
        {
            base.OnEnable();
            PlayerFactory.WeaponChanged += OnWeaponChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PlayerFactory.WeaponChanged -= OnWeaponChanged;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LastUnlockedId = (int)progress.LastUnlockedWeapon;
            OnWeaponChanged(progress.LastChosenWeapon);
        }

        protected override void UpdateUnlock()
        {
            LastUnlockedId = (int)UnlockProvider.UnlockedWeapon;
        }

        private void OnWeaponChanged(WeaponId id)
        {
            OnChanged((int)id);
            _statOperatorView.SetStats(id);
        }
    }
}