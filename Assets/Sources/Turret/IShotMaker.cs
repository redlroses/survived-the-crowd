using System;
using Sources.StaticData;

namespace Sources.Turret
{
    public interface IShotMaker
    {
        public event Action Shooting;

        void Construct(WeaponStaticData weaponData);

        public void MakeShot();
    }
}