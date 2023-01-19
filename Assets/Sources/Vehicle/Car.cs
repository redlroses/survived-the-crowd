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
        [SerializeField] private CarId _id;
        [SerializeField] private CarStaticData _carStaticData;
        [SerializeField] private Engine _engine;
        [SerializeField] private GasTank _gasTank;
        [SerializeField] private Rudder _rudder;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private Transform _weaponPivot;

        public Engine Engine => _engine;
        public GasTank GasTank => _gasTank;
        public Rudder Rudder => _rudder;
        public CarId Id => _id;
        public Transform WeaponPivot => _weaponPivot;
        public IHealth Health => _playerHealth;

        private void Awake()
        {
            SetComponents();
            ConstructData();
        }

        private void ConstructData()
        {
            _engine.Construct(_carStaticData);
            _gasTank.Construct(_carStaticData);
            _rudder.Construct(_carStaticData);
            _playerHealth.Construct(_carStaticData);
        }

        private void SetComponents()
        {
            _engine ??= GetComponent<Engine>();
            _gasTank ??= GetComponent<GasTank>();
            _rudder ??= GetComponent<Rudder>();
        }
    }
}