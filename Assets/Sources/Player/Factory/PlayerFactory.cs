using System;
using System.Collections.Generic;
using System.Linq;
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

        private Car _appliedCar;
        private Weapon _appliedWeapon;

        public event Action<CarId> CarChanged;
        public event Action<WeaponId> WeaponChanged;

        private Vector3 WeaponPivot => _availableCars.Current.WeaponPivot.localPosition;

        private void Awake()
        {
            InitCars();
            InitWeapons();
            ChangeCar(_availableCars.Current);
            ChangeWeapon(_availableWeapons.Current);
            _camera.Follow = _base.transform;
            _camera.LookAt = _base.transform;
            PlaceWeapon();
            ApplyCar();
            ApplyWeapon();
        }

        public void ShowNextCar()
        {
            ChangeCar(GetNext(_availableCars));
            PlaceWeapon();
        }

        public void ShowPreviousCar()
        {
            ChangeCar(GetPrevious(_availableCars));
            PlaceWeapon();
        }

        public void ShowNextWeapon()
        {
            ChangeWeapon(GetNext(_availableWeapons));
            PlaceWeapon();
        }

        public void ShowPreviousWeapon()
        {
            ChangeWeapon(GetPrevious(_availableWeapons));
            PlaceWeapon();
        }

        public void OnResume()
        {
            Hide(_availableCars.Current);
            Hide(_availableWeapons.Current);
            _availableCars.Reset(_appliedCar);
            _availableWeapons.Reset(_appliedWeapon);
            ChangeCar(_appliedCar);
            ChangeWeapon(_appliedWeapon);
        }

        public void ApplyCar()
        {
            _appliedCar = _availableCars.Current;
            PlayerHealth playerHealth = _appliedCar.GetComponentInChildren<PlayerHealth>();
            _base.Init(_appliedCar);
            _fuelView.Init(_appliedCar.GasTank);
            _loseDetector.Init(_appliedCar.GasTank, playerHealth);
            _healthView.Init(playerHealth);
            _enemyPool.Init(playerHealth);
        }

        public void ApplyWeapon()
        {
            _appliedWeapon = _availableWeapons.Current;
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

        private void ChangeCar(Car car)
        {
            Show(car);
            CarChanged?.Invoke(car.Id);
        }

        private void ChangeWeapon(Weapon weapon)
        {
            Show(weapon);
            WeaponChanged?.Invoke(weapon.Id);
        }

        private List<T> Spawn<T>(List<T> objects) where T : MonoBehaviour
        {
            List<T> spawnedCars = new List<T>(objects.Count);
            spawnedCars.AddRange(objects
                .Select(spawnedObject => Instantiate(spawnedObject, _base.transform)));

            return spawnedCars;
        }

        private T GetNext<T>(Iterable<T> iterable) where T : MonoBehaviour
        {
            Hide(iterable.Current);
            T current = iterable.Next();
            return current;
        }

        private T GetPrevious<T>(Iterable<T> iterable) where T : MonoBehaviour
        {
            Hide(iterable.Current);
            T current = iterable.Previous();
            return current;
        }

        private void Hide<T>(T iterable) where T : MonoBehaviour
        {
            iterable.gameObject.SetActive(false);
        }

        private void Show<T>(T iterable) where T : MonoBehaviour
        {
            iterable.gameObject.SetActive(true);
        }

        private void PlaceWeapon()
        {
            _availableWeapons.Current.transform.localPosition = WeaponPivot;
        }
    }
}