using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Graphic _disabledSprite;
        [SerializeField] private Graphic _enabledSprite;
        [SerializeField] private Toggle _toggle;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(ChangeSprite);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(ChangeSprite);
        }

        private void ChangeSprite(bool isOn)
        {
            _toggle.targetGraphic.enabled = false;
            Graphic icon = isOn ? _enabledSprite : _disabledSprite;
            icon.enabled = true;
            _toggle.targetGraphic = icon;
        }
    }
}