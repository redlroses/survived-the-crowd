using System.Linq;
using UnityEngine;

namespace Sources.Level.Infrastructure
{
    public sealed class CheckpointChainView : MonoBehaviour
    {
        [SerializeField] private CheckpointsChain _checkpointsChain;
        [SerializeField] private Transform _startZone;
        [SerializeField] private Transform _finishZone;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(_startZone.position, _checkpointsChain.CheckpointsPosition[0]);

            for (var i = 0; i < _checkpointsChain.Size - 1; i++)
            {
                var from = _checkpointsChain.CheckpointsPosition[i];
                var to = _checkpointsChain.CheckpointsPosition[i + 1];
                Gizmos.DrawLine(from, to);
            }

            Gizmos.DrawLine(_checkpointsChain.CheckpointsPosition.Last(), _finishZone.position);
            Gizmos.color = Color.white;
        }
    }
}