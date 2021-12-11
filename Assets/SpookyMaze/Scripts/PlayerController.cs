using Cysharp.Threading.Tasks;
using SpookyMaze.Scripts.LookDetection;
using StarterAssets;
using UnityEngine;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(ThirdPersonController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LookDetector lookDetector;

        /// <summary>
        /// Checks if player looking at the door and tries to open it.
        /// </summary>
        public void TryOpenDoor()
        {
            if (lookDetector.LookingAt != null)
            {
                Door door = lookDetector.LookingAt.GetComponent<Door>();
                if (door != null)
                {
                    door.Open(true).Forget();
                }
            }
        }
    }
}