using Sources.Projectiles;
using UnityEngine;

namespace Sources.Pool
{
    public sealed class ProjectilePool : ObjectPool<Projectile>
    {
        [Header("Projectile Settings")]
        [SerializeField] private Collider _ownerHurtBox;

        private int _projectileDamage;
        private bool _isOwnerHurtBoxNull;

        private void Start()
        {
            _isOwnerHurtBoxNull = _ownerHurtBox == null;
        }

        public void Construct(int projectileDamage)
        {
            _projectileDamage = projectileDamage;
            FillPool();
        }

        protected override void InitCopy(Projectile copy)
        {
            copy.Construct(_projectileDamage);

            if (_isOwnerHurtBoxNull)
            {
                return;
            }

            if (copy.TryGetComponent(out Collider bulletCollider) == false)
            {
                Physics.IgnoreCollision(bulletCollider, _ownerHurtBox, true);
            }
        }
    }
}
