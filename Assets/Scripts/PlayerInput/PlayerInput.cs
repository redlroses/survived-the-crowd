using System;
using Import.Joystick.Scripts;
using Tools;
using UnityEngine;

namespace PlayerInput
{
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerRotator))]
    public sealed class PlayerInput : MonoBehaviour
    {
        private const string MainCameraIsNull = "Main Camera is null";
        private const string JoystickIsNull = "Joystick is null";

        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerRotator _rotator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _directionSwitchingThreshold = 0.5f;

        private float _cameraRotationCompensation;

        private void Awake()
        {
            _mover ??= GetComponent<PlayerMover>();
            _rotator ??= GetComponent<PlayerRotator>();

            CheckNull(_joystick, JoystickIsNull);
            CheckNull(Camera.main, MainCameraIsNull);

            _cameraRotationCompensation = Camera.main.transform.rotation.eulerAngles.y;
        }

        private void CheckNull<T>(T component, string message)
        {
            if (component == null)
            {
                throw new NullReferenceException(message);
            }
        }

        private void OnEnable()
        {
            _joystick.StickDeviated += Rotate;
            _joystick.StickDeviated += SetDirection;
            _joystick.StickUp += StopMove;
            _joystick.StickDown += StartMove;
        }

        private void OnDisable()
        {
            _joystick.StickDeviated -= Rotate;
            _joystick.StickDeviated -= SetDirection;
            _joystick.StickUp -= StopMove;
            _joystick.StickDown -= StartMove;
        }

        private void Rotate(float horizontal, float vertical)
        {
            float angleY = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + _cameraRotationCompensation;
            Quaternion rotation = Quaternion.Euler(0, angleY, 0);
            _rotator.SetRotation(rotation);
            Debug.Log(Vector2.Dot(_mover.Direction, new Vector2(horizontal, vertical).RotateVector2(-_cameraRotationCompensation)));
        }

        private void SetDirection(float horizontal, float vertical)
        {
            Vector2 inputDirection = new Vector2(horizontal, vertical).RotateVector2(-_cameraRotationCompensation);
            _mover.SetMoveDirection(GetMoveDirection(_mover.Direction, inputDirection));
        }

        private void StartMove()
        {
            _mover.StartMove();
        }

        private void StopMove()
        {
            _mover.StopMove();
        }

        private Vector3 GetMoveDirection(Vector2 vehicle, Vector2 input)
        {
            return Vector2.Dot(vehicle, input) > _directionSwitchingThreshold ? Vector3.forward : Vector3.back;
        }
    }
}
