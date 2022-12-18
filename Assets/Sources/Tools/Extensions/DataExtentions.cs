using Sources.Data;
using Sources.Level;
using UnityEngine;

namespace Sources.Tools.Extensions
{
    public static class DataExtensions
    {
        public static string ToJson(this object progress)
            => JsonUtility.ToJson(progress);

        public static T ToDeserialized<T>(this string json)
            => JsonUtility.FromJson<T>(json);

        public static ProgressBar ToProgressBar(this ProgressBarData data)
            => new ProgressBar(data.Stages, data.StepSize, data.LowValue, data.Stage);

        public static ProgressBarData ToProgressBarData(this ProgressBar bar)
            => new ProgressBarData(bar.Stage, bar.Stages, bar.StepSize, bar.LowValue);
    }
}