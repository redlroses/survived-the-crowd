using Sources.Player;
using UnityEngine;

namespace Sources.Hole
{
    public sealed class OpenTrigger : MonoBehaviour
    {
        [SerializeField] private DoorOpener _doorOpener;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover _))
            {
                _doorOpener.Open();
            }
        }
    }
}
