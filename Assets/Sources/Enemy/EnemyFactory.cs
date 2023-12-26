using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Pool;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Enemy
{
    [RequireComponent(typeof(EnemyPool))]
    public sealed class EnemyFactory : MonoBehaviour
    {
        private readonly int _maxEnemies = 30;

        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField] private int _enemiesPerLevelStarted;
        [SerializeField] private int _enemiesPerSpawnTick;
        [SerializeField] private bool _isSpawning;
        [SerializeField] private EnemyPool _pool;
        [SerializeField] private float _spawnRate;

        private Coroutine _spawning;
        private WaitForSeconds _waitForSpawnDelay;

        private void Awake()
        {
            _pool ??= GetComponent<EnemyPool>();
            _waitForSpawnDelay = new WaitForSeconds(_spawnRate);
        }

        public void Run()
        {
            _isSpawning = true;
            SpawnOnLevelStarted();
            _spawning ??= StartCoroutine(Spawn());
        }

        public void Stop()
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
            foreach (Enemy enemy in _pool.GetActiveObjects())
            {
                enemy.Health.Damage(enemy.Health.Max);
            }
        }

        private void SpawnOnLevelStarted()
        {
            for (int i = 0; i < _enemiesPerLevelStarted; i++)
            {
                SpawnEnemy(GetSpawnPoint());
            }
        }

        private void SpawnEnemy(Vector3 at)
        {
            if (_pool.GetActiveObjects().Count() > _maxEnemies)
            {
                return;
            }

            _pool.Enable(at);
        }

        private IEnumerator Spawn()
        {
            while (_isSpawning)
            {
                Vector3 spawnPoint = GetSpawnPoint();

                for (int i = 0; i < _enemiesPerSpawnTick; i++)
                {
                    SpawnEnemy(spawnPoint);
                }

                yield return _waitForSpawnDelay;
            }
        }

        private Vector3 GetSpawnPoint()
            => _spawnPoints.GetRandom().position;
    }
}