using UnityEngine;

namespace RPG.Attributes
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;

        private void Update()
        {
            if(Mathf.Approximately(healthComponent.GetFraction(), 0)
            || Mathf.Approximately(healthComponent.GetFraction(), 1)
            || healthComponent.GetHealthPoints() < 0)
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1.0f, 1.0f);
        }
    }
}