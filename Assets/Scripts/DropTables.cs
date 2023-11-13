using System.Collections.Generic;

public class DropTables : Item
{
    readonly List<int> defaultList = new List<int>();
    protected bool isAlreadyOpen = false;
    public bool IsAlreadyOpen => isAlreadyOpen;
    public virtual List<int> GetObjects()
    {
        return defaultList;
    }
}
