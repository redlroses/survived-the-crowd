using UnityEngine;

namespace Vehicle
{
    [RequireComponent(typeof(GasTank))]
    [RequireComponent(typeof(Engine))]
    public sealed class Car : MonoBehaviour
    {
        [SerializeField] private Engine _engine;
        [SerializeField] private GasTank _gasTank;

        public Engine Engine => _engine;
        public GasTank GasTank => _gasTank;

        private void Awake()
        {
            _engine ??= GetComponent<Engine>();
            _gasTank ??= GetComponent<GasTank>();
        }
    }
}