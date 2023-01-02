using Sources.Score;
using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class LoseScreen : Screen
    {
        [SerializeField] private TextSetter _causeText;
        [SerializeField] private ScoreView _scoreView;

        public void SetLoseCause(string text)
        {
            _causeText.Set(text);
        }

        protected override void OnShow()
        {
            _scoreView.enabled = true;
        }

        protected override void OnHide()
        {
            _scoreView.enabled = false;
        }
    }
}