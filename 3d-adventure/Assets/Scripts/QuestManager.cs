using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public Action<Quest> onQuestStarted, onQuestUpdated, onQuestCompleted;
    readonly List<Quest> active = new();
    IReadOnlyList<Quest> Active => active;
    readonly HashSet<QuestDescriptor> completed = new();

    List<QuestDescriptor> startingQuests = new();

    private void Awake() => instance = this;

    private void OnEnable()  => Events.onCollected += HandleCollected;
    private void OnDisable() => Events.onCollected -= HandleCollected;

    private void Start()
    {
        foreach (var q in startingQuests) StartQuest(q);
    }

    public bool StartQuest(QuestDescriptor questDescriptor)
    {
        if (completed.Contains(questDescriptor)) return false;
        if (active.Exists(q => q.questDescriptor == questDescriptor)) return false;

        var quest = new Quest(questDescriptor);
        active.Add(quest);
        onQuestStarted?.Invoke(quest);

        return true;
    }

    private void HandleCollected(string item, int amount)
    {
        for (int i = active.Count - 1; i >= 0; i--)
        {
            var quest = active[i];
            if (quest.questDescriptor.target != item) continue;

            quest.progress = Mathf.Min(quest.progress + amount, quest.questDescriptor.goal);
            onQuestUpdated?.Invoke(quest);

            if (quest.isCompleted)
            {
                active.RemoveAt(i);
                completed.Add(quest.questDescriptor);
                onQuestCompleted?.Invoke(quest);
            }
        }
    }
}
