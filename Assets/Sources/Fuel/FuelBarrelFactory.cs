using System;
using System.Collections;
using System.Linq;
using Pool;
using UnityEngine;
using Extensions;
using Sources.Fuel;

namespace Fuel
{
    [RequireComponent(typeof(FuelBarrelPool))]
    public sealed class FuelBarrelFactory : MonoBehaviour
    {
        [SerializeField] private bool _isSpawning;
        [SerializeField] private float _spawnDelay = 30f;
        [SerializeField] private FuelBarrelPool _pool;
        [SerializeField] private FuelSpawnPoint[] _spawnPoints;

        private WaitForSeconds _waitForSpawnDelay;

        private void Awake()
        {
            _pool ??= GetComponent<FuelBarrelPool>();
        }

        private void Start()
        {
            _waitForSpawnDelay = new WaitForSeconds(_spawnDelay);
            StartSpawning();
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
            StartCoroutine(Spawning());
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
