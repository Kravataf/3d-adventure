using System.Collections.Generic;
using UnityEngine;

public class UIQuests : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questLog;

    private Dictionary<Quest, GameObject> entries = new();

    private void OnEnable()
    {
        QuestManager.onQuestStarted += HandleQuestStarted;
        QuestManager.onQuestUpdated += HandleQuestUpdated;
        QuestManager.onQuestCompleted += HandleQuestCompleted;

        Refresh();
    }

    private void OnDisable()
    {
        QuestManager.onQuestStarted -= HandleQuestStarted;
        QuestManager.onQuestUpdated -= HandleQuestUpdated;
        QuestManager.onQuestCompleted -= HandleQuestCompleted;
    }

    private void Refresh()
    {
        foreach (var quest in entries.Values) Destroy(quest.gameObject);
        entries.Clear();

        foreach (var quest in QuestManager.instance.Active)
        {
            var entry = Instantiate(questPrefab, questLog);
            entries.Add(quest, entry);
            var title = entry.transform.Find("Title").GetComponent<TMPro.TMP_Text>();
            var progress = entry.transform.Find("Description").GetComponent<TMPro.TMP_Text>();

            title.text = quest.questDescriptor.title;
            progress.text = $"{quest.progress}/{quest.questDescriptor.goal} {quest.questDescriptor.target}";
            progress.text += $"\n{quest.questDescriptor.description}";
        }
    }

    private void HandleQuestStarted(Quest quest)
    {
        if (entries.ContainsKey(quest)) return;

        var entry = Instantiate(questPrefab, questLog);
        entries.Add(quest, entry);
    }

    private void HandleQuestUpdated(Quest quest)
    {
        if (entries.TryGetValue(quest, out var entry))
        {
            var title = entry.transform.Find("Title").GetComponent<TMPro.TMP_Text>();
            var progress = entry.transform.Find("Description").GetComponent<TMPro.TMP_Text>();

            title.text = quest.questDescriptor.title;
            progress.text = $"{quest.progress}/{quest.questDescriptor.goal} {quest.questDescriptor.target}";
            progress.text += $"\n{quest.questDescriptor.description}";
        }
    }

    private void HandleQuestCompleted(Quest quest)
    {
        if (entries.Remove(quest, out var entry)) Destroy(entry);
    }
}
