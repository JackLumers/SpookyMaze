using SpookyMaze.Scripts.LookDetection;
using UnityEngine;
using UnityEngine.UI;

namespace SpookyMaze.Scripts.UI
{
    [RequireComponent(typeof(LookListener))]
    public class UiDoorOpenHint : MonoBehaviour
    {
        [SerializeField] private Text hintText;

        private LookListener _lookListener;

        private void Awake()
        {
            _lookListener = GetComponent<LookListener>();
            _lookListener.response.AddListener(Call);
        }

        private void Call(GameObject door, bool isLookingAt)
        {
            if (isLookingAt)
            {
                ShowHint($"[E] - Open door #{door.GetInstanceID()}");
            }
            else
            {
                HideHint();
            }
        }
        
        public void ShowHint(string text)
        {
            hintText.text = text;
            gameObject.SetActive(true);
        }

        public void HideHint()
        {
            gameObject.SetActive(false);
        }
    }
}