using System;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Vehicle
{
    public sealed class Engine : MonoBehaviour
    {
        [SerializeField] private GasTank _gasTank;
        [SerializeField] [Min(0)] private float _consumption = 0.005f;
        [SerializeField] [Min(0)] private float _idleConsumption = 0.001f;
        [SerializeField] [Min(0)] private float _acceleration = 0.4f;
        [SerializeField] [Min(0)] private float _maxMoveSpeed = 15f;

        private Func<float, float> _speedCalculation;

        private void Awake()
        {
            _speedCalculation = UniformMotion;
        }

        private void OnEnable()
        {
            _gasTank.Empty += OnEmptyGasTank;
        }

        private void OnDisable()
        {
            _gasTank.Empty -= OnEmptyGasTank;
        }

        public void Construct(CarStaticData carStaticData)
        {
            _consumption = carStaticData.Consumption;
            _idleConsumption = carStaticData.IdleConsumption;
            _acceleration = carStaticData.Acceleration;
            _maxMoveSpeed = carStaticData.MaxSpeed;
        }

        private void OnEmptyGasTank()
        {
            StopAcceleration();
        }

        public float CalculateNewSpeed(float currentSpeed)
            => _speedCalculation(currentSpeed);

        public void BeginAcceleration()
        {
            if (_gasTank.IsEmpty)
            {
                StopAcceleration();
                return;
            }

            _speedCalculation = Accelerate;
        }

        public void StopAcceleration()
        {
            _speedCalculation = UniformMotion;
        }

        private float Accelerate(float currentSpeed)
        {
            if (currentSpeed >= _maxMoveSpeed)
            {
                return currentSpeed;
            }

            BurnFuel(currentSpeed);
            return currentSpeed + _acceleration;
        }

        private float UniformMotion(float currentSpeed)
        {
            BurnFuel(currentSpeed);
            return currentSpeed;
        }

        private void BurnFuel(float currentSpeed)
        {
            float burnAmount = _idleConsumption + currentSpeed * _consumption;
            _gasTank.Reduce(burnAmount);
        }
    }
}
