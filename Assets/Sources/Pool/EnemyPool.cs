using Sources.Enemy;

namespace Sources.Pool
{
    public sealed class EnemyPool : ObjectPool<Enemy.Enemy>
    {
        public void Init(IAttackable attackable)
        {
            FillPool();

            foreach (Enemy.Enemy copy in GetReadOnlyCopies())
            {
                copy.GetComponent<AgentToTargetMover>().ApplyTarget(attackable);
                copy.GetComponent<AgentPatternSwitcher>().Init(attackable);
                copy.GetComponent<AgentAttackRangeTracker>().Init(attackable);
            }
        }
    }
}
