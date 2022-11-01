using Tools;
using Tools.Extensions;
using UnityEngine;
using Vehicle;

public sealed class PlayerRotator : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Car _vehicle;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private float _rotationSpeed;

    private Quaternion _rotation;

    public void SetRotation(Vector2 direction)
    {
        direction = direction.RotateVector2(Constants.HalfPiDegrees);
        float inputAngle = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        float moveAngle = Mathf.Atan2(_mover.Direction.x, _mover.Direction.y) * Mathf.Rad2Deg;
        float angleDeviation = inputAngle - moveAngle;
        angleDeviation = ClampAngle(angleDeviation);
        Debug.Log($"inputAngle: {inputAngle}, moveAngle: {moveAngle}, angleDeviation: {angleDeviation}");
        _vehicle.Rudder.DeflectSteeringWheel(angleDeviation);
    }

    private void RotateLegacy()
    {
        float rotationSpeed = Mathf.Clamp01(_mover.CurrentSpeed) * Time.fixedDeltaTime * _rotationSpeed;
        _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, _rotation, rotationSpeed);
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
