using System;
using System.Linq;
using Sources.Player.Factory;
using Sources.StaticData;
using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Ui
{
    public class WeaponStatsOperatorView : MonoBehaviour
    {
        [SerializeField] private WeaponStaticData[] _weaponsStaticData;
        [SerializeField] private TextSetter _label;

        [Space] [Header("Stats")]
        [SerializeField] private StatBarView _damage;
        [SerializeField] private StatBarView _fireRate;
        [SerializeField] private StatBarView _radius;

        private int _damageRange;
        private float _fireRateRange;
        private float _radiusRange;

        private bool IsInitializedStatRange => _damageRange > 0;

        public void SetStats(WeaponId weaponId)
        {
            WeaponStaticData weaponData = _weaponsStaticData.FirstOrDefault(data => data.WeaponId == weaponId);

            if (weaponData is null)
            {
                throw new ArgumentNullException(nameof(weaponData));
            }

            if (IsInitializedStatRange == false)
            {
                InitStatRange();
            }

            _label.Set(weaponId.ToString());
            _damage.SetStat(GetNormalized(weaponData.Damage, _damageRange));
            _fireRate.SetStat(GetNormalized(weaponData.FireRate, _fireRateRange));
            _radius.SetStat(GetNormalized(weaponData.Radius, _radiusRange));
        }

        private void InitStatRange()
        {
            _damageRange = _weaponsStaticData.Max(data => data.Damage);
            _fireRateRange = _weaponsStaticData.Max(data => data.FireRate);
            _radiusRange = _weaponsStaticData.Max(data => data.Radius);
        }

        private float GetNormalized(float value, float maxValue)
            => value / maxValue;
    }
}