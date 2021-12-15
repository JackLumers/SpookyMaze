using System;
using UnityEngine;

namespace SpookyMaze.Scripts.Events
{
    [CreateAssetMenu]
    public class WinEvent : ScriptableObject
    {
        public event Action Won;
        
        public void Raise()
        {
            Won?.Invoke();
        }
        
        public void ResetState()
        {
            Won = null;
        }
    }
}