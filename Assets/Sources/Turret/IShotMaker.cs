using System;

namespace Sources.Turret
{
    internal interface IShotMaker
    {
        public event Action ShotOff;
        public void MakeShot();
    }
}