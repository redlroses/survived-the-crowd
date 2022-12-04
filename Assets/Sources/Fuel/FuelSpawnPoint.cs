using UnityEngine;

namespace Sources.Fuel
{
    public sealed class FuelSpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPointState _state;

        private FuelBarrel _fuelBarrel;

        public SpawnPointState State => _state;

        public void SetFuelBarrel(FuelBarrel fuelBarrel)
        {
            _fuelBarrel = fuelBarrel;
            _fuelBarrel.PickedUp += OnFuelBarrelPickedUp;
            _state = SpawnPointState.Busy;
        }

        public void Clear()
        {
            if (_fuelBarrel == null)
            {
                return;
            }

            OnFuelBarrelPickedUp();
        }

        private void OnFuelBarrelPickedUp()
        {
            _fuelBarrel.PickedUp -= OnFuelBarrelPickedUp;
            _fuelBarrel.Disable();
            _fuelBarrel = null;
            _state = SpawnPointState.Empty;
        }
    }
}