


public interface ITask
{
    bool Enabled { get; set; }
    IBehaviorTree ParentTree { get; set; }
    TaskStatus LastStatus { get; }
    TaskStatus Update(float inDeltaTime);
    void End();
    void Reset();
}
