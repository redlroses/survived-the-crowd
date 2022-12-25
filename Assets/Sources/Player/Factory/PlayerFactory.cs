using System.Collections.Generic;
using Cinemachine;
using Sources.Level;
using Sources.Pool;
using Sources.Turret;
using Sources.Ui;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Player
{
    public class PlayerFactory : MonoBehaviour
    {
        private readonly List<Car> _availableCars = new List<Car>();

        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private PlayerMover _base;
        [SerializeField] private TargetSeeker _currentTurret;
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private FuelView _fuelView;
        [SerializeField] private HealthView _healthView;

        [SerializeField] private List<Car> _cars = new List<Car>();
        [SerializeField] private List<GameObject> _weapons = new List<GameObject>();

        private Car _currentCar;
        private int _carIndex;

        private TargetSeeker _currentWeapon;
        private int _weaponIndex;

        private void Awake()
        {
            Init();
            SetCar(0);
            ApplyCar();
            SetWeapon();
            _camera.Follow = _base.transform;
            _camera.LookAt = _base.transform;
        }

        public void ShowNextCar()
        {
            _carIndex++;

            if (_carIndex >= _cars.Count)
            {
                _carIndex = 0;
            }

            SetCar(_carIndex);
        }

        public void ShowPreviousCar()
        {
            _carIndex--;

            if (_carIndex < 0)
            {
                _carIndex = _availableCars.Count - 1;
            }

            SetCar(_carIndex);
        }

        public void ApplyCar()
        {
            var playerHealth = _currentCar.GetComponentInChildren<PlayerHealth>();

            _base.Init(_currentCar);
            _fuelView.Init(_currentCar.GasTank);
            _loseDetector.Init(_currentCar.GasTank, playerHealth);
            _healthView.Init(playerHealth);
            _enemyPool.Init(playerHealth);
        }

        private void SetCar(int index)
        {
            if (_currentCar != null)
            {
                _currentCar.gameObject.SetActive(false);
            }

            _currentCar = _availableCars[index];
            _currentCar.gameObject.SetActive(true);
        }

        private void SetWeapon()
        {
            var weapon = Instantiate(_currentWeapon, _base.transform);
        }

        private void Init<T>(List<T> objects) where T : MonoBehaviour
        {
            foreach (T item in objects)
            {
                T currentItem = Instantiate(item, _base.transform);
                item.gameObject.SetActive(false);
                _availableCars.Add(item);
            }
        }
    }
}