using Sources.HealthLogic;
using Sources.Player;
using Sources.Player.Factory;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Vehicle
{
    [RequireComponent(typeof(Rudder))]
    [RequireComponent(typeof(GasTank))]
    [RequireComponent(typeof(Engine))]
    public sealed class Car : MonoBehaviour
    {
        [SerializeField] private CarStaticData _carStaticData;
        [SerializeField] private PlayerHealth _playerHealth;

        [field: SerializeField]
        public Engine Engine { get; private set; }

        [field: SerializeField]
        public GasTank GasTank { get; private set; }

        [field: SerializeField]
        public Rudder Rudder { get; private set; }

        [field: SerializeField]
        public CarId Id { get; }

        [field: SerializeField]
        public Transform WeaponPivot { get; }

        public IHealth Health => _playerHealth;

        private void Awake()
        {
            SetComponents();
            ConstructData();
        }

        private void ConstructData()
        {
            Engine.Construct(_carStaticData);
            GasTank.Construct(_carStaticData);
            Rudder.Construct(_carStaticData);
            _playerHealth.Construct(_carStaticData);
        }

        private void SetComponents()
        {
            Engine ??= GetComponent<Engine>();
            GasTank ??= GetComponent<GasTank>();
            Rudder ??= GetComponent<Rudder>();
        }
    }
}