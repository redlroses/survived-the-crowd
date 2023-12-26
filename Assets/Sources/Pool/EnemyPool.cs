using Sources.Enemy;
using Sources.HealthLogic;

namespace Sources.Pool
{
    public sealed class EnemyPool : ObjectPool<Enemy.Enemy>
    {
        private IAttackable _attackable;

        public int DeadEnemiesAmount { get; private set; }

        private void OnDisable()
        {
            foreach (Enemy.Enemy copy in GetActiveObjects())
            {
                copy.GetComponentInChildren<IEnemyAnimator>().DeathAnimationEnded -= OnEnemyDead;
            }
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
            DeadEnemiesAmount = 0;
        }

        protected override void InitCopy(Enemy.Enemy copy)
        {
            copy.GetComponent<AgentToTargetMover>().ApplyTarget(_attackable);
            copy.GetComponent<AgentPatternSwitcher>().Init(_attackable);
            copy.GetComponent<AgentAttackRangeTracker>().Init(_attackable);

            copy.GetComponentInChildren<IHealth>().Ended += OnEnemyDead;
        }

        private void OnEnemyDead()
        {
            DeadEnemiesAmount++;
        }
    }
}