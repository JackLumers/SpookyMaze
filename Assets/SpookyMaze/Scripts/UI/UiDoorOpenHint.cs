using UnityEngine;
using UnityEngine.UI;

namespace SpookyMaze.Scripts.UI
{
    public class UiDoorOpenHint : MonoBehaviour
    {
        [SerializeField] private Text hintText;

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