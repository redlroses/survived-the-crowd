using Cinemachine;
using UnityEngine;

namespace Sources
{
    public sealed class CameraOperator : MonoBehaviour
    {
        private const int ActiveCameraPriority = 1;
        private const int InactiveCameraPriority = 0;

        [SerializeField] private CinemachineVirtualCamera _gameCamera;
        [SerializeField] private CinemachineVirtualCamera _garageCamera;

        private CinemachineVirtualCamera _currentCamera;

        private void Awake()
        {
            _currentCamera = _gameCamera;
            MoveToGameView();
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
    }
}
