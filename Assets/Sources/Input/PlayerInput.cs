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
        private bool _isInputActive;

        private ICarControllable Mover => (ICarControllable) _mover;

        private void Awake()
        {
            _camera = Camera.main;

            CheckNull(_camera);

            _cameraRotationCompensation = _camera.transform.rotation.eulerAngles.y;
        }

        private void Update()
        {
            var direction = _isInputActive && _joystick.Direction != Vector2.zero
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

            StopMove();
        }

        public void Init(Joystick joyStick)
        {
            _joystick = joyStick;
        }

        public void Activate()
        {
            enabled = true;
        }

        public void Deactivate()
        {
            enabled = false;
        }

        private void StartMove()
        {
            _isInputActive = true;
            Mover.Accelerate();
        }

        private void StopMove()
        {
            _isInputActive = false;
            Mover.Decelerate();
        }
    }
}
