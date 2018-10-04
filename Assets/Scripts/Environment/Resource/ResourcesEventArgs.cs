using System;

public class ResourcesEventArgs : EventArgs
{
    public int ResourceCount { get; private set; }

    public ResourcesEventArgs(int count)
    {
        ResourceCount = count;
    }
}
