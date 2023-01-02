using Sources.HealthLogic;
using Sources.StaticData;
using UnityEngine;

namespace Sources.Player
{
    public class PlayerHealth : Health, IAttackable
    {
        [SerializeField] private Collider _hurtBox;

        public Vector3 GetAttackPoint(Vector3 attackerPosition)
            => _hurtBox.ClosestPoint(attackerPosition);

        public Vector3 GetAttackCenter(Vector3 attackerPosition)
            => _hurtBox.transform.position;

        public bool IsAttackable => gameObject.activeInHierarchy;

        public void Construct(CarStaticData carStaticData)
        {
            SetMaxPoints(carStaticData.MaxHealth);
        }
    }
}