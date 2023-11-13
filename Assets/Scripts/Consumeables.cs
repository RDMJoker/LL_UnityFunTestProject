using System.Collections.Generic;
using UnityEngine;

public class Consumeables : DropTables
{
    [SerializeField][Min(1)] int amount = 1;
    List<int> consumeables;
    private CreateInventory _mySpriteInventory;
    private SpriteRenderer _mySpriteRenderer;

    private void Awake()
    {
        _mySpriteInventory = this.gameObject.GetComponent<CreateInventory>();
        _mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        consumeables = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            consumeables.Add((int)Item.EItemCategories.food);
        }
    }
    public override List<int> GetObjects()
    {
        _mySpriteRenderer.sprite = _mySpriteInventory.SpriteStateOpen;
        isAlreadyOpen = true;
        return consumeables;
    }
}
