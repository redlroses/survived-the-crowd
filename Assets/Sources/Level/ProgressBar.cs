using System;
using System.Text;

namespace Sources.Level
{
    public sealed class ProgressBar
    {
        private const string MaximumProgressException = "High value must be greater than low value";
        private const string StageCountException = "Stage Count must be equal or greater than 1";

        private const int HighValueDefault = 1;
        private const int LowValueDefault = 0;

        private int _stage;

        public ProgressBar(int stages, int highValue = HighValueDefault, int lowValue = LowValueDefault)
        {
            if (stages <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stages));
            }

            if (highValue < lowValue)
            {
                throw new Exception(MaximumProgressException);
            }

            Stages = stages;
            HighValue = highValue;
            LowValue = lowValue;
            StepSize = highValue / (float) stages;
        }

        public ProgressBar(int stages, float stepSize, int lowValue = LowValueDefault)
        {
            if (stepSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stepSize));
            }

            if (stages < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(stages), StageCountException);
            }

            StepSize = stepSize;
            Stages = stages;
            HighValue = stepSize * stages;
            LowValue = lowValue;
        }

        public event Action Updated;

        public int Stages { get; }
        public float StepSize { get; }
        public float HighValue { get; }
        public float LowValue { get; }

        public bool IsComplete => Stage == Stages;

        public float Amount => (float) Stage / Stages * HighValue;

        public float ClampedAmount => (float) Stage / Stages;

        public float Progress => Stage * StepSize;

        public int Stage
        {
            get => _stage;
            private set
            {
                _stage = value;
                Updated?.Invoke();
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('|');

            for (int i = 0; i < Stage; i++)
            {
                stringBuilder.Append('█');
            }

            for (int i = Stage; i < Stages; i++)
            {
                stringBuilder.Append('_');
            }

            stringBuilder.Append('|');
            stringBuilder.Append(Stage);
            stringBuilder.Append('/');
            stringBuilder.Append(Stages);

            return stringBuilder.ToString();
        }

        public void Reset()
        {
            Stage = 0;
        }

        public void NextStage()
        {
            if (IsComplete)
            {
                return;
            }

            Stage++;
        }
    }
}