using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Pool;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Fuel
{
    [RequireComponent(typeof(FuelBarrelPool))]
    public sealed class FuelBarrelFactory : MonoBehaviour
    {
        private readonly float _spawnDelay = 30f;

        [SerializeField] private bool _isSpawning;
        [SerializeField] private FuelBarrelPool _pool;
        [SerializeField] private FuelSpawnPoint[] _spawnPoints;

        private Coroutine _spawning;
        private WaitForSeconds _waitForSpawnDelay;

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
            foreach (FuelSpawnPoint spawnPoint in _spawnPoints)
            {
                spawnPoint.Clear();
            }
        }

        private FuelSpawnPoint GetSpawnPoint()
        {
            IEnumerable<FuelSpawnPoint> emptyPoints = _spawnPoints.Where(point => point.State == SpawnPointState.Empty);
            FuelSpawnPoint randomEmptyPoint = emptyPoints.GetRandom();

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
            if (_spawning == null)
            {
                return;
            }

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