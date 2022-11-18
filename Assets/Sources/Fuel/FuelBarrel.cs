using System;
using Sources.Pool;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Fuel
{
    public sealed class FuelBarrel : MonoBehaviour, IPoolable<FuelBarrel>
    {
        [SerializeField] private float _fuelAmount = 30f;

        public event Action<FuelBarrel> Destroyed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GasTank gasTank) == false)
            {
                return;
            }

            gasTank.Refuel(_fuelAmount);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }
    }
}
