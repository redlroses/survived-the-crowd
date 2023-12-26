using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class StartScreen : Screen
    {
        [SerializeField] private PlayModEnter _playModEnter;

        protected override void OnShow()
        {
            _playModEnter.gameObject.SetActive(true);
        }

        protected override void OnHide()
        {
            _playModEnter.gameObject.SetActive(false);
        }
    }
}