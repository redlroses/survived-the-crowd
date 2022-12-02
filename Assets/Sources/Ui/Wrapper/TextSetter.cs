using TMPro;
using UnityEngine;

namespace Sources.Ui.Wrapper
{
    public sealed class TextSetter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Set(string text)
        {
            _text.text = text;
        }

        public void Set(int text)
        {
            _text.text = text.ToString();
        }
    }
}