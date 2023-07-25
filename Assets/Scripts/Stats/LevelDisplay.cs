using UnityEngine;
using TMPro;
using RPG.Saving;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        int currentLevel;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            currentLevel = baseStats.GetLevel();
            GetComponent<TextMeshProUGUI>().text = string.Format("{0:0}", currentLevel);
        }
    }
}