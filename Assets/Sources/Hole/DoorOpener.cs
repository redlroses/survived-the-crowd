using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Sources.Hole
{
    public sealed class DoorOpener : MonoBehaviour
    {
        [SerializeField] private float _openPosition = 1.15f;
        [SerializeField] private float _duration;
        [SerializeField] private Transform _leftDoor;
        [SerializeField] private Transform _rightDoor;

        [ContextMenu("Open")]
        public void Open()
        {
            TweenerCore<Vector3, Vector3, VectorOptions> lestDoorMove = _leftDoor.DOLocalMoveZ(
                -_openPosition,
                _duration);

            TweenerCore<Vector3, Vector3, VectorOptions> rightDoorMove = _rightDoor.DOLocalMoveZ(
                _openPosition,
                _duration);
        }

        [ContextMenu("Close")]
        public void Close()
        {
            TweenerCore<Vector3, Vector3, VectorOptions> lestDoorMove = _leftDoor.DOLocalMoveZ(0, _duration);
            TweenerCore<Vector3, Vector3, VectorOptions> rightDoorMove = _rightDoor.DOLocalMoveZ(0, _duration);
        }
    }
}