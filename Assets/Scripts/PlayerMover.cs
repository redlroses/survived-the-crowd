using UnityEngine;
using Vehicle;

public sealed class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Car _vehicle;

    private float _moveSpeed;
    private Vector3 _moveDirection = Vector3.forward;
    
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

    public void SetMoveDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }

    private void Move()
    {
        _moveSpeed = _vehicle.Engine.CalculateNewSpeed(CurrentSpeed);

        if (_moveSpeed <= 0)
        {
            return;
        }

        var velocity = _moveDirection * _moveSpeed;
        velocity.y = _rigidbody.velocity.y;
        Vector3 worldVelocity = transform.TransformVector(velocity);
        _rigidbody.velocity = worldVelocity;
    }
}