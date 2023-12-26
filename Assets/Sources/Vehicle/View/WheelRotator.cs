using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Vehicle.View
{
    public sealed class WheelRotator : MonoBehaviour
    {
        [SerializeField] private Transform _leftWheel;
        [SerializeField] private Transform _rightWheel;

        private void OnEnable()
        {
            RotateWheels(0, 0);
        }

        public void RotateWheels(float leftWheelAngle, float rightWheelAngle)
        {
            _leftWheel.SetLocalEulerY(leftWheelAngle);
            _rightWheel.SetLocalEulerY(rightWheelAngle);
        }
    }
}