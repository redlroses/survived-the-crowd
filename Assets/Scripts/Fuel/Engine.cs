using System;
using UnityEngine;

namespace Fuel
{
    public sealed class Engine : MonoBehaviour
    {
        [SerializeField] private GasTank _gasTank;
        [SerializeField] private float _consumption = 0.01f;
        [SerializeField] private float _acceleration = 0.4f;
        [SerializeField] private float _deceleration = 0.2f;
        [SerializeField] private float _maxMoveSpeed = 15f;

        private Func<float, float> _speedCalculation;

        private void Start()
        {
            _speedCalculation = UniformMotion;
        }

        public float CalculateNewSpeed(float currentSpeed)
        {
            return _speedCalculation(currentSpeed);
        }

        public void BeginAcceleration()
        {
            _speedCalculation = Accelerate;
        }

        public void BeginDeceleration()
        {
            _speedCalculation = Decelerate;
        }

        private float Accelerate(float currentSpeed)
        {
            if (_gasTank.Empty)
            {
                BeginDeceleration();
                return currentSpeed;
            }

            if (currentSpeed >= _maxMoveSpeed)
            {
                return currentSpeed;
            }

            BurnFuel(currentSpeed);
            return currentSpeed + _acceleration;
        }

        private float Decelerate(float currentSpeed)
        {
            if (currentSpeed <= 0)
            {
                return 0;
            }

            return currentSpeed - _deceleration;
        }

        private float UniformMotion(float currentSpeed)
        {
            return currentSpeed;
        }

        private void BurnFuel(float currentSpeed)
        {
            float burnAmount = currentSpeed * _consumption;
            _gasTank.Reduce(burnAmount);
        }
    }
}
