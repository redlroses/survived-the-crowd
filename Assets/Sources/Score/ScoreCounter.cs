using System.Diagnostics.CodeAnalysis;
using Sources.Pool;
using Sources.Timer;
using UnityEngine;

namespace Sources.Score
{
    [SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
    public sealed class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;

        [Space] [Header("Settings")]
        [SerializeField] private int _pointsPerEnemy;
        [SerializeField] private int _pointsPerSecond;
        [Header("Components")]
        [SerializeField] private TimerView _timer;

        public int CalculateScore()
            => Mathf.RoundToInt(
                (_timer.GetTime() * _pointsPerSecond) + (_enemyPool.DeadEnemiesAmount * _pointsPerEnemy));
    }
}