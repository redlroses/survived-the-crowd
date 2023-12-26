using Sources.Data;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Saves
{
    public class PlayerPrefsSaveLoader : ISaveLoader
    {
        private const string ProgressKey = "ProgressKey";

        public void Save(PlayerProgress progress)
        {
            PlayerPrefs.SetString(ProgressKey, progress.ToJson());
            PlayerPrefs.Save();
        }

        public PlayerProgress Load() =>
            PlayerPrefs.HasKey(ProgressKey)
                ? PlayerPrefs.GetString(ProgressKey).ToDeserialized<PlayerProgress>()
                : new PlayerProgress();
    }
}