using System.Collections.Generic;
using RPG.Inventories;
using RPG.Utils;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "RPG Project/Quests/New Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objective> objectives = new List<Objective>();
        [SerializeField] List<Reward> rewards = new List<Reward>();

        [System.Serializable]
        public class Objective
        {
            public string reference;
            public string description;
            public bool usesCondition;
            public Condition completionCondition;
        }

        [System.Serializable]
        public class Reward
        {
            [Min(1)]
            public int number;
            public InventoryItem item;
        }

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectivesCount()
        {
            return objectives.Count;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }

        public bool HasObjective(string objectiveReference)
        {
            foreach (var objective in objectives) 
            {
                if (objective.reference == objectiveReference)
                {
                    return true;
                }
            }
            return false;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName) 
                {
                    return quest;
                }               
            }
            return null;
        }
    }
}