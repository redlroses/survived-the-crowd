using System;
using System.Collections;
using Sources.Audio;
using Sources.Pool;
using UnityEngine;

namespace Sources
{
    [RequireComponent(typeof(ParticleSystem))]
    public class FxPoolable : MonoBehaviour, IPoolable<FxPoolable>, IAudioPlayable
    {
        [SerializeField] private ParticleSystem _particle;

        public event Action<FxPoolable> Destroyed;

        public event Action AudioPlaying;

        private void OnEnable()
        {
            _particle.Play();
            AudioPlaying?.Invoke();
            StartCoroutine(YieldDisable());
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        private IEnumerator YieldDisable()
        {
            yield return new WaitForSeconds(2.5f);

            gameObject.SetActive(false);
        }
    }
}