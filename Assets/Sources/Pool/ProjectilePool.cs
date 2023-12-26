using Sources.Projectiles;
using UnityEngine;

namespace Sources.Pool
{
    public sealed class ProjectilePool : ObjectPool<Projectile>
    {
        [SerializeField] private FxSpawner _fxSpawner;

        [Header("Projectile Settings")]
        [SerializeField] private Collider _ownerHurtBox;
        private bool _isOwnerHurtBoxNull;

        private int _projectileDamage;

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
            _fxSpawner.Subscribe(copy);
            copy.Construct(_projectileDamage);

            if (_isOwnerHurtBoxNull)
            {
                return;
            }

            if (_ownerHurtBox is null)
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