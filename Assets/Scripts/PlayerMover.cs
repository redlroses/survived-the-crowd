using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;

    private float _cameraRotationCompensation;
    
    private void Awake()
    {
        _cameraRotationCompensation = Camera.main.transform.rotation.eulerAngles.y;
        Debug.Log(_cameraRotationCompensation);
        _joystick.StickDeviated += Rotate;
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
        float angles = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg + _cameraRotationCompensation;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, angles, 0),
            Time.deltaTime * _rotationSpeed);
    }
}
