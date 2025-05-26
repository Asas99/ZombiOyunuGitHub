using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public int questID;
    public bool completeQuest = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (completeQuest)
                QuestManager.Instance.CompleteQuest(questID);
            else
                QuestManager.Instance.StartQuest(questID);
        }
    }
}
