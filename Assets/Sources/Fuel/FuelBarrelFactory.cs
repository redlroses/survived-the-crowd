using System.Collections;
using System.Linq;
using Sources.Pool;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Fuel
{
    [RequireComponent(typeof(FuelBarrelPool))]
    public sealed class FuelBarrelFactory : MonoBehaviour
    {
        [SerializeField] private bool _isSpawning;
        [SerializeField] private float _spawnDelay = 30f;
        [SerializeField] private FuelBarrelPool _pool;
        [SerializeField] private FuelSpawnPoint[] _spawnPoints;

        private WaitForSeconds _waitForSpawnDelay;
        private Coroutine _spawning;

        private void Awake()
        {
            _pool ??= GetComponent<FuelBarrelPool>();
        }

        public void Run()
        {
            _waitForSpawnDelay = new WaitForSeconds(_spawnDelay);
            StartSpawning();
        }

        public void Stop()
        {
            StopSpawning();
        }

        public void DisableAll()
        {
            foreach (var spawnPoint in _spawnPoints)
            {
                spawnPoint.Clear();
            }
        }

        private FuelSpawnPoint GetSpawnPoint()
        {
            var emptyPoints = _spawnPoints.Where(point => point.State == SpawnPointState.Empty);
            var randomEmptyPoint = emptyPoints.GetRandom();
            return randomEmptyPoint;
        }

        private bool HasEmptyPoint()
        {
            int emptyPointsCount = _spawnPoints.Count(point => point.State == SpawnPointState.Empty);
            return emptyPointsCount > 0;
        }

        [ContextMenu(nameof(StartSpawning))]
        private void StartSpawning()
        {
            _isSpawning = true;
            _spawning ??= StartCoroutine(Spawning());
        }

        private void StopSpawning()
        {
            _isSpawning = false;
            StopCoroutine(_spawning);
            _spawning = null;
        }

        private IEnumerator Spawning()
        {
            while (_isSpawning)
            {
                if (HasEmptyPoint() == false)
                {
                    yield return _waitForSpawnDelay;

                    continue;
                }

                FuelSpawnPoint spawnPoint = GetSpawnPoint();
                Vector3 spawnPosition = spawnPoint.transform.position;
                FuelBarrel fuelBarrel = _pool.Enable(spawnPosition, Quaternion.identity);
                spawnPoint.SetFuelBarrel(fuelBarrel);
                yield return _waitForSpawnDelay;
            }
        }
    }
}
