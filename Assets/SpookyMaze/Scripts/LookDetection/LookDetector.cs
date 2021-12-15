using SpookyMaze.Scripts.Events;
using UnityEngine;

namespace SpookyMaze.Scripts.LookDetection
{
    [RequireComponent(typeof(Collider))]
    public class LookDetector : MonoBehaviour
    {
        [SerializeField] private LookDetectionEvent[] lookDetectionEvents;

        private void OnTriggerEnter(Collider other)
        {   
            Debug.Log($"[{nameof(LookDetector)}] Collision with object '{other.gameObject.name}'");
            
            foreach (var lookDetectionEvent in lookDetectionEvents)
            {
                if (other.gameObject.CompareTag(lookDetectionEvent.ObjectTag))
                {
                    lookDetectionEvent.Raise(other.gameObject, true);
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"[{nameof(LookDetector)}] Collision with object '{other.gameObject.name}'");
            
            foreach (var lookDetectionEvent in lookDetectionEvents)
            {
                if (other.gameObject.CompareTag(lookDetectionEvent.ObjectTag))
                {
                    lookDetectionEvent.Raise(other.gameObject, false);
                }
            }
        }
        
        private void OnDestroy()
        {
            foreach (var lookDetectionEvent in lookDetectionEvents)
            {
                lookDetectionEvent.ResetState();
            }
        }
    }
}