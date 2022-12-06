using UnityEngine;

namespace Sources.Ui.Wrapper.Screens
{
    public class LoseScreen : Screen
    {
        [SerializeField] private TextSetter _textSetter;

        public void SetLoseCause(string text)
        {
            _textSetter.Set(text);
        }
    }
}