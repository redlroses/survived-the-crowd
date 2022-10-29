using System;
using Import.Joystick.Scripts;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerRotator))]
public sealed class PlayerInput : MonoBehaviour
{
    private const string MainCameraIsNull = "Main Camera is null";
    private const string JoystickIsNull = "Joystick is null";

    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerRotator _rotator;
    [SerializeField] private Joystick _joystick;

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
        _joystick.StickUp += StopMove;
        _joystick.StickDown += StartMove;
    }

    private void OnDisable()
    {
        _joystick.StickDeviated -= Rotate;
        _joystick.StickUp -= StopMove;
        _joystick.StickDown -= StartMove;
    }

    private void Rotate(float horizontal, float vertical)
    {
        float angleY = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + _cameraRotationCompensation;
        Quaternion rotation = Quaternion.Euler(0, angleY, 0);
        _rotator.SetRotation(rotation);
    }

    private void StartMove()
    {
        _mover.StartMove();
    }

    private void StopMove()
    {
        _mover.StopMove();
    }
}
