using System.Threading;
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
        
        private PlayableDirector _playableDirector;
        private CancellationTokenSource _openCancellationTokenSource;

        public bool IsOpened { get; private set; }
        public EDoorType DoorType => doorType;
        public Room[] ConnectedRooms => connectedRooms;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public async UniTask Open(bool open)
        {
            Debug.Log($"[{nameof(Door)}] {nameof(Open)} called.");
            
            // Perform only if not in process right now
            if (_openCancellationTokenSource == null || _openCancellationTokenSource.IsCancellationRequested)
            {
                _openCancellationTokenSource?.Cancel();
                _openCancellationTokenSource?.Dispose();
                _openCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());

                if (open)
                {
                    AnimationPlayableUtilities.PlayClip(_animator, openDoorClip, out _playableGraph);
                
                    await UniTask.Delay((int)openDoorClip.length, DelayType.DeltaTime, 
                        cancellationToken: _openCancellationTokenSource.Token);
                    _openCancellationTokenSource?.Cancel();
                }
                else
                {
                    AnimationPlayableUtilities.PlayClip(_animator, closeDoorClip, out _playableGraph);
                
                    await UniTask.Delay((int)closeDoorClip.length, DelayType.DeltaTime,
                        cancellationToken: _openCancellationTokenSource.Token);
                    _openCancellationTokenSource?.Cancel();
                }
            
                IsOpened = open;
            }
        }

        public enum EDoorType
        {
            Default,
            Exit,
            Enter
        }
    }
}
