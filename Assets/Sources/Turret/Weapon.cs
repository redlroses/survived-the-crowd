using Sources.Player.Factory;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private TargetSeeker _targetSeeker;
        [SerializeField] private WeaponId _id;

        public TargetSeeker TargetSeeker => _targetSeeker;
        public WeaponId Id => _id;
    }
}
