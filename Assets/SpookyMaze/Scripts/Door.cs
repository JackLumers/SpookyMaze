using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private AnimationClip openDoorClip;
        [SerializeField] private AnimationClip closeDoorClip;
        [SerializeField] private Room[] connectedRooms;
        [SerializeField] private EDoorType doorType;
        
        private Animator _animator;
        private PlayableGraph _playableGraph;
        
        public bool IsOpened { get; private set; }
        public EDoorType DoorType => doorType;
        public Room[] ConnectedRooms => connectedRooms;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public async UniTask Open(bool open)
        {
            if (open)
            {
                AnimationPlayableUtilities.PlayClip(_animator, openDoorClip, out _playableGraph);
                _playableGraph.Play();
                
                await UniTask.Delay((int)openDoorClip.length, DelayType.DeltaTime);
            }
            else
            {
                AnimationPlayableUtilities.PlayClip(_animator, closeDoorClip, out _playableGraph);
                _playableGraph.Play();
                
                await UniTask.Delay((int)closeDoorClip.length, DelayType.DeltaTime);
            }
            
            IsOpened = open;
        }
        
        public enum EDoorType
        {
            Default,
            Exit,
            Enter
        }
    }
}
