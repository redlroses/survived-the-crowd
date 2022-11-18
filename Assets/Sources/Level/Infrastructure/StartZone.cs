using UnityEngine;

namespace Sources.Level.Infrastructure
{
    public sealed class StartZone : GameZone
    {
        protected override void OnPlayerEnter()
        {
            Debug.Log("On Start Zone");
        }

        protected override void OnPlayerExit()
        {
            Debug.Log("On Left Start ZOne");
        }
    }
}
