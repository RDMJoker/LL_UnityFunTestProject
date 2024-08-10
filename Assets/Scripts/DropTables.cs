using System.Collections.Generic;

public class DropTables : Item
{
    readonly List<int> defaultList = new();
    protected bool isAlreadyOpen = false;
    public bool IsAlreadyOpen => isAlreadyOpen;
    public virtual List<int> GetObjects()
    {
        return defaultList;
    }
}
