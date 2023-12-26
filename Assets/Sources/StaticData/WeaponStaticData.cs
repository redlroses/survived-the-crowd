using Sources.Player.Factory;
using UnityEngine;

namespace Sources.StaticData
{
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "StaticData/WeaponData", order = 51)]
    public class WeaponStaticData : ScriptableObject
    {
        [Range(0, 20)] public int Damage;
        [Range(0f, 20f)] public float FireRate;
        [Range(0f, 50f)] public float Radius;
        public WeaponId WeaponId;
    }
}