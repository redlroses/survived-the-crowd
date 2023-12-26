using Sources.Level;
using Sources.Player.Factory;
using Sources.Ui;
using UnityEngine;

namespace Sources.Score
{
    public class LeaderboardProvider : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private PlayerFactory _playerFactory;
        [SerializeField] private ScoreCounter _scoreCounter;

        private void OnEnable()
        {
            _loseDetector.Losed += OnLosed;
        }

        private void OnDisable()
        {
            _loseDetector.Losed -= OnLosed;
            OnLosed();
        }

        private void OnLosed()
        {
            _leaderboardView.SetScore(_scoreCounter.CalculateScore(), _playerFactory.CurrentCar);
        }
    }
}