using System;

namespace Level
{
    public sealed class ProgressBar
    {
        private const string MaximumProgressException = "Maximum progress must be equal or greater than 1";

        private readonly int _maxProgress;
        private readonly int _stages;

        private int _stage = 0;

        public ProgressBar(int stages, int maxProgress = 1)
        {
            if (stages <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stages));
            }

            if (_maxProgress < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(stages), MaximumProgressException);
            }

            _stages = stages;
            _maxProgress = maxProgress;
        }

        public bool IsComplete => _stage >= _stages;
        public float Amount => _maxProgress / (float) _stage;

        public void Reset()
        {
            _stage = 0;
        }

        public void NextStage()
        {
            _stage++;
        }
    }
}