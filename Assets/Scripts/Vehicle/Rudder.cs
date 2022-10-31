using System;
using Tools;
using UnityEngine;

public class Rudder : MonoBehaviour
{
    private readonly float _debugSphereRadius = 0.2f;

    [SerializeField] [Range(0, 90f)] private float _maxAngle = 30f;
    [SerializeField] private Transform _leftWheel;
    [SerializeField] private Transform _rightWheel;
    [SerializeField] private Transform _backAxle;

    private float _wheelBase;
    private float _wheelAngle;
    private float _backAxleTurningRadius;
    private Vector3 _turningPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_rightWheel.transform.position, _rightWheel.transform.right * int.MaxValue);
        Gizmos.DrawRay(_leftWheel.transform.position, -_leftWheel.transform.right * int.MaxValue);
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(_backAxle.transform.position, _backAxle.transform.right * int.MaxValue);
        Gizmos.DrawRay(_backAxle.transform.position, -_backAxle.transform.right * int.MaxValue);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_turningPoint, _debugSphereRadius);
    }

    public Vector3 GetRotationCenter(float wheelAngle)
    {
        var rightWheelLocalPositionL = _rightWheel.localPosition;
        _wheelBase = Mathf.Abs(rightWheelLocalPositionL.z - _backAxle.localPosition.z);
        _wheelAngle = _rightWheel.localEulerAngles.y;
        float tan = Mathf.Tan((Constants.HalfPiDegrees - _wheelAngle) * Mathf.Deg2Rad);
        _backAxleTurningRadius = _wheelBase * tan;
        _turningPoint = new Vector3(
            rightWheelLocalPositionL.x + _backAxleTurningRadius,
            rightWheelLocalPositionL.y,
            rightWheelLocalPositionL.z - _wheelBase);
        _turningPoint = transform.TransformPoint(_turningPoint);
        return _turningPoint;
    }
}
