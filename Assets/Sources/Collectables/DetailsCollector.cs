using System.Collections.Generic;
using Sources.Data;
using Sources.Level;
using Sources.Saves;
using Sources.Tools.Extensions;
using UnityEngine;

namespace Sources.Collectables
{
    public class DetailsCollector : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private int _detailsAmount;

        private List<ProgressBar> _carUnlocksProgress = new List<ProgressBar>
        {
            new ProgressBar(4, 4),
            new ProgressBar(8, 8)
        };

        private ProgressBar _progressBar;

        public ProgressBar CurrentProgressBar => _progressBar;

        public void Increase()
        {
            _detailsAmount++;
            _progressBar.NextStage();

            if (_progressBar.IsComplete)
            {
                _progressBar = _carUnlocksProgress[1];
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.CarUnlocksProgressBar == null)
            {
                _progressBar = _carUnlocksProgress[0];
            }

            _progressBar = progress.CarUnlocksProgressBar.ToProgressBar();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CarUnlocksProgressBar = _progressBar.ToProgressBarData();
        }
    }
}