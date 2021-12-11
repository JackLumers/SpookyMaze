using System.Collections.Generic;
using UnityEngine;

namespace SpookyMaze.Scripts.LookDetection
{
    [CreateAssetMenu]
    public class LookDetectionEvent : ScriptableObject
    {
        [SerializeField] private string objectTag;
        
        private List<LookListener> _listeners = new List<LookListener>();

        public string ObjectTag => objectTag;

        public void Raise(GameObject gameObject, bool isLookingAt)
        {
            for (var i = _listeners.Count - 1; i <= 0; i--)
            {
                var listener = _listeners[i];
                listener.OnEventRaised(gameObject, isLookingAt);
            }
        }

        public void RegisterListener(LookListener listener)
        {
            _listeners.Add(listener);
        }

        public void UnregisterListener(LookListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}