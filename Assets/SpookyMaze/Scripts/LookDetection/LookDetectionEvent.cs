using System;
using UnityEngine;

namespace SpookyMaze.Scripts.LookDetection
{
    [CreateAssetMenu]
    public class LookDetectionEvent : ScriptableObject
    {
        [SerializeField] private string objectTag;
        
        public string ObjectTag => objectTag;
        public GameObject LookingAt { get; private set; }

        public event Action<GameObject, bool> LookDetected; 

        public void Raise(GameObject gameObject, bool isLookingAt)
        {
            LookingAt = isLookingAt ? gameObject : null;
            LookDetected?.Invoke(gameObject, isLookingAt);
        }

        public void ResetState()
        {
            LookingAt = null;
        }
    }
}