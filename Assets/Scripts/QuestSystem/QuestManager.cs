using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [System.Serializable]
    public class QuestRecord
    {
        public QuestData questData;
        public QuestStatus status;
    }

    public List<QuestRecord> quests = new List<QuestRecord>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadQuestProgress();
    }

    public void StartQuest(int questID)
    {
        QuestRecord record = quests.Find(q => q.questData.questID == questID);
        if (record != null)
        {
            record.status = QuestStatus.Active;
            SaveQuestProgress();
        }
        QuestUIManager ui = FindFirstObjectByType<QuestUIManager>();
        if (ui != null) ui.UpdateUI();
    }

    public void CompleteQuest(int questID)
    {
        QuestRecord record = quests.Find(q => q.questData.questID == questID);
        if (record != null)
        {
            record.status = QuestStatus.Completed;
            SaveQuestProgress();
        }
        QuestUIManager ui = FindFirstObjectByType<QuestUIManager>();
        if (ui != null) ui.UpdateUI();
    }

    public QuestStatus GetQuestStatus(int questID)
    {
        QuestRecord record = quests.Find(q => q.questData.questID == questID);
        return record != null ? record.status : QuestStatus.Inactive;
    }

    public bool IsQuestActive(int questID) => GetQuestStatus(questID) == QuestStatus.Active;

    public void SaveQuestProgress()
    {
        foreach (var record in quests)
        {
            PlayerPrefs.SetInt($"Quest_{record.questData.questID}", (int)record.status);
        }
        PlayerPrefs.Save();
    }

    public void LoadQuestProgress()
    {
        foreach (var record in quests)
        {
            int savedStatus = PlayerPrefs.GetInt($"Quest_{record.questData.questID}", (int)record.questData.defaultStatus);
            record.status = (QuestStatus)savedStatus;
        }
    }
}
