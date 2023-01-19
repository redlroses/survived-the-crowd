using Sources.Enemy;
using Sources.HealthLogic;

namespace Sources.Pool
{
    public sealed class EnemyPool : ObjectPool<Enemy.Enemy>
    {
        private IAttackable _attackable;
        private int _deadEnemiesAmount;

        public int DeadEnemiesAmount => _deadEnemiesAmount;

        private void OnDisable()
        {
            foreach (var copy in GetActiveObjects())
            {
                copy.GetComponentInChildren<IEnemyAnimator>().DeathAnimationEnded -= OnEnemyDead;
            }
        }

        protected override void InitCopy(Enemy.Enemy copy)
        {
            copy.GetComponent<AgentToTargetMover>().ApplyTarget(_attackable);
            copy.GetComponent<AgentPatternSwitcher>().Init(_attackable);
            copy.GetComponent<AgentAttackRangeTracker>().Init(_attackable);

            copy.GetComponentInChildren<IHealth>().Empty += OnEnemyDead;
        }

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

        public void ResetDeadEnemyCounter()
        {
            _deadEnemiesAmount = 0;
        }

        private void OnEnemyDead()
        {
            _deadEnemiesAmount++;
        }
    }
}
