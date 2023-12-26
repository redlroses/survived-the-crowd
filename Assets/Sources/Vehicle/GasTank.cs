using System;
using Sources.StaticData;
using Sources.Tools;
using UnityEngine;

namespace Sources.Vehicle
{
    public sealed class GasTank : MonoBehaviour
    {
        [SerializeField] private float _fuelLevel;

        public event Action Ended;

        public bool IsEmpty => _fuelLevel <= 0;

        public float FuelLevelPercent => _fuelLevel / MaxFuelLevel * Constants.ToPercent;

        public float MaxFuelLevelPercent => 100f;

        [field: SerializeField]
        public float MaxFuelLevel { get; private set; }

        private void OnEnable()
        {
            _fuelLevel = MaxFuelLevel;
        }

        public void Construct(CarStaticData carStaticData)
        {
            MaxFuelLevel = carStaticData.MaxFuel;
        }

        public void Refuel(float amount)
        {
            _fuelLevel += amount;

            if (_fuelLevel > MaxFuelLevel)
            {
                _fuelLevel = MaxFuelLevel;
            }
        }

        public void Reduce(float amount)
        {
            if (IsEmpty)
            {
                Ended?.Invoke();

                return;
            }

            _fuelLevel -= amount;

            if (_fuelLevel < 0)
            {
                _fuelLevel = 0;
            }
        }
    }
}