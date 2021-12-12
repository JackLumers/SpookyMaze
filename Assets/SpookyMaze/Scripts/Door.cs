using System;
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
        
        private Animator _animator;
        private PlayableGraph _playableGraph;
        private CancellationTokenSource _openCancellationTokenSource;
        private UniTask _openTask;

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
            if (_openTask.Status != UniTaskStatus.Pending)
            {
                _openCancellationTokenSource?.Cancel();
                _openCancellationTokenSource?.Dispose();
                _openCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());

                if (open)
                {
                    AnimationPlayableUtilities.PlayClip(_animator, openDoorClip, out _playableGraph);
                    _playableGraph.Play();
                
                    _openTask = UniTask.Delay((int)openDoorClip.length, DelayType.DeltaTime, 
                        cancellationToken: _openCancellationTokenSource.Token);
                    await _openTask;
                }
                else
                {
                    AnimationPlayableUtilities.PlayClip(_animator, closeDoorClip, out _playableGraph);
                    _playableGraph.Play();
                
                    _openTask = UniTask.Delay((int)closeDoorClip.length, DelayType.DeltaTime,
                        cancellationToken: _openCancellationTokenSource.Token);
                    await _openTask;
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
