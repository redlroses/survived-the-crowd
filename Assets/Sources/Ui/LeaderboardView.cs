using System;
using System.Collections.Generic;
using Sources.LeaderBoard;
using Sources.Player.Factory;
using Sources.Ui.Animations;
using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Ui
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private int _leaderboardCompetingPlayersCount;
        [SerializeField] private string _leaderboardName;
        [SerializeField] private int _leaderboardTopPlayersCount;
        [SerializeField] private LoadingAnimation _loadingAnimation;
        [SerializeField] private Transform _ranksViewContainer;
        private bool _isLeaderboardDataReceived;
        private ILeaderBoard _leaderBoard;

        private List<RankView> _ranksView;

        private void Awake()
        {
            FindRanksView();
        }

        private void Start()
        {
#if UNITY_EDITOR
            _leaderBoard = new EditorLeaderBoard();
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
            _leaderBoard =
                new YandexLeaderBoard(_leaderboardName, _leaderboardTopPlayersCount, _leaderboardCompetingPlayersCount);
#endif
        }

        public async void ShowLeaderBoard()
        {
            _loadingAnimation.Play();
            RanksData[] ranksData = await _leaderBoard.GetLeaderboardEntries();
            _loadingAnimation.Stop();
            int ranksCount = Math.Min(_ranksViewContainer.childCount, ranksData.Length);

            for (int i = 0; i < ranksCount; i++)
            {
                _ranksView[i].Set(ranksData[i]);
                _ranksView[i].gameObject.SetActive(true);
            }
        }

        public void HideLeaderBoard()
        {
            foreach (RankView rankView in _ranksView)
            {
                rankView.gameObject.SetActive(false);
            }
        }

        public void SetScore(int score, CarId avatarName)
        {
            _leaderBoard.SetScore(score, avatarName.ToString());
        }

        public void OnLogIn()
        {
            _leaderBoard.TryAuthorize();
        }

        private void FindRanksView()
        {
            _ranksView = new List<RankView>(_ranksViewContainer.childCount);
            _ranksView.AddRange(_ranksViewContainer.GetComponentsInChildren<RankView>());

            foreach (RankView rank in _ranksView)
            {
                rank.gameObject.SetActive(false);
            }
        }
    }
}