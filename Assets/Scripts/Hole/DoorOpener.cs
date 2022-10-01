using UnityEngine;
using DG.Tweening;

namespace Hole
{
    public sealed class DoorOpener : MonoBehaviour
    {
        [SerializeField] private Transform _leftDoor;
        [SerializeField] private Transform _rightDoor;
        [SerializeField] private float _duration;
        [SerializeField] private float _targetZPosition = 1.15f;

        [ContextMenu("Open")]
        public void Open()
        {
            var lestDoorMove = _leftDoor.DOLocalMoveZ(-_targetZPosition, _duration);
            var rightDoorMove = _rightDoor.DOLocalMoveZ(_targetZPosition, _duration);
        }
    }
}
