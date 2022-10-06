using System;
using UnityEngine;

public sealed class PlayerMover : MonoBehaviour
{
    private const string MainCameraIsNull = "Main Camera is null";

    [SerializeField] private Rotator _rotator;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed = 5f;

    private float _cameraRotationCompensation;

    private void Awake()
    {
        if (_rotator == null)
        {
            _rotator = GetComponent<Rotator>();
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

    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Time.deltaTime * _moveSpeed * Vector3.forward);
    }

    private void Rotate(float horizontal, float vertical)
    {
        float angleY = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + _cameraRotationCompensation;
        _rotator.Rotate(Quaternion.Euler(0, angleY, 0));
    }
}