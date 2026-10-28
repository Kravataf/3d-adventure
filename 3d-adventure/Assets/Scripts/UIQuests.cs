using System.Collections.Generic;
using UnityEngine;

public class UIQuests : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questLog;

    private Dictionary<Quest, GameObject> entries = new();

    private void OnEnable()
    {
        QuestManager.instance.onQuestStarted += HandleQuestStarted;
        QuestManager.instance.onQuestUpdated += HandleQuestUpdated;
        QuestManager.instance.onQuestCompleted += HandleQuestCompleted;
    }

    private void OnDisable()
    {
        QuestManager.instance.onQuestStarted -= HandleQuestStarted;
        QuestManager.instance.onQuestUpdated -= HandleQuestUpdated;
        QuestManager.instance.onQuestCompleted -= HandleQuestCompleted;
    }

    private void HandleQuestStarted(Quest quest)
    {
        if (entries.ContainsKey(quest)) return;

        var entry = Instantiate(questPrefab, questLog);
        entries.Add(quest, entry);
    }

    private void HandleQuestUpdated(Quest quest)
    {}

    private void HandleQuestCompleted(Quest quest)
    {
        if (entries.Remove(quest, out var entry)) Destroy(entry);
    }
}
