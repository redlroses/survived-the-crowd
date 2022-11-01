using System;
using UnityEngine;
using Vehicle;

public sealed class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Car _vehicle;

    private float _moveSpeed;
    private int _transmissionDirection = 1;

    public float CurrentSpeed => _rigidbody.velocity.magnitude;
    public Vector2 Direction => new Vector2(transform.forward.x, transform.forward.z);

    private void FixedUpdate()
    {
        Move();
    }

    public void StartMove()
    {
        _vehicle.Engine.BeginAcceleration();
    }

    public void StopMove()
    {
        _vehicle.Engine.BeginDeceleration();
    }

    public void SetTransmissionDirection(int direction)
    {
        _transmissionDirection = direction;
    }

    private void Move()
    {
        _moveSpeed = _vehicle.Engine.CalculateNewSpeed(CurrentSpeed);

        if (_moveSpeed <= 0)
        {
            return;
        }

        Vector3 moveDirection = _vehicle.Rudder.MoveDirection;
        var velocity = moveDirection * (_moveSpeed * _transmissionDirection);
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }
}