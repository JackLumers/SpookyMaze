using SpookyMaze.Scripts.Events;
using UnityEngine;

namespace SpookyMaze.Scripts.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private UiDoorOpenHint doorOpenHint;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private LookDetectionEvent doorLookDetection;
        [SerializeField] private PlayerKilledEvent playerKilledEvent;
        [SerializeField] private WinEvent winEvent;
        
        private void Awake()
        {
            doorLookDetection.LookDetected += DoorLookDetected;
            playerKilledEvent.Killed += OnPlayerKilled;
            winEvent.Won += OnWin;
        }

        private void OnWin()
        {
            loseScreen.gameObject.SetActive(true);
        }

        private void OnPlayerKilled()
        {
            winScreen.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            doorLookDetection.LookDetected -= DoorLookDetected;
        }

        private void DoorLookDetected(GameObject doorObject, bool isLookingAt)
        {
            if (isLookingAt)
            {
                Door door = doorObject.GetComponent<Door>();
                if (door.IsLocked)
                {
                    doorOpenHint.ShowHint("Заперто");
                }
                else if(!door.IsOpened)
                {
                    doorOpenHint.ShowHint($"[E] - Открыть дверь #{doorObject.GetInstanceID()}");
                }
                else
                {
                    doorOpenHint.HideHint();
                }
            }
            else
            {
                doorOpenHint.HideHint();
            }
        }
    }
}