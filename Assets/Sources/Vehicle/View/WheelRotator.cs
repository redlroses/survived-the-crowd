using Tools.Extensions;
using UnityEngine;

namespace Sources.Vehicle.View
{
    public sealed class WheelRotator : MonoBehaviour
    {
        [SerializeField] private Transform _leftWheel;
        [SerializeField] private Transform _rightWheel;

        public void RotateWheels(float leftWheelAngle, float rightWheelAngle)
        {
            _leftWheel.SetLocalEulerY(leftWheelAngle);
            _rightWheel.SetLocalEulerY(rightWheelAngle);
        }
    }
}