using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private TextSetter _textSetter;

        private void OnEnable()
        {
            _textSetter.Set(_scoreCounter.CalculateScore().ToString());
        }
    }
}