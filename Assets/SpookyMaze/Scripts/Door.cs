using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private EDoorType doorType;
        [SerializeField] private float animationSeconds;
        
        private Animator _animator;
        private CancellationTokenSource _openCancellationTokenSource;
        
        public EDoorType DoorType => doorType;

        public bool IsOpened { get; private set; } = false;
        public bool IsLocked { get; set; } = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public async UniTask Close(bool bypassLock)
        {
            if ((!IsLocked || bypassLock) && IsOpened)
            {
                CancelProcess();
                _animator.Play("Close");
                IsOpened = false;
                
                await UniTask.Delay((int) animationSeconds * 1000, DelayType.DeltaTime,
                    cancellationToken: _openCancellationTokenSource.Token);
                _openCancellationTokenSource?.Cancel();
            }
        }

        public async UniTask Open(bool bypassLock)
        {
            if ((!IsLocked || bypassLock) && !IsOpened)
            {
                CancelProcess();
                _animator.Play("Open");
                IsOpened = true;
                
                await UniTask.Delay((int) animationSeconds * 1000, DelayType.DeltaTime,
                    cancellationToken: _openCancellationTokenSource.Token);
                _openCancellationTokenSource?.Cancel();
            }
        }
        
        public async UniTask Switch(bool bypassLock)
        {
            if (IsOpened)
            {
                await Close(bypassLock);
            }
            else
            {
                await Open(bypassLock);
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
        
        #region RoomsLogic
        
        private readonly List<Room> _connectedRooms = new List<Room>(2);
        public ReadOnlyCollection<Room> ConnectedRooms => _connectedRooms.AsReadOnly();
        public void ConnectRoom(Room room)
        {
            _connectedRooms.Add(room);
        }
        
        #endregion
    }
}
