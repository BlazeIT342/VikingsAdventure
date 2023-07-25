using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class StatsDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI strengthText;
        [SerializeField] TextMeshProUGUI defenceText;
        [SerializeField] TextMeshProUGUI intelligenceText;

        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            strengthText.text = string.Format("{0:0}", baseStats.GetStat(Stat.Damage));
            defenceText.text = string.Format("{0:0}", baseStats.GetStat(Stat.Defence));
            intelligenceText.text = string.Format("{0:0}", (baseStats.GetStat(Stat.Mana) / 20));
        }
    }
}