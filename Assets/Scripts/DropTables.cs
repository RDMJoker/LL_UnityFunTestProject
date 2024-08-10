using System.Collections.Generic;
using UnityEngine;

public class DropTables : MonoBehaviour
{
    readonly List<int> defaultList = new();
    protected bool isAlreadyOpen = false;
    public bool IsAlreadyOpen => isAlreadyOpen;
    public virtual List<int> GetObjects()
    {
        return defaultList;
    }
}
