using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestData : ScriptableObject
{
    public int questID;
    public string questName;
    public string description;
    public QuestStatus defaultStatus = QuestStatus.Inactive;
}
