using System;
using Sources.Creatures;
using Sources.HealthLogic;
using UnityEngine;

namespace Hole
{
    public sealed class FallTrigger : MonoBehaviour
    {
        private const string GroundColliderIsNull = "Ground collider is null";

        [SerializeField] private Collider _groundCollider;

        private void Awake()
        {
            if (_groundCollider == null)
            {
                throw new NullReferenceException(GroundColliderIsNull);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable _))
            {
                return;
            }

            Physics.IgnoreCollision(other, _groundCollider);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable _))
            {
                return;
            }

            Physics.IgnoreCollision(other, _groundCollider, false);
        }
    }
}
