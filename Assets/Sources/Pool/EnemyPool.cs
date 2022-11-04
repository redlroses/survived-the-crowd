using UnityEngine;

namespace Pool
{
    public sealed class EnemyPool : ObjectPool<BaseEnemy>
    {
        [SerializeField] private Transform _targetFollow;
        protected override void InitCopy(BaseEnemy copy)
        {
            copy.GetComponent<EnemyMover>().Init(_targetFollow);
        }
    }
}
