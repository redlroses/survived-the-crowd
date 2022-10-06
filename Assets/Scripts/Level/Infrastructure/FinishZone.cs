using System;
using UnityEngine;

namespace Level.Infrastructure
{
    public sealed class FinishZone : GameZone
    {
        protected override void OnPlayerEnter()
        {
            Debug.Log("Finish Reached");
        }

        protected override void OnPlayerExit()
        {
            Debug.Log("Finish Left");
        }
    }
}
