using System;
using Cinemachine;
using Sources.Level;
using Sources.Ui;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Player
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private GameObject _base;
        [SerializeField] private GameObject _currentWeapon;
        [SerializeField] private Car _currentCar;
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private LevelLauncher _levelLauncher;
        [SerializeField] private FuelView _fuelView;

        public event Action<GameObject> PlayerAssembled;

        private void Awake()
        {
            SetCar();
            SetWeapon();
            PlayerAssembled?.Invoke(_base);
            _camera.Follow = _base.transform;
            _camera.LookAt = _base.transform;
        }

        private void SetWeapon()
        {
            var weapon = Instantiate(_currentWeapon, _base.transform);
        }

        private void SetCar()
        {
            Car car = Instantiate(_currentCar, _base.transform);
            _base.GetComponent<PlayerMover>().Init(car);
            _loseDetector.Init(car.GasTank);
            _levelLauncher.Init(car);
            _fuelView.Init(car.GasTank);
        }
    }
}