using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextSetter _textSetter;
        [SerializeField] private ScoreCounter _scoreCounter;

        private void OnEnable()
        {
            _textSetter.Set(_scoreCounter.CalculateScore().ToString());
        }
    }
}