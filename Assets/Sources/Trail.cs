using UnityEngine;

namespace Sources
{
    public class Trail : MonoBehaviour
    {
        [SerializeField] private TrailRenderer _trailRenderer;

        private void OnDisable()
        {
            _trailRenderer.Clear();
        }
    }
}