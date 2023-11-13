using System.Collections.Generic;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
    protected List<int> pickedUpKeys = new List<int>();
    private GameObject touchedObject;
    private List<GameObject> touchedObjects = new List<GameObject>();
    public delegate void PickedUpUIChange(int _keyID);

    public event PickedUpUIChange OnKeyPickup;

    private void OnCollisionEnter2D(Collision2D _collidingObject)
    {
        if (_collidingObject.gameObject.GetComponent<Item>() && !_collidingObject.gameObject.GetComponent<DropTables>())
        {
            _collidingObject.gameObject.GetComponent<Item>().OnPickup(this.gameObject);
            Destroy(_collidingObject.gameObject);

        }
        else if (_collidingObject.gameObject.GetComponent<Door>())
        {
            _collidingObject.gameObject.GetComponent<Door>().OpenDoor(pickedUpKeys);

        }
        else if (_collidingObject.gameObject.GetComponent<DropTables>())
        {
            touchedObject = _collidingObject.gameObject;
            touchedObjects.Add(touchedObject);
            this.GetComponent<PlayerMovementTopDown>().SetNearInventory(true);
        }
        else if (_collidingObject.gameObject.TryGetComponent(out EndGame _endgame))
        {
            Debug.Log("Das Spiel ist vorbei!");
            _endgame.EndGameTrigger(this.gameObject);
            Application.Quit();
        }
    }
    private void OnCollisionExit2D(Collision2D _collidingObject)
    {
        if (_collidingObject.gameObject.GetComponent<DropTables>())
        {
            this.GetComponent<PlayerMovementTopDown>().SetNearInventory(false);
            touchedObjects.Remove(_collidingObject.gameObject);
        }
    }

    public void GetKey(int _keyID)
    {
        OnKeyPickup.Invoke(_keyID);
        pickedUpKeys.Add(_keyID);

    }

    public void LootInventory()
    {
        foreach (GameObject box in touchedObjects)
        {
            if (box.gameObject.TryGetComponent(out DropTables InventoryList) && !InventoryList.IsAlreadyOpen)
            {
                this.gameObject.GetComponent<PlayerInventory>().RecieveItems(InventoryList.GetObjects());
                break;
            }
        }
    }
}
