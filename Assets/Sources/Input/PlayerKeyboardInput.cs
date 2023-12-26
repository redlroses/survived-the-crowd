using System;
using Sources.Audio;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Input
{
    public class PlayerKeyboardInput : MonoBehaviour, IAudioStoppable, IInput
    {
        private const string AxisName = "Horizontal";

        [RequireInterface(typeof(ICarControllable))]
        [SerializeField] private MonoBehaviour _mover;

        private bool _isInput;
        private bool _isInputActive;
        private Transform _selfTransform;

        public event Action AudioPlaying;

        public event Action AudioStopped;

        private ICarControllable Mover => (ICarControllable)_mover;

        private void Awake()
        {
            _selfTransform = transform;
        }

        private void Update()
        {
            if (_isInputActive == false)
            {
                Mover.Move(Vector2.zero);

                return;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                StartMove();
            }
            else if (UnityEngine.Input.GetKeyUp(KeyCode.W))
            {
                StopMove();
            }

            float horizontal = UnityEngine.Input.GetAxis(AxisName);
            Vector2 direction;

            if (_isInput)
            {
                Vector2 inputDirection = new Vector2(1f, horizontal);
                Vector3 worldDirection = _selfTransform.TransformDirection(inputDirection.ToWorld());
                direction = worldDirection.ToInputFormat();
            }
            else
            {
                direction = Vector2.zero;
            }

            Mover.Move(direction);
        }

        private void OnDisable()
        {
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
            StopMove();
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