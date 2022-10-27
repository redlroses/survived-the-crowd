using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerRotator : MonoBehaviour
{
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
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _rotation, Time.fixedDeltaTime * _rotationSpeed);
    }
}
