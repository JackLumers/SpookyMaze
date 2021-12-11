using UnityEngine;
using UnityEngine.Events;

namespace SpookyMaze.Scripts.LookDetection
{
    public class LookListener : MonoBehaviour
    {
        public LookDetectionEvent lookEvent;
        public UnityEvent<GameObject, bool> response;
        
        private void Awake()
        {
            lookEvent.RegisterListener(this);
        }
        
        private void OnDestroy()
        {
            lookEvent.UnregisterListener(this);
        }

        public void OnEventRaised(GameObject obj, bool isLookingAt)
        {
            response.Invoke(obj, isLookingAt);
        }
    }
}