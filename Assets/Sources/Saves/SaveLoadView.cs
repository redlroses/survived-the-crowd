using System.Collections.Generic;
using System.Linq;
using Sources.Data;
using UnityEngine;

namespace Sources.Saves
{
    public class SaveLoadView : MonoBehaviour
    {
        private ISaveLoader _saveLoader = new PlayerPrefsSaveLoader();

        [SerializeField] [RequireInterface(typeof(ISavedProgressReader))]
        private List<MonoBehaviour> _progressWriters = new List<MonoBehaviour>();

        private List<ISavedProgressReader> ProgressReaders => _progressWriters
            .ConvertAll(input => (ISavedProgressReader) input);
        private List<ISavedProgress> ProgressWriters => _progressWriters.Where(obj => obj is ISavedProgress)
            .ToList()
            .ConvertAll(input => (ISavedProgress) input);

        private void Awake()
        {
            Load();
        }

        private void OnDisable()
        {
            Save();
        }

        public void Load()
        {
            foreach (var progressReader in ProgressReaders)
            {
                progressReader.LoadProgress(_saveLoader.Load());
            }
        }

        public void Save()
        {
            PlayerProgress progress = _saveLoader.Load();

            foreach (var progressWriter in ProgressWriters)
            {
                progressWriter.UpdateProgress(progress);
            }

            _saveLoader.Save(progress);
        }

        [ContextMenu("Clear Prefs")]
        private void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}