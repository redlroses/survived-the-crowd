using System;
using System.Collections.Generic;
using Sources.Level;
using UnityEngine;

namespace Sources.Collectables
{
    public class DetailsCollector : MonoBehaviour
    {
        [SerializeField] private int _detailsAmount;

        private List<ProgressBar> _carUnlocksProgress = new List<ProgressBar>
        {
            new ProgressBar(4, 4),
            new ProgressBar(8, 8)
        };

        private ProgressBar _progressBar;

        public event Action<ProgressBar> BarActivated;

        public ProgressBar CurrentProgressBar => _progressBar;

        private void Awake()
        {
            _progressBar = _carUnlocksProgress[0];
        }

        public void Increase()
        {
            _detailsAmount++;
            _progressBar.NextStage();

            if (_progressBar.IsComplete)
            {
                _progressBar = _carUnlocksProgress[1];
            }
        }
    }
} 