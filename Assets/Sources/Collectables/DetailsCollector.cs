using System;
using System.Collections.Generic;
using Sources.Data;
using Sources.Level;
using Sources.Player.Factory;
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
            new ProgressBar(10, 10),
        };

        private CarUnlockBars _carUnlockBars;
        private CarId _lastUnlockedCarId;

        public event Action<CarId> NewCarUnlocked;

        public ProgressBar CurrentProgressBar { get; private set; }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.CarUnlocksProgressBar == null)
            {
                _carUnlockBars = new CarUnlockBars(_carUnlocksProgress, 0);
                CurrentProgressBar = _carUnlockBars.Current;
            }
            else
            {
                _carUnlockBars = new CarUnlockBars(_carUnlocksProgress, progress.CurrentCarUnlockBarIndex);
                CurrentProgressBar = progress.CarUnlocksProgressBar.ToProgressBar();
            }

            _lastUnlockedCarId = progress.LastChosenCar;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.CarUnlocksProgressBar = CurrentProgressBar.ToProgressBarData();
            progress.LastUnlockedCar = _lastUnlockedCarId;
        }

        public void Increase()
        {
            CurrentProgressBar.NextStage();

            if (CurrentProgressBar.IsComplete == false)
            {
                return;
            }

            CurrentProgressBar = _carUnlockBars.Next();
            _lastUnlockedCarId++;
            NewCarUnlocked?.Invoke(_lastUnlockedCarId);
        }
    }
}