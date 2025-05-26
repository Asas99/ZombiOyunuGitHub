using TMPro;
using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    public TextMeshProUGUI questText;

    void Start()
    {
        // QuestText objesini bul ve ba�la (atama yoksa)
        if (questText == null)
        {
            GameObject textObj = GameObject.Find("QuestText");
            if (textObj != null)
            {
                questText = textObj.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                Debug.LogWarning("QuestText isimli GameObject bulunamad�!");
            }
        }

        // UpdateUI �a�r�s�
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Null kontrol
        if (questText == null)
        {
            Debug.LogWarning("QuestText bile�eni atanmad�!");
            return;
        }

        if (QuestManager.Instance == null)
        {
            Debug.LogWarning("QuestManager.Instance null! QuestManager sahnede mi?");
            return;
        }

        string currentQuest = "";

        foreach (var quest in QuestManager.Instance.quests)
        {
            if (quest.status == QuestStatus.Active)
            {
                currentQuest = quest.questData.questName + ": " + quest.questData.description;
                break;
            }
        }

        questText.text = currentQuest;
    }
}
