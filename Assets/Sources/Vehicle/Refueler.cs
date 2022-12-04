using Sources.Fuel;
using UnityEngine;

namespace Sources.Vehicle
{
    public sealed class Refueler : MonoBehaviour
    {
        [SerializeField] private GasTank _gasTank;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FuelBarrel barrel) == false)
            {
                return;
            }

            _gasTank.Refuel(barrel.PickUp());
        }
    }
}