using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Graphic _enabledSprite;
        [SerializeField] private Graphic _disabledSprite;

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