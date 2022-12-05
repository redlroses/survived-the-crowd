using Sources.Enemy;
using UnityEngine;

namespace Sources.Pool
{
    public sealed class EnemyPool : ObjectPool<Enemy.Enemy>
    {
        [SerializeField] [RequireInterface(typeof(IAttackable))] private MonoBehaviour _targetFollow;
        protected override void InitCopy(Enemy.Enemy copy)
        {
            IAttackable targetAsIAttackable = (IAttackable) _targetFollow;
            copy.GetComponent<AgentToTargetMover>().ApplyTarget(targetAsIAttackable);
            copy.GetComponent<AgentPatternSwitcher>().Init(targetAsIAttackable);
            copy.GetComponent<AgentAttackRangeTracker>().Init(targetAsIAttackable);
        }
    }
}
