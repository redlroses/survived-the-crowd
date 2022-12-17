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
        private readonly List<Enemy> _aliveEnemies = new List<Enemy>();

        [SerializeField] private EnemyPool _pool;
        [SerializeField] private int _enemiesPerSpawnTick;
        [SerializeField] private float _spawnRate;
        [SerializeField] private int _enemiesPerLevelStarted;
        [SerializeField] private int _maxEnemies = 30;
        [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
        [SerializeField] private bool _isSpawning;

        private WaitForSeconds _waitForSpawnDelay;
        private Coroutine _spawning;

        private void Awake()
        {
            _pool ??= GetComponent<EnemyPool>();
            _waitForSpawnDelay = new WaitForSeconds(_spawnRate);
        }

        public void Run()
        {
            _isSpawning = true;
            SpawnOnLevelStarted();
            StartCoroutine(Spawn());
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
            foreach (var enemy in _aliveEnemies.Where(enemy => enemy.enabled))
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
            if (_aliveEnemies.Count(enemy => enemy.enabled) > _maxEnemies)
            {
                return;
            }

            Enemy spawnedEnemy = _pool.Enable(at);

            if (_aliveEnemies.Contains(spawnedEnemy))
            {
                return;
            }

            _aliveEnemies.Add(spawnedEnemy);
        }

        private IEnumerator Spawn()
        {
            while (_isSpawning)
            {
                var spawnPoint = GetSpawnPoint();

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