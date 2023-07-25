using UnityEngine;
using RPG.Quests;
using TMPro;

namespace RPG.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI progress;

        QuestStatus status;

        public void Setup(QuestStatus status)
        {
            this.status = status;
            title.text = status.GetQuest().GetTitle();
            progress.text = status.GetCompletedCount() + "/" + status.GetQuest().GetObjectivesCount();
        }

        public QuestStatus GetQuestStatus()
        {
            return status;
        }
    }
}