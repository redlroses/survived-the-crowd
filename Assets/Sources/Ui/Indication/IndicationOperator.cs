using System.Collections.Generic;
using System.Linq;
using Sources.Collectables;
using Sources.Pool;
using UnityEngine;

namespace Sources.Ui.Indication
{
    public class IndicationOperator : MonoBehaviour
    {
        [SerializeField] private DirectionIndicator[] _carDetailsDirectionIndicators;
        [SerializeField] private CarDetailsPool _carDetailsPool;
        [SerializeField] private FuelBarrelPool _fuelBarrelPool;
        [SerializeField] private DirectionIndicator[] _fuelDirectionIndicators;
        [SerializeField] private Transform _playerTransform;

        private int _currentActiveIndicatorsCount;

        private void FixedUpdate()
        {
            SetFuelTargets();
            SetCarDetailsTarget();
        }

        private void SetCarDetailsTarget()
        {
            Transform[] sortedTargets = GetSortedTargets(_carDetailsPool.GetActiveObjects());
            SetTargets(sortedTargets, _carDetailsDirectionIndicators);
        }

        private void SetFuelTargets()
        {
            Transform[] sortedTargets = GetSortedTargets(_fuelBarrelPool.GetActiveObjects());
            SetTargets(sortedTargets, _fuelDirectionIndicators);
        }

        private void SetTargets(Transform[] sortedTargets, DirectionIndicator[] containers)
        {
            int activeIndicatorsCount = Mathf.Min(sortedTargets.Length, containers.Length);

            for (int i = 0; i < activeIndicatorsCount; i++)
            {
                containers[i].Activate(sortedTargets[i]);
            }

            for (int i = activeIndicatorsCount; i < containers.Length; i++)
            {
                containers[i].Deactivate();
            }
        }

        private Transform[] GetSortedTargets<T>(IEnumerable<T> targets)
            where T : MonoBehaviour
        {
            Transform[] sorted = targets.Select(target => target.transform)
                .OrderBy(
                    target =>
                        Vector3.Distance(target.position, _playerTransform.position))
                .ToArray();

            return sorted;
        }
    }
}