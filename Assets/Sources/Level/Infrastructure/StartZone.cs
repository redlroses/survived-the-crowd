using UnityEngine;

namespace Level.Infrastructure
{
    public sealed class StartZone : GameZone
    {
        protected override void OnPlayerEnter()
        {
            Debug.Log("On Start");
        }

        protected override void OnPlayerExit()
        {
            Debug.Log("On Left Start");
        }
    }
}
