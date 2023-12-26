using UnityEngine;

namespace Sources
{
    public sealed class Rotator : MonoBehaviour
    {
        private readonly Vector3 _defaultRotation = Vector3.zero;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private Transform _rotor;

        public void Rotate(Quaternion rotation)
        {
            ApplyRotation(rotation);
        }

        public void RotateTo(Transform target)
        {
            Quaternion rotationToTarget = Quaternion.LookRotation(target.position - transform.position);
            ApplyRotation(rotationToTarget);
        }

        public void ResetRotation()
        {
            _rotor.localRotation = Quaternion.Euler(_defaultRotation);
        }

        private void ApplyRotation(Quaternion rotation)
        {
            _rotor.rotation = Quaternion.Lerp(
                _rotor.rotation,
                rotation,
                Time.deltaTime * _rotationSpeed);
        }
    }
}