using System;
using System.Linq;
using UnityEngine;

namespace Level.Infrastructure
{
    public sealed class CheckpointsChain : MonoBehaviour
    {
        private const int StartIndex = -1;
        private const string SomeCheckpointIndicesAreEquals = "Some checkpoint indices are equals";

        [SerializeField] private Checkpoint[] _checkpoints;

        private int _activeCheckpointIndex;

        public event Action CheckpointReached;
        public event Action ChainComplete;

        public int Size => _checkpoints.Length + 1;
        private bool IsChainComplete => _activeCheckpointIndex > _checkpoints.Length;

        private void Awake()
        {
            Sort();
            Validate();
        }

        private void OnEnable()
        {
            foreach (var checkpoint in _checkpoints)
            {
                checkpoint.Reached += OnCheckpointReached;
            }
        }

        private void OnDisable()
        {
            foreach (var checkpoint in _checkpoints)
            {
                checkpoint.Reached -= OnCheckpointReached;
            }
        }

        public void Begin()
        {
            _activeCheckpointIndex = StartIndex;
            ActivateNext();
        }

        private void OnCheckpointReached()
        {
            CheckpointReached?.Invoke();
            DisableCurrent();
            ActivateNext();
        }

        private void DisableCurrent()
        {
            _checkpoints[_activeCheckpointIndex].enabled = false;
        }

        private void ActivateNext()
        {
            _activeCheckpointIndex++;

            if (IsChainComplete)
            {
                ChainComplete?.Invoke();
                return;
            }

            _checkpoints[_activeCheckpointIndex].enabled = true;
        }

        private void Validate()
        {
            for (int i = 0; i < _checkpoints.Length; i++)
            {
                if (_checkpoints[i].Index == _checkpoints[i + 1].Index)
                {
                    throw new Exception(SomeCheckpointIndicesAreEquals);
                }
            }
        }

        private void Sort()
        {
            _checkpoints = _checkpoints.OrderBy(checkpoint => checkpoint.Index).ToArray();
        }
    }
}
