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
        public event Action PickedUp;

        public float FuelAmount => _fuelAmount;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GasTank gasTank) == false)
            {
                return;
            }

            gasTank.Refuel(_fuelAmount);
            PickedUp?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
