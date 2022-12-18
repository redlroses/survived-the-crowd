using System;
using Sources.Level;
// ReSharper disable InconsistentNaming

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress : IPlayerProgress
    {
        public ProgressBarData CarUnlocksProgressBar;
    }
}