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
        private readonly List<ProgressBar> _carUnlocksProgress = new List<ProgressBar>
        {
            new ProgressBar(5, 5),
            new ProgressBar(10, 10)
        };

        [SerializeField] private int _detailsAmount;

        private ProgressBar _progressBar;
        private CarUnlockBars _carUnlockBars;

        public ProgressBar CurrentProgressBar => _progressBar;

        public void Increase()
        {
            _detailsAmount++;
            _progressBar.NextStage();

            if (_progressBar.IsComplete)
            {
                _progressBar = _carUnlockBars.Next();
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.CarUnlocksProgressBar == null)
            {
                _carUnlockBars = new CarUnlockBars(_carUnlocksProgress, 0);
                _progressBar = _carUnlockBars.Current;
            }
            else
            {
                _carUnlockBars = new CarUnlockBars(_carUnlocksProgress, progress.CurrentCarUnlockBarIndex);
                _progressBar = progress.CarUnlocksProgressBar.ToProgressBar();
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CarUnlocksProgressBar = _progressBar.ToProgressBarData();
        }
    }
}