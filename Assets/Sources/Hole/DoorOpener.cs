using DG.Tweening;
using UnityEngine;

namespace Sources.Hole
{
    public sealed class DoorOpener : MonoBehaviour
    {
        [SerializeField] private Transform _leftDoor;
        [SerializeField] private Transform _rightDoor;
        [SerializeField] private float _duration;
        [SerializeField] private float _openPosition = 1.15f;

        [ContextMenu("Open")]
        public void Open()
        {
            var lestDoorMove = _leftDoor.DOLocalMoveZ(-_openPosition, _duration);
            var rightDoorMove = _rightDoor.DOLocalMoveZ(_openPosition, _duration);
        }

        [ContextMenu("Close")]
        public void Close()
        {
            var lestDoorMove = _leftDoor.DOLocalMoveZ(0, _duration);
            var rightDoorMove = _rightDoor.DOLocalMoveZ(0, _duration);
        }
    }
}
