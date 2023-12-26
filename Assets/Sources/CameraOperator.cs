using Cinemachine;
using UnityEngine;

namespace Sources
{
    public sealed class CameraOperator : MonoBehaviour
    {
        private const int ActiveCameraPriority = 1;
        private const int InactiveCameraPriority = 0;

        [SerializeField] private float _landscapeFov = 22.8f;
        [SerializeField] private float _portraitFov = 34.4f;

        [SerializeField] private CinemachineVirtualCamera _gameCamera;
        [SerializeField] private CinemachineVirtualCamera _garageCamera;

        private CinemachineVirtualCamera _currentCamera;

        private void Awake()
        {
            _currentCamera = _gameCamera;
            MoveToGameView();
            SetFov();
        }

        [ContextMenu("MoveToGameView")]
        public void MoveToGameView()
        {
            SwitchTo(_gameCamera);
        }

        [ContextMenu("MoveToGarageView")]
        public void MoveToGarageView()
        {
            SwitchTo(_garageCamera);
        }

        private void SwitchTo(CinemachineVirtualCamera targetCamera)
        {
            _currentCamera.Priority = InactiveCameraPriority;
            _currentCamera = targetCamera;
            _currentCamera.Priority = ActiveCameraPriority;
        }

        private void SetFov()
        {
            _gameCamera.m_Lens.FieldOfView = Camera.main.aspect < 1f ? _portraitFov : _landscapeFov;
        }
    }
}