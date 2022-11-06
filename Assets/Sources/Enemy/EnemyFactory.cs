using Sources.Creatures.Player;
using Sources.Pool;
using UnityEngine;

namespace Sources.Enemy
{
    [RequireComponent(typeof(EnemyPool))]
    public sealed class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private EnemyPool _pool;
        [SerializeField] private int _enemiesPerLevel;
        [SerializeField] private Transform _spawnPoint;

        private void Awake()
        {
            _pool ??= GetComponent<EnemyPool>();
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < _enemiesPerLevel; i++)
            {
                _pool.Enable(_spawnPoint.position);
            }
        }
    }
}
