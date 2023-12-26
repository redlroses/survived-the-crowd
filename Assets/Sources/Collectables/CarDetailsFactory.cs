using System.Collections;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Collectables
{
    [RequireComponent(typeof(CarDetailsPool))]
    public class CarDetailsFactory : MonoBehaviour
    {
        private readonly float _spawnRate = 30f;

        [SerializeField] private bool _isSpawning;
        [SerializeField] private CarDetailsPool _pool;
        [SerializeField] private CarDetailsSpawnZone[] _spawnZones;

        private Coroutine _spawning;
        private WaitForSeconds _waitForSpawnDelay;

        private void Awake()
        {
            _pool ??= GetComponent<CarDetailsPool>();
            _waitForSpawnDelay = new WaitForSeconds(_spawnRate);
        }

        public void Run()
        {
            _isSpawning = true;
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

        public void DisableAll()
        {
            foreach (CarDetails carDetails in _pool.GetActiveObjects())
            {
                carDetails.Disable();
            }
        }

        private IEnumerator Spawn()
        {
            yield return _waitForSpawnDelay;

            while (_isSpawning)
            {
                Vector3 spawnPoint = _spawnZones.GetRandom().GetRandomPosition();
                _pool.Enable(spawnPoint);

                yield return _waitForSpawnDelay;
            }
        }
    }
}