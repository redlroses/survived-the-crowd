using System;

namespace Sources.Data
{
    [Serializable]
    public class ProgressBarData
    {
        public int Stage;
        public int Stages;
        public float StepSize;
        public int LowValue;

        public ProgressBarData(int stage, int stages, float stepSize, int lowValue)
        {
            Stage = stage;
            Stages = stages;
            StepSize = stepSize;
            LowValue = lowValue;
        }
    }
}