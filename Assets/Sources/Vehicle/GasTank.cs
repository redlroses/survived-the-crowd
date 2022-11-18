using Sources.Tools;
using UnityEngine;

namespace Sources.Vehicle
{
    public sealed class GasTank : MonoBehaviour
    {
        [SerializeField] private float _fuelLevel;
        [SerializeField] private float _maxFuel;

        public bool Empty => _fuelLevel <= 0;
        public float FuelLevelPercent => _fuelLevel / _maxFuel * Constants.ToPercent;

        private void Start()
        {
            _fuelLevel = _maxFuel;
        }

        public void Refuel(float amount)
        {
            _fuelLevel += amount;

            if (_fuelLevel > _maxFuel)
            {
                _fuelLevel = _maxFuel;
            }
        }

        public void Reduce(float amount)
        {
            if (Empty)
            {
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
