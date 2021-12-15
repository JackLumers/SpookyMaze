using System;
using Cysharp.Threading.Tasks;
using SpookyMaze.Scripts.Events;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(ThirdPersonController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LookDetectionEvent doorLookDetection;
        [SerializeField] private InputAction openDoorAction;
        [SerializeField] private PlayerKilledEvent playerKilledEvent;
        
        public event Action<Door> OpensDoor;
        
        private void Awake()
        {
            openDoorAction.Enable();
            openDoorAction.performed += context => TryOpenDoor();
        }

        private void OnDestroy()
        {
            playerKilledEvent.ResetState();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                playerKilledEvent.Raise();
                gameObject.SetActive(false);
            }
        }
        
        /// <summary>
        /// Checks if player looking at the door and tries to open it.
        /// </summary>
        private void TryOpenDoor()
        {
            Debug.Log($"[{nameof(PlayerController)}] {nameof(TryOpenDoor)} called.");
            if (doorLookDetection.LookingAt != null)
            {
                Door door = doorLookDetection.LookingAt.GetComponent<Door>();
                if (door != null)
                {
                    if (!door.IsLocked && !door.IsOpened)
                    {
                        door.Open(false).Forget();
                        OpensDoor?.Invoke(door);
                    }
                }
            }
        }
    }
}