using System;
using UnityEngine;

namespace SpookyMaze.Scripts.Events
{
    [CreateAssetMenu]
    public class PlayerKilledEvent : ScriptableObject
    {
        public event Action Killed;
        
        public void Raise()
        {
            Killed?.Invoke();
        }
        
        public void ResetState()
        {
            Killed = null;
        }
    }
}