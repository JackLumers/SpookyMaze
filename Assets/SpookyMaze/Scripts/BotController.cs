using UnityEngine;
using UnityEngine.AI;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BotController : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            SetTarget(target);
        }

        public void SetTarget(GameObject targetToSet)
        {
            target = targetToSet;
            Vector3 targetPosition = target.transform.position;
            
            if (_agent.SetDestination(target.transform.position))
            {
                Debug.Log($"[{nameof(BotController)}] Target set. " +
                          $"Target name: {target.name}. " +
                          $"Target Position: {targetPosition}");
            }
            else
            {
                Debug.Log($"[{nameof(BotController)}] Destination wasn't requested successfully. " +
                          $"Target name: {target.name}. " +
                          $"Target Position: {targetPosition}");
            }
        }
    }
}