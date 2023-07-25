using UnityEngine;

namespace RPG.UI
{
    public class ShowHideInLive : MonoBehaviour
    {
        [SerializeField] GameObject uiContainer = null;
        [SerializeField] bool active = false;

        private void Update()
        {
            if (uiContainer != null)
            {
                uiContainer.SetActive(active);
            }
        }
    }
}