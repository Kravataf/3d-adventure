public class Quest
{
    public readonly QuestDescriptor questDescriptor;
    public int progress;
    public bool isCompleted => progress >= questDescriptor.goal;

    public Quest(QuestDescriptor questDescriptor) => this.questDescriptor = questDescriptor;
}
