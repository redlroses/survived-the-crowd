using UnityEngine;

namespace Sources.Fuel
{
    public sealed class FuelSpawnPoint : MonoBehaviour
    {
        private FuelBarrel _fuelBarrel;

        [field: SerializeField] public SpawnPointState State { get; private set; }

        public void SetFuelBarrel(FuelBarrel fuelBarrel)
        {
            _fuelBarrel = fuelBarrel;
            _fuelBarrel.Picked += OnFuelBarrelPicked;
            State = SpawnPointState.Busy;
        }

        public void Clear()
        {
            if (_fuelBarrel == null)
            {
                return;
            }

            OnFuelBarrelPicked();
        }

        private void OnFuelBarrelPicked()
        {
            _fuelBarrel.Picked -= OnFuelBarrelPicked;
            _fuelBarrel.Disable();
            _fuelBarrel = null;
            State = SpawnPointState.Empty;
        }
    }
}