using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Pool;
using Sources.Tools;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Enemy
{
    [RequireComponent(typeof(EnemyPool))]
    public sealed class EnemyFactory : MonoBehaviour
    {
        private readonly List<Enemy> _aliveEnemies = new List<Enemy>();

        [SerializeField] private EnemyPool _pool;
        [SerializeField] private int _enemiesPerSpawnTick;
        [SerializeField] private float _spawnRate;
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField] private bool _isSpawning;

        private WaitForSeconds _waitForSpawn;
        private Coroutine _spawning;

        private void Awake()
        {
            _pool ??= GetComponent<EnemyPool>();
            _waitForSpawn = new WaitForSeconds(_spawnRate);
        }

        public void Run()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            _isSpawning = true;
            StartCoroutine(Spawn());
        }

        private void StopSpawn()
        {
            if (_spawning == null)
            {
                return;
            }

            _isSpawning = false;
            StopCoroutine(_spawning);
            _spawning = null;
        }

        public void KillAll()
        {
            foreach (var enemy in _aliveEnemies.Where(enemy => enemy.enabled))
            {
                enemy.Health.Damage(enemy.Health.Max);
            }
        }

        private IEnumerator Spawn()
        {
            while (_isSpawning)
            {
                var spawnPoint = GetSpawnPoint();

                for (int i = 0; i < _enemiesPerSpawnTick; i++)
                {
                    Enemy spawnedEnemy = _pool.Enable(spawnPoint);

                    if (_aliveEnemies.Contains(spawnedEnemy))
                    {
                        continue;
                    }

                    _aliveEnemies.Add(spawnedEnemy);
                }

                yield return _waitForSpawn;
            }
        }

        private Vector3 GetSpawnPoint()
            => _spawnPoints.GetRandom().position;
    }
}