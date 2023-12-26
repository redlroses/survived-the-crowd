using UnityEngine;

namespace Sources.Ui
{
    public class RanksData
    {
        public RanksData(int rank, int score, Sprite avatar, string name, Sprite flag)
        {
            Rank = rank;
            Score = score;
            Avatar = avatar;
            Name = name;
            Flag = flag;
        }

        public Sprite Flag { get; }

        public int Rank { get; }

        public int Score { get; }

        public Sprite Avatar { get; }

        public string Name { get; }
    }
}