using System;
using System.Collections.Generic;
using Cinemachine;
using Sources.Collectables;
using Sources.Level;
using Sources.Pool;
using Sources.Turret;
using Sources.Ui;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Player.Factory
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private PlayerMover _base;
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private FuelView _fuelView;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private List<Car> _cars = new List<Car>();
        [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

        private Iterable<Car> _availableCars;
        private Iterable<Weapon> _availableWeapons;

        private Car _currentCar;
        private Weapon _currentWeapon;

        public event Action<CarId> CarChanged;
        public event Action<WeaponId> WeaponChanged;

        private void Awake()
        {
            InitCars();
            InitWeapons();
            ApplyCar();
            ApplyWeapon();
            ShowPreviousCar();
            ShowPreviousWeapon();
            _camera.Follow = _base.transform;
            _camera.LookAt = _base.transform;
        }

        public void ShowNextCar()
        {
            _currentCar = ShowNext(_availableCars);
            CarChanged?.Invoke(_currentCar.Id);
        }

        public void ShowPreviousCar()
        {
            _currentCar = ShowPrevious(_availableCars);
            CarChanged?.Invoke(_currentCar.Id);
        }

        public void ShowNextWeapon()
        {
            _currentWeapon = ShowNext(_availableWeapons);
            WeaponChanged?.Invoke(_currentWeapon.Id);
        }

        public void ShowPreviousWeapon()
        {
            _currentWeapon = ShowPrevious(_availableWeapons);
            WeaponChanged?.Invoke(_currentWeapon.Id);
        }

        public void ApplyCar()
        {
            Car currentCar = _availableCars.Current;
            var playerHealth = currentCar.GetComponentInChildren<PlayerHealth>();
            _base.Init(currentCar);
            _fuelView.Init(currentCar.GasTank);
            _loseDetector.Init(currentCar.GasTank, playerHealth);
            _healthView.Init(playerHealth);
            _enemyPool.Init(playerHealth);
        }

        public void ApplyWeapon()
        {
            TargetSeeker currentWeapon = _availableWeapons.Current.TargetSeeker;
        }

        private void InitCars()
        {
            _availableCars = new Iterable<Car>(Spawn(_cars), 0);
        }

        private void InitWeapons()
        {
            _availableWeapons = new Iterable<Weapon>(Spawn(_weapons), 0);

            foreach (Weapon weapon in _availableWeapons)
            {
                weapon.TargetSeeker.Init(_loseDetector);
            }
        }

        private List<T> Spawn<T>(List<T> objects) where T : MonoBehaviour
        {
            List<T> spawnedCars = new List<T>(objects.Count);

            foreach (T spawnedObject in objects)
            {
                T currentItem = Instantiate(spawnedObject, _base.transform);
                spawnedCars.Add(currentItem);
            }

            return spawnedCars;
        }

        private T ShowNext<T>(Iterable<T> iterable) where T : MonoBehaviour
        {
            iterable.Current.gameObject.SetActive(false);
            T current = iterable.Next();
            current.gameObject.SetActive(true);
            return current;
        }

        private T ShowPrevious<T>(Iterable<T> iterable) where T : MonoBehaviour
        {
            iterable.Current.gameObject.SetActive(false);
            T current = iterable.Previous();
            current.gameObject.SetActive(true);
            return current;
        }
    }

    public enum WeaponId
    {
        MachineGun,
        RocketLauncher
    }
}