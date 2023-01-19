using UnityEngine;

namespace Sources.StaticData
{
    [CreateAssetMenu(fileName = "NewWeaponUnlockStaticData", menuName = "StaticData/WeaponUnlockStaticData", order = 51)]
    public class WeaponUnlockStaticData : ScriptableObject
    {
        public int[] ScorePerUpdrades;
    }
}
