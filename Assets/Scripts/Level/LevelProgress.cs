using System;
using System.Collections.Generic;
using Level.Infrastructure;
using UnityEngine;

namespace Level
{
    public sealed class LevelProgress : MonoBehaviour
    {
        [SerializeField] private StartZone _startZone;
        [SerializeField] private StartZone _finishZone;
        [SerializeField] private ProgressBar _progress;
        [SerializeField] private CheckpointsChain _chain;

        private void Awake()
        {
            _progress = new ProgressBar(_chain.Size);
        }

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        private void AddListeners()
        {
            _chain.CheckpointReached += IncreaseProgress;
        }

        private void RemoveListeners()
        {
            _chain.CheckpointReached -= IncreaseProgress;
        }

        private void IncreaseProgress()
        {
            _progress.NextStage();

            Debug.Log($"Progress: {_progress.Amount * 100f}%");
        }
    }
}
