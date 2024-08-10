using System.Collections.Generic;
using UnityEngine;

public class Consumables : DropTables
{
    [SerializeField][Min(1)] int amount = 1;
    List<int> consumables;
    CreateInventory mySpriteInventory;
    SpriteRenderer mySpriteRenderer;

    void Awake()
    {
        mySpriteInventory = gameObject.GetComponent<CreateInventory>();
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        consumables = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            consumables.Add((int)EItemCategories.Food);
        }
    }
    public override List<int> GetObjects()
    {
        mySpriteRenderer.sprite = mySpriteInventory.SpriteStateOpen;
        isAlreadyOpen = true;
        return consumables;
    }
}
