using UnityEngine;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
