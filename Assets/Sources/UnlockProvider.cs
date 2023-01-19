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

        private CarId _unlockedCar = CarId.Buggy;
        private WeaponId _unlockedWeapon = WeaponId.MachineGun;

        public CarId UnlockedCar => _unlockedCar;
        public WeaponId UnlockedWeapon => _unlockedWeapon;
        public event Action Updated;

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
            _unlockedWeapon = id;
            Updated?.Invoke();
        }

        private void OnNewCarUnlocked(CarId id)
        {
            _unlockedCar = id;
            Updated?.Invoke();
        }
    }
}
