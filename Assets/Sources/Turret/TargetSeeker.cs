using System;
using Sources.Level;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class TargetSeeker : Radar
    {
        private Transform _selfTransform;
        private Transform _closestTarget;
        private LoseDetector _loseDetector;
        private bool _isHasTarget;

        public event Action<Transform> TargetUpdated;
        public event Action TargetLost;
        public event Action TargetFound;

        private void Start()
        {
            _selfTransform = transform;
        }

        private void OnEnable()
        {
            Updated += OnTargetsUpdated;
            _loseDetector.Lose += OnLose;
            StartScan();
        }

        private void OnDisable()
        {
            Updated -= OnTargetsUpdated;
            _loseDetector.Lose -= OnLose;
            StopScan();
        }

        public void Init(LoseDetector loseDetector)
        {
            _loseDetector = loseDetector;
        }

        private void OnTargetsUpdated()
        {
            if (TargetsCount != 0)
            {
                TryUpdateTarget(GetClosest());
            }

            InvokeStateEvents(TargetsCount);
        }

        private void OnLose()
        {
            StopScan();
            TargetLost?.Invoke();
        }

        private Transform GetClosest()
        {
            var minDistance = float.MaxValue;
            Transform closest = null;

            foreach (var target in Targets)
            {
                if (target is null)
                {
                    break;
                }

                float distanceToTarget = Vector3.Distance(_selfTransform.position, target.position);

                if (distanceToTarget >= minDistance)
                {
                    continue;
                }

                minDistance = distanceToTarget;
                closest = target;
            }

            if (closest is null)
            {
                throw new NullReferenceException();
            }

            return closest;
        }

        private void TryUpdateTarget(Transform newClosest)
        {
            if (newClosest == _closestTarget)
            {
                return;
            }

            _closestTarget = newClosest;
            TargetUpdated?.Invoke(_closestTarget);
        }

        private void InvokeStateEvents(int targetsCount)
        {
            if (targetsCount == 0)
            {
                if (_isHasTarget)
                {
                    TargetLost?.Invoke();
                    _isHasTarget = false;
                }
            }
            else
            {
                if (_isHasTarget == false)
                {
                    TargetFound?.Invoke();
                    _isHasTarget = true;
                }
            }
        }
    }
}