using System.Collections.Generic;
using System.Threading.Tasks;
using Agava.YandexGames;
using Sources.Ui;

namespace Sources.LeaderBoard
{
    public class YandexLeaderBoard : ILeaderBoard
    {
        private const string Anonymous = "Anonymous";

        private readonly int _competingPlayersCount;
        private readonly string _name;
        private readonly int _topPlayersCount;

        private string _cashedAvatar;
        private int _cashedScore;
        private bool _isLeaderboardDataReceived;

        private List<RanksData> _ranksData;

        public YandexLeaderBoard(string name, int topPlayersCount, int competingPlayersCount, bool isIncludeSelf = true)
        {
            _name = name;
            _topPlayersCount = topPlayersCount;
            _competingPlayersCount = competingPlayersCount;
        }

        public bool IsReady => PlayerAccount.IsAuthorized;

        public async Task<RanksData[]> GetLeaderboardEntries()
        {
            _isLeaderboardDataReceived = false;

            Leaderboard.GetEntries(
                _name,
                OnGetLeaderBoardEntries,
                null,
                _topPlayersCount,
                _competingPlayersCount,
                PlayerAccount.IsAuthorized);

            while (_isLeaderboardDataReceived == false)
            {
                await Task.Yield();
            }

            return _ranksData.ToArray();
        }

        public void SetScore(int score, string avatarName)
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                _cashedScore = score;
                _cashedAvatar = avatarName;

                return;
            }

            TryGetPersonalData();

            Leaderboard.GetPlayerEntry(
                _name,
                result =>
                {
                    if (result.score >= score)
                    {
                        return;
                    }

                    Leaderboard.SetScore(_name, score, null, null, avatarName);
                });
        }

        public void TryAuthorize()
        {
            if (PlayerAccount.IsAuthorized)
            {
                TryGetPersonalData();

                return;
            }

            PlayerAccount.Authorize(TryGetPersonalData);
        }

        private void TryGetPersonalData()
        {
            if (PlayerAccount.HasPersonalProfileDataPermission == false)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
            }
        }

        private void OnGetLeaderBoardEntries(LeaderboardGetEntriesResponse board)
        {
            _ranksData = new List<RanksData>(board.entries.Length);

            foreach (LeaderboardEntryResponse entry in board.entries)
            {
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                {
                    name = Anonymous;
                }

                int rank = entry.rank;
                int score = entry.score;
                string lang = entry.player.lang;
                string avatar = entry.extraData;

                _ranksData.Add(RanksDataConverter.FromYandex(name, rank, score, lang, avatar));
            }

            _isLeaderboardDataReceived = true;
        }
    }
}