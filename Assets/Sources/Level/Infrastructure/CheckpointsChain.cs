using System;
using System.Linq;
using UnityEngine;

namespace Level.Infrastructure
{
    public sealed class CheckpointsChain : MonoBehaviour
    {
        private const int StartIndex = -1;
        private const string SomeCheckpointIndicesAreEquals = "Some checkpoint indices are equals";

        [SerializeField] private StartZone _startZone;
        [SerializeField] private Checkpoint[] _checkpoints;

        private int _activeCheckpointIndex;

        public event Action CheckpointReached;
        public event Action ChainComplete;

        public int Size => _checkpoints.Length;
        public Vector3[] CheckpointsPosition => _checkpoints.Select(position => position.transform.position).ToArray();
        private bool IsChainComplete => _activeCheckpointIndex >= _checkpoints.Length;

        private void Awake()
        {
            Sort();
            Validate();
            DisableAll();
        }

        private void OnEnable()
        {
            _startZone.Left += Begin;

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

        private void Begin()
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
            _checkpoints[_activeCheckpointIndex].gameObject.SetActive(false);
        }

        private void DisableAll()
        {
            foreach (var checkpoint in _checkpoints)
            {
                checkpoint.gameObject.SetActive(false);
            }
        }

        private void ActivateNext()
        {
            _activeCheckpointIndex++;

            if (IsChainComplete)
            {
                ChainComplete?.Invoke();
                return;
            }

            _checkpoints[_activeCheckpointIndex].gameObject.SetActive(true);
        }

        private void Validate()
        {
            for (int i = 1; i < _checkpoints.Length; i++)
            {
                if (_checkpoints[i - 1].Index == _checkpoints[i].Index)
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
