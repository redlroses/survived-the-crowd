using System;
// ReSharper disable InconsistentNaming

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress : IPlayerProgress
    {
        public ProgressBarData CarUnlocksProgressBar;
    }
}