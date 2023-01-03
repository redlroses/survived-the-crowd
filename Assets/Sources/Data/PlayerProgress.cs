using System;
using Sources.Player.Factory;

// ReSharper disable InconsistentNaming

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress : IPlayerProgress
    {
        public ProgressBarData CarUnlocksProgressBar;
        public int CurrentCarUnlockBarIndex = 0;
        public int LastUnlockedCarId = 0;
        public int LastUnlockedWeaponId = 0;
        public WeaponId LastChosenWeapon = WeaponId.MachineGun;
        public CarId LastChosenCar = CarId.HotRod;
    }
}