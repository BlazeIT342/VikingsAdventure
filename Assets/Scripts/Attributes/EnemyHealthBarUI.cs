using UnityEngine.UI;
using UnityEngine;
using RPG.Combat;
using RPG.Stats;

namespace RPG.Attributes
{
    public class EnemyHealthBarUI : MonoBehaviour
    {
        [SerializeField] AvatarCharacterClass[] characterClasses = null;

        [SerializeField] Slider enemyHealthSlider;
        [SerializeField] GameObject enemyBar;
        [SerializeField] GameObject enemyAvatar = null;
        Fighter fighter;


        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            Health target = fighter.GetTarget();
            if (target == null)
            {
                enemyBar.SetActive(false);
                return;
            }
            enemyBar.SetActive(true);
            foreach (AvatarCharacterClass characterClass in characterClasses) 
            {
                if (target.GetComponent<BaseStats>().GetCharacterClass() == characterClass.characterClass)
                {
                    enemyAvatar.GetComponent<Image>().sprite = characterClass.avatar;
                }
            }
            enemyHealthSlider.maxValue = target.GetMaxHealthPoints();
            enemyHealthSlider.value = target.GetHealthPoints();
        }

        [System.Serializable]
        class AvatarCharacterClass
        {
            public CharacterClass characterClass;
            public Sprite avatar;
        }
    }
}