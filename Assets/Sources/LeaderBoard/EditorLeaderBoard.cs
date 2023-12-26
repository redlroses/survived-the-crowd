using System.Collections.Generic;
using System.Threading.Tasks;
using Sources.Tools.Extensions;
using Sources.Ui;

namespace Sources.LeaderBoard
{
    internal class EditorLeaderBoard : ILeaderBoard
    {
        private bool _isLeaderboardDataReceived;
        private List<RanksData> _ranksData;

        public async Task<RanksData[]> GetLeaderboardEntries()
        {
            _isLeaderboardDataReceived = false;
            List<RanksData> ranks = new List<RanksData>();
            string[] langs = { "ru", "en", "tr" };
            string[] avatars = { "HotRod", "Buggy", "Muscle" };

            ApplyTestData(langs, avatars, ranks);

            while (_isLeaderboardDataReceived == false)
            {
                await Task.Yield();
            }

            return ranks.ToArray();
        }

        public void SetScore(int score, string avatarName)
        {
        }

        public void TryAuthorize()
        {
        }

        private async void ApplyTestData(string[] langs, string[] avatars, List<RanksData> ranks)
        {
            int count = 10;
            int millisecondsDelay = 300;

            for (int i = 0; i < count; i++)
            {
                RanksData data = RanksDataConverter.FromYandex(
                    $"name {i}",
                    i,
                    i * count,
                    langs.GetRandom(),
                    avatars.GetRandom());

                ranks.Add(data);

                await Task.Delay(millisecondsDelay);
            }

            _isLeaderboardDataReceived = true;
        }
    }
}