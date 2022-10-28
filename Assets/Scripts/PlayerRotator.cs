using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerRotator : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _rotationSpeed;

    private Quaternion _rotation;

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    public void SetRotation(Quaternion rotation)
    {
        _rotation = rotation;
    }

    private void Rotate()
    {
        float rotationSpeed = Mathf.Clamp01(_mover.CurrentSpeed) * Time.fixedDeltaTime * _rotationSpeed;
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _rotation, rotationSpeed);
    }
}
