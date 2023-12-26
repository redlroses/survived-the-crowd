using System.Collections.Generic;
using UnityEngine;

namespace Sources
{
    public sealed class ComponentSwitcher : MonoBehaviour
    {
        [SerializeField] private List<Behaviour> _toDisable;
        [SerializeField] private List<Behaviour> _toEnable;
        private Collider _collider;

        public void EnableComponents()
        {
            SwitchComponents(_toEnable, true);
        }

        public void DisableComponents()
        {
            SwitchComponents(_toDisable, false);
        }

        private void SwitchComponents(IEnumerable<Behaviour> components, bool isEnabled)
        {
            foreach (Behaviour component in components)
            {
                component.enabled = isEnabled;
            }
        }
    }
}