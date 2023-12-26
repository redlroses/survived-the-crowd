using System.Collections.Generic;
using System.Linq;
using Sources.Data;
using Sources.Level;
using UnityEngine;

namespace Sources.Saves
{
    public class SaveLoadView : MonoBehaviour
    {
        private readonly ISaveLoader _saveLoader = new PlayerPrefsSaveLoader();

        [SerializeField] [RequireInterface(typeof(ISavedProgressReader))]
        private List<MonoBehaviour> _progressReaders = new List<MonoBehaviour>();

        [SerializeField] private LoseDetector _loseDetector;

        private List<ISavedProgressReader> ProgressReaders => _progressReaders
            .ConvertAll(input => input as ISavedProgressReader);

        private IEnumerable<ISavedProgress> ProgressWriters => _progressReaders.Where(obj => obj is ISavedProgress)
            .ToList()
            .ConvertAll(input => (ISavedProgress)input);

        private void Awake()
        {
            Load();
        }

        private void OnEnable()
        {
            _loseDetector.Losed += Save;
        }

        private void OnDisable()
        {
            _loseDetector.Losed -= Save;
            Save();
        }

        public void Load()
        {
            foreach (ISavedProgressReader progressReader in ProgressReaders)
            {
                progressReader.LoadProgress(_saveLoader.Load());
            }
        }

        public void Save()
        {
            PlayerProgress progress = _saveLoader.Load();

            foreach (ISavedProgress progressWriter in ProgressWriters)
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