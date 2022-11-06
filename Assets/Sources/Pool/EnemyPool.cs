using Pool;
using Sources.Custom;
using Sources.Enemy;
using UnityEngine;

namespace Sources.Pool
{
    public sealed class EnemyPool : ObjectPool<Enemy.Enemy>
    {
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _targetFollow;
        protected override void InitCopy(Enemy.Enemy copy)
        {
            copy.GetComponent<AgentToTargetMover>().ApplyTarget(_targetFollow as IAttackable);
        }
    }
}
