using SpookyMaze.Scripts.LookDetection;
using UnityEngine;

namespace SpookyMaze.Scripts.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private UiDoorOpenHint doorOpenHint;
        [SerializeField] private LookDetectionEvent doorLookDetection;
        
        private void Awake()
        {
            doorLookDetection.LookDetected += DoorLookDetected;
        }

        private void OnDestroy()
        {
            doorLookDetection.LookDetected -= DoorLookDetected;
        }

        private void DoorLookDetected(GameObject door, bool isLookingAt)
        {
            if (isLookingAt)
            {
                doorOpenHint.ShowHint($"[E] - Open door #{door.GetInstanceID()}");
            }
            else
            {
                doorOpenHint.HideHint();
            }
        }
    }
}