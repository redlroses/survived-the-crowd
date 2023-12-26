using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Wrapper.Screens
{
    public class LeaderboardScreen : Screen
    {
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private Button _logInButton;

        protected override void OnShow()
        {
            _logInButton.gameObject.SetActive(true);
            _leaderboardView.ShowLeaderBoard();
        }

        protected override void OnHide()
        {
            _leaderboardView.HideLeaderBoard();
        }
    }
}