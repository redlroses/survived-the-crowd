using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IAudioPlayable))] private MonoBehaviour _audioPlayable;
        [FormerlySerializedAs("_IsOverlay")] [SerializeField] private bool _isOverlay;

        private AudioSource _audioSource;

        private IAudioPlayable AudioPlayable => _audioPlayable as IAudioPlayable;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (_audioPlayable == null)
            {
                return;
            }

            AudioPlayable.AudioPlaying += OnAudioPlayed;

            if (TryGetAudioStoppable(out IAudioStoppable audioStoppable) == false)
            {
                return;
            }

            audioStoppable.AudioStopped += OnAudioStopped;
        }

        private void OnDisable()
        {
            AudioPlayable.AudioPlaying -= OnAudioPlayed;

            if (TryGetAudioStoppable(out IAudioStoppable audioStoppable) == false)
            {
                return;
            }

            audioStoppable.AudioStopped -= OnAudioStopped;
        }

        public void Construct(IAudioPlayable audioPlayable)
        {
            _audioPlayable = (MonoBehaviour)audioPlayable;
            OnEnable();
        }

        private bool TryGetAudioStoppable(out IAudioStoppable audioStoppable)
        {
            audioStoppable = null;

            if (AudioPlayable is IAudioStoppable == false)
            {
                return false;
            }

            audioStoppable = AudioPlayable as IAudioStoppable;

            return true;
        }

        private void OnAudioStopped()
        {
            _audioSource.Stop();
        }

        private void OnAudioPlayed()
        {
            if (_isOverlay)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
            }
            else
            {
                _audioSource.Play();
            }
        }
    }
}