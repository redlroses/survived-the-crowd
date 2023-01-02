using Sources.Player.Factory;
using UnityEngine;

namespace Sources.StaticData
{
    [CreateAssetMenu(fileName = "NewCarData", menuName = "StaticData/CarData", order = 51)]
    public class CarStaticData : ScriptableObject
    {
        public CarId CarId;
        [Range(0, 100)] public int MaxHealth;
        [Range(0, 90)] public float MaxWheelAngle;
        [Range(0, 0.1f)] public float Consumption;
        [Range(0, 0.1f)] public float IdleConsumption;
        [Range(0, 2)] public float Acceleration;
        [Range(0, 2)] public float Deceleration;
        [Range(0, 20)] public float MaxSpeed;
        [Range(0, 100)] public float MaxFuel;
    }
}
