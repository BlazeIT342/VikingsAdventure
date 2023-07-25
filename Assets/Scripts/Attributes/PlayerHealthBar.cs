using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class PlayerHealthBar : MonoBehaviour
    {
        Slider playerHealthSlider;
        Health health;

        private void Start()
        {
            playerHealthSlider = GetComponent<Slider>();
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            playerHealthSlider.maxValue = health.GetMaxHealthPoints();
            playerHealthSlider.value = health.GetHealthPoints();
        }
    }
}