using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Quest")]
public class QuestDescriptor : ScriptableObject
{
    public string title;
    public string description;
    public string target; // onCollected event
    public int goal;
}
