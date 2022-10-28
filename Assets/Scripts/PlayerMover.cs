using Fuel;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Engine _engine;

    private float _moveSpeed;

    public float CurrentSpeed => _rigidbody.velocity.magnitude;

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        _moveSpeed = _engine.CalculateNewSpeed(CurrentSpeed);
        Move();
    }

    public void StartMove()
    {
        _engine.BeginAcceleration();
    }

    public void StopMove()
    {
        _engine.BeginDeceleration();
    }

    private void Move()
    {
        if (_moveSpeed <= 0)
        {
            return;
        }

        var velocity = Vector3.forward * _moveSpeed;
        velocity.y = _rigidbody.velocity.y;
        Vector3 worldVelocity = transform.TransformVector(velocity);
        _rigidbody.velocity = worldVelocity;
    }
}