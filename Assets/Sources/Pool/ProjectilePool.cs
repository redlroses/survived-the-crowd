using Pool;
using Sources.Projectiles;
using UnityEngine;

namespace Sources.Pool
{
    public sealed class ProjectilePool : ObjectPool<Projectile>
    {
        // [Header("Projectile Settings")]
        // [SerializeField] private Collider _ownerHurtBox;
        //
        // private bool _isOwnerHurtBoxNull;
        //
        // private void Start()
        // {
        //     _isOwnerHurtBoxNull = _ownerHurtBox == null;
        // }
        //
        // protected override void InitCopy(Projectile copy)
        // {
        //     if (_isOwnerHurtBoxNull)
        //     {
        //         return;
        //     }
        //
        //     if (copy.TryGetComponent(out Collider bulletCollider) == false)
        //     {
        //         Physics.IgnoreCollision(bulletCollider, _ownerHurtBox, true);
        //     }
        //
        //     Physics.IgnoreCollision(bulletCollider, _ownerHurtBox, true);
        // }
    }
}
