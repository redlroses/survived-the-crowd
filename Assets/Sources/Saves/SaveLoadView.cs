using System.Collections.Generic;
using System.Linq;
using Sources.Data;
using Sources.Level;
using UnityEngine;

namespace Sources.Saves
{
    public class SaveLoadView : MonoBehaviour
    {
        [SerializeField] private LoseDetector _loseDetector;

        [SerializeField] [RequireInterface(typeof(ISavedProgressReader))]
        private List<MonoBehaviour> _progressReaders = new List<MonoBehaviour>();

        private ISaveLoader _saveLoader = new PlayerPrefsSaveLoader();

        private List<ISavedProgressReader> ProgressReaders => _progressReaders
            .ConvertAll(input => input as ISavedProgressReader);
        private List<ISavedProgress> ProgressWriters => _progressReaders.Where(obj => obj is ISavedProgress)
            .ToList()
            .ConvertAll(input => (ISavedProgress) input);

        private void Awake()
        {
            Load();
        }

        private void OnEnable()
        {
            _loseDetector.Lose += Save;
        }

        private void OnDisable()
        {
            _loseDetector.Lose -= Save;
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