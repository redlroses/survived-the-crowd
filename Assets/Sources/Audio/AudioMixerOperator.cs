using Agava.WebUtility;
using UnityEngine;
using UnityEngine.Audio;

namespace Sources.Audio
{
    public class AudioMixerOperator : MonoBehaviour
    {
        private const float MutedVolumeLevel = -80f;

        [SerializeField] private AudioMixer _mixer;

        private float _masterLevel;
        private float _musicLevel;
        private float _soundLevel;

        private void Awake()
        {
            _mixer.GetFloat(AudioMixerGroup.Master.ToString(), out float level);
            _masterLevel = level;

            _mixer.GetFloat(AudioMixerGroup.Sound.ToString(), out level);
            _soundLevel = level;

            _mixer.GetFloat(AudioMixerGroup.Music.ToString(), out level);
            _musicLevel = level;
        }

        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;
        }

        private void OnApplicationFocus(bool pauseStatus)
        {
            Mute(!pauseStatus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Mute(pauseStatus);
        }

        public void SetSound(bool isEnable)
        {
            SetVolume(AudioMixerGroup.Sound, isEnable ? _soundLevel : MutedVolumeLevel);
        }

        public void SetMusic(bool isEnable)
        {
            SetVolume(AudioMixerGroup.Music, isEnable ? _musicLevel : MutedVolumeLevel);
        }

        public void SetMaster(bool isEnable)
        {
            SetVolume(AudioMixerGroup.Master, isEnable ? _masterLevel : MutedVolumeLevel);
        }

        private void OnInBackgroundChangeEvent(bool inBackground)
        {
            Mute(inBackground);
        }

        private void Mute(bool inBackground)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }

        private void SetVolume(AudioMixerGroup group, float volume)
        {
            _mixer.SetFloat(group.ToString(), volume);
        }
    }
}