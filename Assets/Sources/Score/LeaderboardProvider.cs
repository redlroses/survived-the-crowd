using Sources.Level;
using Sources.Player.Factory;
using Sources.Ui;
using UnityEngine;

namespace Sources.Score
{
    public class LeaderboardProvider : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private PlayerFactory _playerFactory;

        private void OnEnable()
        {
            _loseDetector.Lose += OnLose;
        }

        private void OnDisable()
        {
            _loseDetector.Lose -= OnLose;
        }

        private void OnLose()
        {
            _leaderboardView.SetScore(_scoreCounter.CalculateScore(), _playerFactory.CurrentCar);
        }
    }
}
