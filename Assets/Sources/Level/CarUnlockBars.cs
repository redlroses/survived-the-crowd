using System.Collections.Generic;
using Sources.Collectables;
using Sources.Player.Factory;

namespace Sources.Level
{
    public class CarUnlockBars : Iterable<ProgressBar>
    {
        public CarUnlockBars(List<ProgressBar> iterableObjects, int index) : base(iterableObjects, index)
        {
        }
    }
}