using System;
using System.Linq;
using Sources.Player.Factory;
using Sources.StaticData;
using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Ui
{
    public class CarStatsOperatorView : MonoBehaviour
    {
        [SerializeField] private CarStaticData[] _carsStaticData;
        [SerializeField] private TextSetter _label;

        [Space] [Header("Stats")]
        [SerializeField] private StatBarView _maxHealth;
        [SerializeField] private StatBarView _maxSpeed;
        [SerializeField] private StatBarView _acceleration;
        [SerializeField] private StatBarView _maxWheelAngle;
        [SerializeField] private StatBarView _consumption;

        private int _maxHealthRange;
        private float _maxSpeedRange;
        private float _accelerationRange;
        private float _maxWheelAngleRange;
        private float _consumptionRange;

        private bool IsInitializedStatRange => _maxHealthRange > 0;

        public void SetStats(CarId carId)
        {
            CarStaticData carData = _carsStaticData.FirstOrDefault(data => data.CarId == carId);

            if (carData is null)
            {
                throw new ArgumentNullException(nameof(carData));
            }

            if (IsInitializedStatRange == false)
            {
                InitStatRange();
            }

            _label.Set(carId.ToString());
            _maxHealth.SetStat(GetNormalized(carData.MaxHealth, _maxHealthRange));
            _maxSpeed.SetStat(GetNormalized(carData.MaxSpeed, _maxSpeedRange));
            _acceleration.SetStat(GetNormalized(carData.Acceleration, _accelerationRange));
            _maxWheelAngle.SetStat(GetNormalized(carData.MaxWheelAngle, _maxWheelAngleRange));
            _consumption.SetStat(GetNormalized(carData.Consumption, _consumptionRange));
        }

        private void InitStatRange()
        {
            _maxHealthRange = _carsStaticData.Max(data => data.MaxHealth);
            _maxSpeedRange = _carsStaticData.Max(data => data.MaxSpeed);
            _accelerationRange = _carsStaticData.Max(data => data.Acceleration);
            _maxWheelAngleRange = _carsStaticData.Max(data => data.MaxWheelAngle);
            _consumptionRange = _carsStaticData.Max(data => data.Consumption);
        }

        private float GetNormalized(float value, float maxValue)
            => value / maxValue;
    }
}