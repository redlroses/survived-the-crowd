using Import.Joystick.Scripts;
using UnityEngine;
using UnityEngine.UI;
using DeviceType = Agava.YandexGames.DeviceType;

namespace Sources.Ui
{
    public class PlayModEnter : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _desktop;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private GameObject _mobile;

        private GameObject _current;

        private void Awake()
        {
#if !UNITY_EDITOR
            DeviceType deviceType = Agava.YandexGames.Device.Type;
#endif

#if UNITY_EDITOR
            DeviceType deviceType = DeviceType.Desktop;
#endif
            bool isMobile = deviceType != DeviceType.Desktop;
            _current = isMobile ? _mobile : _desktop;
            _button.interactable = isMobile;
            _joystick.gameObject.SetActive(isMobile);
        }

        private void OnEnable()
        {
            _current.SetActive(true);
        }

        private void OnDisable()
        {
            _current.SetActive(false);
        }
    }
}