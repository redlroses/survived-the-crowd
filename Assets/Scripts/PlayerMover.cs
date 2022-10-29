using UnityEngine;
using Vehicle;

public sealed class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private Car _vehicle;

    private float _moveSpeed;

    public float CurrentSpeed => _rigidbody.velocity.magnitude;

    private void FixedUpdate()
    {
        _moveSpeed = _vehicle.Engine.CalculateNewSpeed(CurrentSpeed);
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