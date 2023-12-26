using System;
using Sources.Collectables;
using Sources.Player.Factory;
using UnityEngine;

namespace Sources
{
    public class UnlockProvider : MonoBehaviour
    {
        [SerializeField] private DetailsCollector _detailsCollector;

        [SerializeField] private WeaponUpgradeCollector _weaponUpgradeCollector;

        public event Action Updated;

        public CarId UnlockedCar { get; private set; } = CarId.HotRod;

        public WeaponId UnlockedWeapon { get; private set; } = WeaponId.MachineGun;

        private void OnEnable()
        {
            _weaponUpgradeCollector.NewWeaponUnlocked += OnNewWeaponUnlocked;
            _detailsCollector.NewCarUnlocked += OnNewCarUnlocked;
        }

        private void OnDisable()
        {
            _weaponUpgradeCollector.NewWeaponUnlocked -= OnNewWeaponUnlocked;
            _detailsCollector.NewCarUnlocked -= OnNewCarUnlocked;
        }

        private void OnNewWeaponUnlocked(WeaponId id)
        {
            UnlockedWeapon = id;
            Updated?.Invoke();
        }

        private void OnNewCarUnlocked(CarId id)
        {
            UnlockedCar = id;
            Updated?.Invoke();
        }
    }
}