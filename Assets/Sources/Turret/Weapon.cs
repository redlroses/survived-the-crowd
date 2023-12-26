using Sources.Player.Factory;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] [RequireInterface(typeof(IShotMaker))] private MonoBehaviour _shotMaker;
        [SerializeField] private WeaponStaticData _weaponData;

        [field: SerializeField]
        public TargetSeeker TargetSeeker { get; }

        [field: SerializeField]
        public WeaponId Id { get; }

        private IShotMaker ShotMaker => (IShotMaker)_shotMaker;

        private void Awake()
        {
            ConstructData();
        }

        private void ConstructData()
        {
            TargetSeeker.Construct(_weaponData);
            _shooter.Construct(_weaponData);
            ShotMaker.Construct(_weaponData);
        }
    }
}