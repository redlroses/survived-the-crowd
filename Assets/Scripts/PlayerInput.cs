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
        if (_mover == null)
        {
            _mover = GetComponent<PlayerMover>();
        }

        if (_rotator == null)
        {
            _rotator = GetComponent<PlayerRotator>();
        }

        if (_joystick == null)
        {
            throw new NullReferenceException(JoystickIsNull);
        }

        if (Camera.main == null)
        {
            throw new NullReferenceException(MainCameraIsNull);
        }

        _cameraRotationCompensation = Camera.main.transform.rotation.eulerAngles.y;
    }

    private void OnEnable()
    {
        _joystick.StickDeviated += Rotate;
    }

    private void OnDisable()
    {
        _joystick.StickDeviated -= Rotate;
    }

    private void Rotate(float horizontal, float vertical)
    {
        float angleY = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + _cameraRotationCompensation;
        Quaternion rotation = Quaternion.Euler(0, angleY, 0);
        _rotator.SetRotation(rotation);
    }
}
