using UnityEngine;

namespace Sources
{
    [CreateAssetMenu(menuName = "Create CarHurtBoxPreset", fileName = "NewCarHurtBoxPreset", order = 51)]
    public class CarHurtBoxPreset : ScriptableObject
    {
        [SerializeField] private int _maxHealthPoints;
        [SerializeField] private Vector3 _hurtBoxSize;
        [SerializeField] private Vector3 _hurtBoxOffset;
    }
}
