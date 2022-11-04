using System;
using UnityEngine;
using Vehicle;

namespace Sources.Vehicle
{
    public sealed class Engine : MonoBehaviour
    {
        [SerializeField] private GasTank _gasTank;
        [SerializeField] [Min(0)] float _consumption = 0.01f;
        [SerializeField] [Min(0)] private float _acceleration = 0.4f;
        [SerializeField] [Min(0)] private float _deceleration = 0.2f;
        [SerializeField] [Min(0)] private float _maxMoveSpeed = 15f;
        [SerializeField] [Min(0)] private float _minMoveSpeed = 0.0001f;

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
            if (_gasTank.Empty)
            {
                BeginDeceleration();
                return;
            }

            _speedCalculation = Accelerate;
        }

        public void BeginDeceleration()
        {
            _speedCalculation = Decelerate;
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

        private float Decelerate(float currentSpeed)
        {
            if (currentSpeed <= _minMoveSpeed)
            {
                _speedCalculation = UniformMotion;
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
