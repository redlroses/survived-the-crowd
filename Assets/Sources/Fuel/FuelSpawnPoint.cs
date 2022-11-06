using Sources.Fuel;
using UnityEngine;

namespace Fuel
{
    public sealed class FuelSpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPointState _state;

        private FuelBarrel _fuelBarrel;

        public SpawnPointState State => _state;

        public void SetFuelBarrel(FuelBarrel fuelBarrel)
        {
            _fuelBarrel = fuelBarrel;
            _fuelBarrel.Destroyed += FuelBarrelRaised;
            _state = SpawnPointState.Busy;
        }

        private void FuelBarrelRaised(FuelBarrel fuelBarrel)
        {
            _fuelBarrel.Destroyed -= FuelBarrelRaised;
            _state = SpawnPointState.Empty;
        }
    }
}
