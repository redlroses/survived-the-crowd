using System.Collections.Generic;
using System.Linq;
using Sources.Pool;
using UnityEngine;

namespace Sources.Enemy
{
    [RequireComponent(typeof(EnemyPool))]
    public sealed class EnemyFactory : MonoBehaviour
    {
        private readonly List<Enemy> _aliveEnemies = new List<Enemy>();

        [SerializeField] private EnemyPool _pool;
        [SerializeField] private int _enemiesPerLevel;
        [SerializeField] private Transform _spawnPoint;

        private void Awake()
        {
            _pool ??= GetComponent<EnemyPool>();
        }

        public void Run()
        {
            Spawn();
        }

        public void KillAll()
        {
            foreach (var enemy in _aliveEnemies.Where(enemy => enemy.enabled))
            {
                enemy.Health.Damage(enemy.Health.Max);
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < _enemiesPerLevel; i++)
            {
                Enemy spawnedEnemy = _pool.Enable(_spawnPoint.position);

                if (_aliveEnemies.Contains(spawnedEnemy))
                {
                    continue;
                }

                _aliveEnemies.Add(spawnedEnemy);
            }
        }
    }
}