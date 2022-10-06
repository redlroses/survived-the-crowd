using UnityEngine;

namespace Projectiles
{
    public sealed class Bullet : Projectile
    {
        protected override void Move(float moveSpeed)
        {
            transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }
    }
}
