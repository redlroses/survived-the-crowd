using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class LeaderboardScreen : Screen
    {
        [SerializeField] private LeaderboardView _leaderboardView;

        protected override void OnShow()
        {
            _leaderboardView.ShowLeaderBoard();
        }

        protected override void OnHide()
        {
            _leaderboardView.HideLeaderBoard();
        }
    }
}
