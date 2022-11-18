using UnityEngine;

namespace Sources
{
    public sealed class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            transform.position = _target.position;
        }
    }
}