
public class BehaviorTreeObject
{
    protected BehaviorTree tree = null;

    public void Update(float deltaTime)
    {
        tree.Tick(deltaTime);
    }
}
