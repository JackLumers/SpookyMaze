using Cysharp.Threading.Tasks;
using SpookyMaze.Scripts.LookDetection;
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

        private void Awake()
        {
            openDoorAction.Enable();
            openDoorAction.performed += context => TryOpenDoor();
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
                    door.Open().Forget();
                }
            }
        }
    }
}