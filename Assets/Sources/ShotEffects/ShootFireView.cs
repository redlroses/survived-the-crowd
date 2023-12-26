using Sources.Turret;
using UnityEngine;

namespace Sources.ShotEffects
{
    [RequireComponent(typeof(Shooter))]
    public sealed class ShootFireView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _shootEffects;
        [SerializeField] [RequireInterface(typeof(IShotMaker))] private MonoBehaviour _shooter;

        private IShotMaker Shooter => _shooter as IShotMaker;

        private void OnEnable()
        {
            Shooter.Shooting += OnShooting;
        }

        private void OnDisable()
        {
            Shooter.Shooting += OnShooting;
        }

        private void OnShooting()
        {
            _shootEffects.Play();
        }
    }
}