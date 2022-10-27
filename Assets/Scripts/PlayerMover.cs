using System;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerMover : MonoBehaviour
{
    private readonly float _velocityEpsilon = 5f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _acceleration = 1;
    [SerializeField] private float _maxMoveSpeed = 5f;

    private float _moveSpeed;
    private Vector2 _direction;

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        Accelerate();
        Move();
    }

    private void Move()
    {
        var velocity = Vector3.forward * _moveSpeed;
        velocity.y = _rigidbody.velocity.y;
        Vector3 worldVelocity = transform.TransformVector(velocity);
        _rigidbody.velocity = worldVelocity;
    }

    private void Accelerate()
    {
        _moveSpeed = _rigidbody.velocity.magnitude;

        if (_moveSpeed > _maxMoveSpeed)
        {
            return;
        }

        _moveSpeed += _acceleration;
    }
}