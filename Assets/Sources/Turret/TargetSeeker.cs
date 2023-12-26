using System;
using Sources.Level;
using UnityEngine;

namespace Sources.Turret
{
    public sealed class TargetSeeker : Radar
    {
        private Transform _closestTarget;
        private bool _isHasTarget;
        private LoseDetector _loseDetector;
        private Transform _selfTransform;

        public event Action<Transform> TargetUpdated;

        public event Action TargetLosted;

        public event Action TargetFounded;

        protected override void Awake()
        {
            base.Awake();
            _selfTransform = transform;
        }

        private void OnEnable()
        {
            Updated += OnTargetsUpdated;

            if (_loseDetector != null)
            {
                _loseDetector.Losed += OnLosed;
            }

            StartScan();
        }

        private void OnDisable()
        {
            Updated -= OnTargetsUpdated;

            if (_loseDetector != null)
            {
                _loseDetector.Losed -= OnLosed;
            }

            StopScan();
        }

        public void Init(LoseDetector loseDetector)
        {
            _loseDetector = loseDetector;
        }

        public void OnRevived()
        {
            StartScan();
        }

        private void OnTargetsUpdated()
        {
            if (TargetsCount != 0)
            {
                TryUpdateTarget(GetClosest());
            }

            InvokeStateEvents(TargetsCount);
        }

        private void OnLosed()
        {
            StopScan();
            TargetLosted?.Invoke();
        }

        private Transform GetClosest()
        {
            float minDistance = float.MaxValue;
            Transform closest = null;

            foreach (Transform target in Targets)
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
                    TargetLosted?.Invoke();
                    _isHasTarget = false;
                }
            }
            else
            {
                if (_isHasTarget == false)
                {
                    TargetFounded?.Invoke();
                    _isHasTarget = true;
                }
            }
        }
    }
}