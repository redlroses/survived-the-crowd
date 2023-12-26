using System;
using Sources.Player.Factory;

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress : IPlayerProgress
    {
        public ProgressBarData CarUnlocksProgressBar;
        public int CurrentCarUnlockBarIndex;
        public WeaponId LastChosenWeapon = WeaponId.MachineGun;
        public WeaponId LastUnlockedWeapon = WeaponId.MachineGun;
        public CarId LastChosenCar = CarId.HotRod;
        public CarId LastUnlockedCar = CarId.HotRod;
    }
}