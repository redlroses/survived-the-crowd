using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Wrapper
{
    public class RankView : MonoBehaviour
    {
        [SerializeField] private TextSetter _rank;
        [SerializeField] private TextSetter _name;
        [SerializeField] private TextSetter _score;
        [SerializeField] private Image _avatar;
        [SerializeField] private Image _flag;

        public void Set(RanksData ranksData)
        {
            _rank.Set(ranksData.Rank);
            _name.Set(ranksData.Name);
            _score.Set(ranksData.Score);
            _avatar.sprite = ranksData.Avatar;
            _flag.sprite = ranksData.Flag;
        }
    }
}
