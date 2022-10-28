using System;

public class SelectorRandom : CompositeBase
{
    private bool _init;
    protected override TaskStatus OnUpdate()
    {
        if (!_init)
        {
            ShuffleChildren();
            _init = true;
        }

        for (int i = ChildIndex; i < Children.Count; i++)
        {
            ITask child = Children[ChildIndex];

            switch (child.Update(deltaTime))
            {
                case TaskStatus.Success:
                    return TaskStatus.Success;
                case TaskStatus.Continue:
                    return TaskStatus.Continue;
            }

            ChildIndex++;
        }

        return TaskStatus.Failure;
    }

    public override void Reset()
    {
        base.Reset();

        ShuffleChildren();
    }

    private void ShuffleChildren()
    {
        Random rng = new Random();
        int n = Children.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            ITask value = Children[k];
            Children[k] = Children[n];
            Children[n] = value;
        }
    }
}
