using UnityEngine;

namespace Sources.Ui
{
    public class RanksData
    {
        private readonly int _rank;
        private readonly int _score;
        private readonly Sprite _avatar;
        private readonly Sprite _flag;
        private readonly string _name;

        public Sprite Flag => _flag;
        public int Rank => _rank;
        public int Score => _score;
        public Sprite Avatar => _avatar;
        public string Name => _name;

        public RanksData(int rank, int score, Sprite avatar, string name, Sprite flag)
        {
            _rank = rank;
            _score = score;
            _avatar = avatar;
            _name = name;
            _flag = flag;
        }
    }
}