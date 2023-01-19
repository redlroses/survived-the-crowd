using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Sources.Collectables;
using Sources.Data;
using Sources.Level;
using Sources.Pool;
using Sources.Saves;
using Sources.Turret;
using Sources.Ui;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Player.Factory
{
    public class PlayerFactory : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private PlayerMover _base;
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private PlayerReviver _playerReviver;
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

        public CarId CurrentCar => _availableCars.Current.Id;
        private Vector3 WeaponPivot => _availableCars.Current.WeaponPivot.localPosition;

        public void ShowNextCar()
        {
            ChangeCar(GetNext(_availableCars));
        }

        public void ShowPreviousCar()
        {
            ChangeCar(GetPrevious(_availableCars));
        }

        public void ShowNextWeapon()
        {
            ChangeWeapon(GetNext(_availableWeapons));
        }

        public void ShowPreviousWeapon()
        {
            ChangeWeapon(GetPrevious(_availableWeapons));
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
            _playerReviver.Init(_appliedCar);
        }

        public void ApplyWeapon()
        {
            _appliedWeapon = _availableWeapons.Current;
            _playerReviver.Init(_appliedWeapon);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Init(progress);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.LastChosenCar = _availableCars.Current.Id;
            progress.LastChosenWeapon = _availableWeapons.Current.Id;
        }

        private void Init(PlayerProgress progress)
        {
            InitCars(progress.LastChosenCar);
            InitWeapons(progress.LastChosenWeapon);
            ChangeCar(_availableCars.Current);
            ChangeWeapon(_availableWeapons.Current);
            InitCamera();
            PlaceWeapon();
            ApplyCar();
            ApplyWeapon();
        }

        private void InitCamera()
        {
            var baseTransform = _base.transform;
            _camera.Follow = baseTransform;
            _camera.LookAt = baseTransform;
        }

        private void InitCars(CarId chosenCar)
        {
            List<Car> ordered = Sort(Spawn(_cars), car => car.Id);
            _availableCars = new Iterable<Car>(ordered, (int) chosenCar);
        }

        private void InitWeapons(WeaponId chosenWeapon)
        {
            List<Weapon> ordered = Sort(Spawn(_weapons), weapon => weapon.Id);
            _availableWeapons = new Iterable<Weapon>(ordered, (int) chosenWeapon);

            foreach (Weapon weapon in _availableWeapons)
            {
                weapon.TargetSeeker.Init(_loseDetector);
            }
        }

        private List<T> Sort<T, TKey>(IEnumerable<T> unsorted, Func<T, TKey> keySelector) where T : MonoBehaviour
        {
            List<T> ordered = unsorted.OrderBy(keySelector).ToList();
            return ordered;
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
            List<T> spawned = new List<T>(objects.Count);
            spawned.AddRange(objects
                .Select(spawnedObject => Instantiate(spawnedObject, _base.transform)));

            return spawned;
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
            PlaceWeapon();
        }

        private void PlaceWeapon()
        {
            _availableWeapons.Current.transform.localPosition = WeaponPivot;
        }
    }
}