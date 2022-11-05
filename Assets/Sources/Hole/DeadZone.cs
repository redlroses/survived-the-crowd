using System;
using Sources.Creatures;
using Sources.Creatures.Player;
using UnityEngine;

namespace Hole
{
    public sealed class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMover playerMover))
            {
                playerMover.enabled = false;
                playerMover.GetComponent<Creature>().Damage(int.MaxValue);
            }

            if (other.TryGetComponent(out EnemyMover enemyMover))
            {
                enemyMover.enabled = false;
                enemyMover.GetComponent<Creature>().Damage(int.MaxValue);
            }
        }
    }
}
