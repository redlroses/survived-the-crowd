using System.Collections.Generic;
using Sources.Turret;

namespace Sources.Player.Factory
{
    public class Weapons
    {
        private readonly List<TargetSeeker> _weapons;

        public Weapons(List<TargetSeeker> weapons)
        {
            _weapons = weapons;
        }
    }
}