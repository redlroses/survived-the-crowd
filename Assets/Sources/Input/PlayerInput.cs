using Import.Joystick.Scripts;
using Sources.Creatures.Player;
using Sources.Tools;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Input
{
    [RequireComponent(typeof(IControllable))]
    [RequireComponent(typeof(PlayerRotator))]
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _mover;
        [SerializeField] private Joystick _joystick;

        private float _cameraRotationCompensation;
        private Camera _camera;
        private bool _isInput;

        private ICarControllable Mover => (ICarControllable) _mover;

        private void OnValidate()
        {
            if (_mover is ICarControllable)
            {
                return;
            }

            Debug.LogError($"{_mover.name} needs to implement {nameof(ICarControllable)}");
            _mover = null;
        }

        private void Awake()
        {
            _camera = Camera.main;

            ComponentTool.CheckNull(_joystick);
            ComponentTool.CheckNull(_camera);

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
