using System.Collections;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Collectables
{
    [RequireComponent(typeof(CarDetailsPool))]
    public class CarDetailsFactory : MonoBehaviour
    {
        [SerializeField] private bool _isSpawning;
        [SerializeField] private CarDetailsPool _pool;
        [SerializeField] private float _spawnRate = 30f;
        [SerializeField] private CarDetailsSpawnZone[] _spawnZones;

        private WaitForSeconds _waitForSpawnDelay;
        private Coroutine _spawning;

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
            foreach (var carDetails in _pool.GetActiveObjects())
            {
                carDetails.Disable();
            }
        }

        private IEnumerator Spawn()
        {
            while (_isSpawning)
            {
                Vector3 spawnPoint = _spawnZones.GetRandom().GetRandomPosition();

                CarDetails carDetails = _pool.Enable(spawnPoint);

                yield return _waitForSpawnDelay;
            }
        }
    }
}
