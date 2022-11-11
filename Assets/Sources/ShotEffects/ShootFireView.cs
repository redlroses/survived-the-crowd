using Sources.Custom;
using Sources.Turret;
using UnityEngine;

namespace Sources.ShotEffects
{
    [RequireComponent(typeof(Shooter))]
    public sealed class ShootFireView : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IShotMaker))] private MonoBehaviour _shooter;
        [SerializeField] private ParticleSystem _shootEffects;

        private IShotMaker Shooter => (IShotMaker) _shooter;

        private void OnEnable()
        {
            Shooter.ShotOff += OnShotOff;
        }

        private void OnDisable()
        {
            Shooter.ShotOff += OnShotOff;
        }

        private void OnShotOff()
        {
            _shootEffects.Play();
        }
    }
}