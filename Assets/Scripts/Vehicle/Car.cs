using UnityEngine;

namespace Vehicle
{
    [RequireComponent(typeof(Rudder))]
    [RequireComponent(typeof(GasTank))]
    [RequireComponent(typeof(Engine))]
    public sealed class Car : MonoBehaviour
    {
        [SerializeField] private Engine _engine;
        [SerializeField] private GasTank _gasTank;
        [SerializeField] private Rudder _rudder;

        public Engine Engine => _engine;
        public GasTank GasTank => _gasTank;
        public Rudder Rudder => _rudder;

        private void Awake()
        {
            _engine ??= GetComponent<Engine>();
            _gasTank ??= GetComponent<GasTank>();
            _rudder ??= GetComponent<Rudder>();
        }
    }
}