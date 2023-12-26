using System;
using Import.Joystick.Scripts;
using Sources.Audio;
using Sources.Tools.Extensions;
using UnityEngine;
using static Sources.Tools.ComponentTool;

namespace Sources.Input
{
    [RequireComponent(typeof(IControllable))]
    public sealed class PlayerInput : MonoBehaviour, IAudioStoppable, IInput
    {
        [SerializeField] private Joystick _joystick;
        [RequireInterface(typeof(ICarControllable))] [SerializeField] private MonoBehaviour _mover;

        private Camera _camera;
        private float _cameraRotationCompensation;
        private bool _isInput;
        private bool _isInputActive;

        public event Action AudioPlaying;

        public event Action AudioStopped;

        private ICarControllable Mover => (ICarControllable)_mover;

        private void Awake()
        {
            _camera = Camera.main;

            CheckNull(_camera);

            _cameraRotationCompensation = _camera.transform.rotation.eulerAngles.y;
        }

        private void Update()
        {
            if (_isInputActive == false)
            {
                Mover.Move(Vector2.zero);

                return;
            }

            Vector2 direction = _isInput && _joystick.Direction != Vector2.zero
                ? _joystick.Direction.RotateVector2(_cameraRotationCompensation).ToWorld()
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

        public void Activate()
        {
            AudioPlaying?.Invoke();
            _isInputActive = true;
        }

        public void Deactivate()
        {
            AudioStopped?.Invoke();
            _isInputActive = false;
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