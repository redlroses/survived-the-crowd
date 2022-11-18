using Import.Joystick.Scripts;
using Sources.Tools.Extensions;
using UnityEngine;
using static Sources.Tools.ComponentTool;

namespace Sources.Input
{
    [RequireComponent(typeof(IControllable))]
    public sealed class PlayerInput : MonoBehaviour
    {
        [RequireInterface(typeof(ICarControllable))]
        [SerializeField] private MonoBehaviour _mover;
        [SerializeField] private Joystick _joystick;

        private float _cameraRotationCompensation;
        private Camera _camera;
        private bool _isInput;

        private ICarControllable Mover => (ICarControllable) _mover;

        private void Awake()
        {
            _camera = Camera.main;

            CheckNull(_joystick);
            CheckNull(_camera);

            _cameraRotationCompensation = _camera.transform.rotation.eulerAngles.y;
        }

        private void Update()
        {
            Vector2 direction = _isInput
                ? _joystick.Direction.RotateVector2(byAngle: _cameraRotationCompensation).ToWorld()
                : Vector2.zero;
            Mover.Move(direction);
        }

        private void OnEnable()
        {
            _joystick.StickUp += StopMove;
            _joystick.StickDown += StartMove;
        }

        private void OnDisable()
        {
            _joystick.StickUp -= StopMove;
            _joystick.StickDown -= StartMove;
        }

        private void StartMove()
        {
            _isInput = true;
            Mover.Accelerate();
        }

        private void StopMove()
        {
            _isInput = false;
            Mover.Decelerate();
        }
    }
}
