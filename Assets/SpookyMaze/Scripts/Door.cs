using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private Room[] connectedRooms;
        [SerializeField] private EDoorType doorType;
        [SerializeField] private float animationSeconds;
        
        private Animator _animator;
        private CancellationTokenSource _openCancellationTokenSource;

        public bool IsOpened { get; private set; }
        public EDoorType DoorType => doorType;
        public Room[] ConnectedRooms => connectedRooms;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public async UniTask Close()
        {
            CancelProcess();
            _animator.Play("Close");
            
            await UniTask.Delay((int) animationSeconds * 1000, DelayType.DeltaTime,
                cancellationToken: _openCancellationTokenSource.Token);
            _openCancellationTokenSource?.Cancel();
        }
        
        public async UniTask Open()
        {
            CancelProcess();
            _animator.Play("Open");

            await UniTask.Delay((int) animationSeconds * 1000, DelayType.DeltaTime,
                cancellationToken: _openCancellationTokenSource.Token);
            _openCancellationTokenSource?.Cancel();
        }
        
        public async UniTask Switch()
        {
            if (IsOpened)
            {
                await Close();
            }
            else
            {
                await Open();
            }

            IsOpened = !IsOpened;
        }

        private void CancelProcess()
        {
            _openCancellationTokenSource?.Cancel();
            _openCancellationTokenSource?.Dispose();
            _openCancellationTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(this.GetCancellationTokenOnDestroy());
        }
        
        public enum EDoorType
        {
            Default,
            Exit,
            Enter
        }
    }
}
