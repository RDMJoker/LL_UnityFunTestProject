using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    protected List<int> inventory;
    private int currentFoodCount = 0;
    public int CurrentFoodCount => currentFoodCount;
    private void Awake()
    {
        inventory = new List<int>();
    }

    public void RecieveItems(List<int> _returnList)
    {
        currentFoodCount = 0;
        this.inventory.AddRange(_returnList);
        foreach (int item in inventory)
        {
            if (item == 1)
            {
                Debug.Log("This is food!");
                currentFoodCount++;
            }
        }

    }

    public bool EatFood()
    {
        foreach (int item in inventory)
        {
            if (item == 1)
            {
                this.inventory.Remove(item);
                this.GetComponent<PlayerMovementTopDown>().IncreaseMoveSpeed(1);
                currentFoodCount--;
                Debug.Log("You ate food!");
                return true;
            }
        }
        return false;
    }
}
