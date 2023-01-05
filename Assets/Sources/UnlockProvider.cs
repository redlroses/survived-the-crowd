using System;
using Sources.Collectables;
using Sources.Player.Factory;
using UnityEngine;

namespace Sources
{
    public class UnlockProvider : MonoBehaviour
    {
        [SerializeField] private DetailsCollector _detailsCollector;

        private CarId _unlockedCar = CarId.Buggy;
        private WeaponId _unlockedWeapon = WeaponId.MachineGun;

        public CarId UnlockedCar => _unlockedCar;
        public WeaponId UnlockedWeapon => _unlockedWeapon;
        public event Action Updated;

        private void OnEnable()
        {
            _detailsCollector.NewCarUnlocked += OnNewCarUnlocked;
        }

        private void OnDisable()
        {
            _detailsCollector.NewCarUnlocked -= OnNewCarUnlocked;
        }

        private void OnNewCarUnlocked(CarId id)
        {
            _unlockedCar = id;
            Updated?.Invoke();
        }
    }
}
