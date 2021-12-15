using UnityEngine;
using UnityEngine.AI;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BotController : MonoBehaviour
    {
        private GameObject _target;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(!ReferenceEquals(_target, null)) 
                SetTarget(_target);
        }

        public void SetTarget(GameObject targetToSet)
        {
            _target = targetToSet;
            
            //Vector3 targetPosition = _target.transform.position;
            // if (_agent.SetDestination(_target.transform.position))
            // {
            //     Debug.Log($"[{nameof(BotController)}] Target set. " +
            //               $"Target name: {_target.name}. " +
            //               $"Target Position: {targetPosition}");
            // }
            // else
            // {
            //     Debug.Log($"[{nameof(BotController)}] Destination wasn't requested successfully. " +
            //               $"Target name: {_target.name}. " +
            //               $"Target Position: {targetPosition}");
            // }
        }

        public void ReachTargetIfPossible(GameObject target)
        {
            _agent.SetDestination(target.transform.position);
            
            if (_agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                _target = target;
            }
            else
            {
                // Reset to previous target
                SetTarget(_target);
            }
        }
    }
}