using System.Collections.Generic;
using Sources.Collectables;

namespace Sources.Level
{
    public class CarUnlockBars : Iterable<ProgressBar>
    {
        public CarUnlockBars(List<ProgressBar> iterableObjects, int index)
            : base(iterableObjects, index)
        {
        }
    }
}