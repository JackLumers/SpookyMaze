using JetBrains.Annotations;
using UnityEngine;

namespace SpookyMaze.Scripts.LookDetection
{
    [RequireComponent(typeof(Collider))]
    public class LookDetector : MonoBehaviour
    {
        [CanBeNull] public GameObject LookingAt { get; private set; }
        [SerializeField] private LookDetectionEvent[] lookDetectionEvents;

        private void OnCollisionEnter(Collision other)
        {
            LookingAt = other.gameObject;
            Debug.Log($"Collision with object {other.gameObject.name}");
            foreach (var lookDetectionEvent in lookDetectionEvents)
            {
                if (other.gameObject.CompareTag(lookDetectionEvent.ObjectTag))
                {
                    lookDetectionEvent.Raise(gameObject, true);
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            LookingAt = null;
            foreach (var lookDetectionEvent in lookDetectionEvents)
            {
                if (other.gameObject.CompareTag(lookDetectionEvent.ObjectTag))
                {
                    lookDetectionEvent.Raise(gameObject, false);
                }
            }
        }
    }
}