using Sources.Enemy;

namespace Sources.Pool
{
    public sealed class EnemyPool : ObjectPool<Enemy.Enemy>
    {
        private IAttackable _attackable;

        public void Init(IAttackable attackable)
        {
            _attackable = attackable;

            FillPool();

            foreach (Enemy.Enemy copy in GetReadOnlyCopies())
            {
                copy.GetComponent<AgentToTargetMover>().ApplyTarget(_attackable);
                copy.GetComponent<AgentPatternSwitcher>().Init(_attackable);
                copy.GetComponent<AgentAttackRangeTracker>().Init(_attackable);
            }
        }

        protected override void InitCopy(Enemy.Enemy copy)
        {
            copy.GetComponent<AgentToTargetMover>().ApplyTarget(_attackable);
            copy.GetComponent<AgentPatternSwitcher>().Init(_attackable);
            copy.GetComponent<AgentAttackRangeTracker>().Init(_attackable);
        }
    }
}
