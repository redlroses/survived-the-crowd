using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Enemy
{
    public sealed class AgentIdleMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AgentIdleArea _idleArea;
        [SerializeField] private float _moveDelay;
        [SerializeField] private float _speed;

        private NavMeshPath _path;
        private WaitForSeconds _waitForNewPath;
        private WaitUntil _waitUntilHasPath;
        private Coroutine _setNewPath;
        private bool _isIdleMove;

        private void Awake()
        {
            _setNewPath = null;
            _path = new NavMeshPath();
            _waitUntilHasPath = new WaitUntil(IsStopped);
            _waitForNewPath = new WaitForSeconds(_moveDelay);
        }

        private void OnEnable()
        {
            StartIdeMoving();
        }

        private void OnDisable()
        {
            StopIdleMoving();
        }

        private void StartIdeMoving()
        {
            Debug.Log(name + " on enable");
            _agent.speed = _speed;
            _isIdleMove = true;
            _setNewPath ??= StartCoroutine(SetNewPath());
        }

        private void StopIdleMoving()
        {
            _isIdleMove = false;
            StopCoroutine(_setNewPath);
            _setNewPath = null;
        }

        private IEnumerator SetNewPath()
        {
            while (_isIdleMove)
            {
                Debug.Log("before wait");
                
                yield return _waitForNewPath;
                yield return _waitUntilHasPath;

                Debug.Log("after wait");
                
                Vector3 nextPosition = _idleArea.GetRandomPosition();

                Debug.Log("nextPosition " + nextPosition);
                
                if (_agent.CalculatePath(nextPosition, _path))
                {
                    Debug.Log("is calcilate path");
                    _agent.SetPath(_path);
                }
            }
        }

        private bool IsStopped()
            => _agent.remainingDistance <= _agent.stoppingDistance;
    }
}
