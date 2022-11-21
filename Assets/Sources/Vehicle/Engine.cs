using System;
using UnityEngine;

namespace Sources.Vehicle
{
    public sealed class Engine : MonoBehaviour
    {
        [SerializeField] private GasTank _gasTank;
        [SerializeField] [Min(0)] float _consumption = 0.01f;
        [SerializeField] [Min(0)] private float _acceleration = 0.4f;
        [SerializeField] [Min(0)] private float _maxMoveSpeed = 15f;

        private Func<float, float> _speedCalculation;

        private void Start()
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
            => currentSpeed;

        private void BurnFuel(float currentSpeed)
        {
            float burnAmount = currentSpeed * _consumption;
            _gasTank.Reduce(burnAmount);
        }
    }
}
