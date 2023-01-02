using Sources.Pool;
using Sources.Timer;
using UnityEngine;

namespace Sources.Score
{
    public sealed class ScoreCounter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TimerView _timer;
        [SerializeField] private EnemyPool _enemyPool;

        [Space] [Header("Settings")]
        [SerializeField] private int _pointsPerEnemy;
        [SerializeField] private int _pointsPerSecond;

        public int CalculateScore()
            => Mathf.RoundToInt(
                _timer.GetTime() * _pointsPerSecond + _enemyPool.DeadEnemiesAmount * _pointsPerEnemy);
    }
}
