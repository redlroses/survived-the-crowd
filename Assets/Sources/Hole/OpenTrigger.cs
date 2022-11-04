using Sources.Creatures.Player;
using UnityEngine;

namespace Hole
{
    public sealed class OpenTrigger : MonoBehaviour
    {
        [SerializeField] private DoorOpener _doorOpener;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _doorOpener.Open();
            }
        }
    }
}
