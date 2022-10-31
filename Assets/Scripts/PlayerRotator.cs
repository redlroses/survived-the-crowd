using Tools;
using UnityEngine;
using Vehicle;

public sealed class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Car _vehicle;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private float _rotationSpeed;

    private Quaternion _rotation;

    private void FixedUpdate()
    {
        RotateLegacy();
    }

    public void SetRotation(Quaternion rotation)
    {
        _rotation = rotation;
    }

    private void RotateLegacy()
    {
        float rotationSpeed = Mathf.Clamp01(_mover.CurrentSpeed) * Time.fixedDeltaTime * _rotationSpeed;
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _rotation, rotationSpeed);
    }
}
