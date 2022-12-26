using System.Linq;
using Sources.Pool;
using UnityEngine;

namespace Sources.Ui.Indication
{
    public class IndicationOperator : MonoBehaviour
    {
        [SerializeField] private FuelBarrelPool _fuelBarrelPool;
        [SerializeField] private DirectionIndicator[] _directionIndicators;
        [SerializeField] private Transform _playerTransform;

        private int _currentActiveIndicatorsCount;

        private void FixedUpdate()
        {
            SetTargets();
        }

        private void SetTargets()
        {
            Transform[] sortedTargets = GetSortedTargets();

            int activeIndicatorsCount = Mathf.Min(sortedTargets.Length, _directionIndicators.Length);

            for (int i = 0; i < activeIndicatorsCount; i++)
            {
                _directionIndicators[i].Activate(sortedTargets[i]);
            }

            for (int i = activeIndicatorsCount; i < _directionIndicators.Length; i++)
            {
                _directionIndicators[i].Deactivate();
            }
        }

        private Transform[] GetSortedTargets()
        {
            var a = _fuelBarrelPool.GetActiveObjects()
                .Select(target => target.transform)
                .OrderBy(target =>
                    Vector3.Distance(target.position, _playerTransform.position))
                .ToArray();
            return a;
        }
    }
}