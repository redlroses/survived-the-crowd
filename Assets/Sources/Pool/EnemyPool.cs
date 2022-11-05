using Sources.Enemy;
using UnityEngine;

namespace Pool
{
    public sealed class EnemyPool : ObjectPool<Enemy>
    {
        [SerializeField] private Transform _targetFollow;
        protected override void InitCopy(Enemy copy)
        {
            copy.GetComponent<EnemyMover>().Init(_targetFollow);
        }
    }
}
