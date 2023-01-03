using Sources.Player.Factory;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponId _id;
        [SerializeField] private WeaponStaticData _weaponData;
        [SerializeField] private TargetSeeker _targetSeeker;
        [SerializeField] private Shooter _shooter;
        [SerializeField] [RequireInterface(typeof(IShotMaker))] private MonoBehaviour _shotMaker;

        private IShotMaker ShotMaker => (IShotMaker) _shotMaker;

        public TargetSeeker TargetSeeker => _targetSeeker;
        public WeaponId Id => _id;

        private void Awake()
        {
            ConstructData();
        }

        private void ConstructData()
        {
            _targetSeeker.Construct(_weaponData);
            _shooter.Construct(_weaponData);
            ShotMaker.Construct(_weaponData);
        }
    }
}
