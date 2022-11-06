using System;
using Sources.Creatures;
using Sources.Creatures.Player;
using Sources.HealthLogic;
using Sources.Player;
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
                playerMover.GetComponent<IDamageable>().Damage(int.MaxValue);
            }

            if (other.TryGetComponent(out EnemyMover enemyMover))
            {
                enemyMover.enabled = false;
                enemyMover.GetComponent<IDamageable>().Damage(int.MaxValue);
            }
        }
    }
}
