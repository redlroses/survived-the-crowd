using UnityEngine;

namespace Sources.Turret
{
    public sealed class Weapon : MonoBehaviour
    {
        [SerializeField] private TargetSeeker _targetSeeker;

        public TargetSeeker TargetSeeker => _targetSeeker;
    }
}
