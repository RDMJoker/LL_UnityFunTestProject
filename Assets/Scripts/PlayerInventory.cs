using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<int> inventory;
    public int CurrentFoodCount { get; private set; }

    void Awake()
    {
        inventory = new List<int>();
    }
    
    public void ReceiveItems(List<int> _returnList)
    {
        CurrentFoodCount = 0;
        inventory.AddRange(_returnList);
        foreach (int item in inventory.Where(_item => _item == 1))
        {
            Debug.Log("This is food!");
            CurrentFoodCount++;
        }
    }

    public bool EatFood()
    {
        foreach (int item in inventory.Where(_item => _item == 1))
        {
            inventory.Remove(item);
            GetComponent<PlayerMovementTopDown>().IncreaseMoveSpeed(1);
            CurrentFoodCount--;
            Debug.Log("You ate food!");
            return true;
        }
        return false;
    }
}