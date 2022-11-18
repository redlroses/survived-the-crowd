using DG.Tweening;
using Sources.Hole;
using Sources.Player;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class TurretActivator : MonoBehaviour
    {
        [SerializeField] private DoorOpener _bunkerDoor;
        [SerializeField] private Transform _turretOrigin;
        [SerializeField] private Transform _turretBarrel;
        [SerializeField] private TargetSeeker _targetSeeker;
        [SerializeField] private float _riseTime;
        [SerializeField] private float _riseDelay;

        private bool _isActivated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover _) == false)
            {
                return;
            }

            if (_isActivated)
            {
                return;
            }

            Activate();
            _isActivated = true;
        }

        private void Activate()
        {
            _bunkerDoor.Open();
            var sequence = DOTween.Sequence();
            sequence.Append(_turretOrigin.DOMoveY(0, _riseTime).OnComplete((() => _bunkerDoor.Close())).SetDelay(_riseDelay));
            sequence.Append(_turretBarrel.DORotate(Vector3.zero, _riseTime).OnComplete(ActivateTurretSystems));
        }

        private void ActivateTurretSystems()
        {
            _targetSeeker.enabled = true;
        }
    }
}
