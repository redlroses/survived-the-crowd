using System;
using Tools;
using Tools.Extensions;
using UnityEngine;
using Vehicle;

public sealed class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Car _vehicle;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private float _rotationSpeed = 1;

    private Quaternion _rotation;

    private void FixedUpdate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_vehicle.Rudder.MoveDirection);
        _rigidbody.rotation =
            Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.fixedDeltaTime * _rotationSpeed);
    }

    public void SetRotation(Vector2 direction)
    {
        direction = direction.RotateVector2(Constants.HalfPiDegrees);
        float dotProduct = Vector2.Dot(direction, _mover.Direction);
        float moveAngle = Mathf.Atan2(_mover.Direction.x, _mover.Direction.y) * Mathf.Rad2Deg;
        float inputAngle = CalculateInputAngle(direction, dotProduct);
        float angleDeviation = moveAngle - inputAngle;
        angleDeviation = ClampAngle(angleDeviation) * Math.Sign(dotProduct);
        _vehicle.Rudder.DeflectSteeringWheel(angleDeviation);
    }

    private static float CalculateInputAngle(Vector2 direction, float dotProduct)
    {
        float dotSign = Mathf.Sign(dotProduct);
        return Mathf.Atan2(direction.x * dotSign, direction.y * dotSign) * Mathf.Rad2Deg;
    }

    private float ClampAngle(float angle)
    {
        if (angle > Constants.PiDegrees)
        {
            angle -= Constants.TwoPiDegrees;
        }

        if (angle < -Constants.PiDegrees)
        {
            angle += Constants.TwoPiDegrees;
        }

        return angle;
    }
}
