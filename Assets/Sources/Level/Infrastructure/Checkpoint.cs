using UnityEngine;

namespace Sources.Level.Infrastructure
{
    public sealed class Checkpoint : GameZone
    {
        [SerializeField] [Min(0)] private int _index = -1;

        // public event Action Activated;
        // public event Action Deactivated;

        public int Index => _index;

        protected override void OnPlayerEnter()
        {
            Debug.Log($"Checkpoint ({Index}) Reached");
        }

        protected override void OnPlayerExit()
        {
            Debug.Log($"Checkpoint ({Index}) Left");
        }
    }
}
