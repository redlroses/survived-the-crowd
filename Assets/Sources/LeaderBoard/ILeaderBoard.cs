using System.Threading.Tasks;
using Sources.Ui;

namespace Sources.LeaderBoard
{
    public interface ILeaderBoard
    {
        Task<RanksData[]> GetLeaderboardEntries();

        void SetScore(int score, string avatarName);

        void TryAuthorize();
    }
}