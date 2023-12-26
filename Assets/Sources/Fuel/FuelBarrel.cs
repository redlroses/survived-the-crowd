using System;
using Sources.Pool;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Fuel
{
    public sealed class FuelBarrel : MonoBehaviour, IPoolable<FuelBarrel>
    {
        public event Action<FuelBarrel> Destroyed;

        public event Action Picked;

        [field: SerializeField] private float FuelAmount { get; } = 30f;

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GasTank gasTank) == false)
            {
                return;
            }

            gasTank.Refuel(FuelAmount);
            Picked?.Invoke();
            gameObject.SetActive(false);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}