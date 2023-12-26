using Sources.Pool;
using Sources.Projectiles;
using UnityEngine;

namespace Sources
{
    public class FxSpawner : MonoBehaviour
    {
        [SerializeField] private FxPool _pool;

        public void Subscribe(Projectile projectile)
        {
            projectile.Disabled += OnFxSpawn;
            IPoolable<Projectile> poolable = projectile;
            poolable.Destroyed += UnSubscribe;
        }

        private void UnSubscribe(Projectile projectile)
        {
            projectile.Disabled -= OnFxSpawn;
            IPoolable<Projectile> poolable = projectile;
            poolable.Destroyed -= UnSubscribe;
        }

        private void OnFxSpawn(Projectile projectile)
        {
            _pool.Enable(projectile.transform.position);
        }
    }
}