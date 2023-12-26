using Sources.Player.Factory;
using UnityEngine;

namespace Sources.StaticData
{
    [CreateAssetMenu(fileName = "NewCarData", menuName = "StaticData/CarData", order = 51)]
    public class CarStaticData : ScriptableObject
    {
        [Range(0, 2f)] public float Acceleration;
        public CarId CarId;
        [Range(0, 0.1f)] public float Consumption;
        [Range(0, 2f)] public float Deceleration;
        [Range(0, 0.1f)] public float IdleConsumption;
        [Range(0, 100f)] public float MaxFuel;
        [Range(0, 100)] public int MaxHealth;
        [Range(0, 20f)] public float MaxSpeed;
        [Range(0, 90f)] public float MaxWheelAngle;
    }
}