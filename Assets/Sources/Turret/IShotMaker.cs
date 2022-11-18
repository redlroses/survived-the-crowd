using System;

namespace Sources.Turret
{
    public interface IShotMaker
    {
        public event Action ShotOff;
        public void MakeShot();
    }
}