using Agava.YandexGames;
using Sources.Audio;
using UnityEngine;
using DeviceType = Agava.YandexGames.DeviceType;

namespace Sources.Input
{
    public class InputProvider : MonoBehaviour
    {
        [SerializeField] private AudioPlayer _audioPlayer;
        [SerializeField] private bool _isKeyboard;
        [SerializeField] private PlayerKeyboardInput _keyboardInput;
        [SerializeField] private PlayerInput _mobileInput;

        public IInput Input { get; private set; }

        private void Awake()
        {
#if !UNITY_EDITOR
            ChooseInput();
#endif

#if UNITY_EDITOR
            if (_isKeyboard)
            {
                SetInput(_keyboardInput);
            }
            else
            {
                SetInput(_mobileInput);
            }

            _audioPlayer.Construct((IAudioPlayable)Input);
#endif
        }

        private void ChooseInput()
        {
            switch (Device.Type)
            {
                case DeviceType.Desktop:
                    SetInput(_keyboardInput);

                    break;
                case DeviceType.Mobile:
                    SetInput(_mobileInput);

                    break;
                default:
                    SetInput(_mobileInput);

                    break;
            }

            _audioPlayer.Construct((IAudioPlayable)Input);
        }

        private void SetInput(MonoBehaviour input)
        {
            input.enabled = true;
            Input = (IInput)input;
        }
    }
}