using System;
using Sources.Pool;
using UnityEngine;

namespace Sources.Collectables
{
    public sealed class CarDetails : MonoBehaviour, IPoolable<CarDetails>
    {
        public event Action<CarDetails> Destroyed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DetailsCollector detailsCollector) == false)
            {
                return;
            }

            detailsCollector.Increase();
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
