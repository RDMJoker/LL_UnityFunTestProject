using System.Collections.Generic;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
    public readonly List<int> PickedUpKeys = new();
    GameObject touchedObject;
    readonly List<GameObject> touchedObjects = new();
    public delegate void PickedUpUIChange(int _keyID);

    public event PickedUpUIChange OnKeyPickup;

    void OnCollisionEnter2D(Collision2D _collidingObject)
    {
        if (_collidingObject.gameObject.GetComponent<Item>() && !_collidingObject.gameObject.GetComponent<DropTables>())
        {
            _collidingObject.gameObject.GetComponent<Item>().OnPickup(gameObject);
            Destroy(_collidingObject.gameObject);
        }
        else if (_collidingObject.gameObject.GetComponent<DropTables>())
        {
            touchedObject = _collidingObject.gameObject;
            touchedObjects.Add(touchedObject);
            GetComponent<PlayerMovementTopDown>().SetNearInventory(true);
        }
    }
    void OnCollisionExit2D(Collision2D _collidingObject)
    {
        if (!_collidingObject.gameObject.GetComponent<DropTables>()) return;
        GetComponent<PlayerMovementTopDown>().SetNearInventory(false);
        touchedObjects.Remove(_collidingObject.gameObject);
    }

    public void GetKey(int _keyID)
    {
        OnKeyPickup?.Invoke(_keyID);
        PickedUpKeys.Add(_keyID);

    }

    public void LootInventory()
    {
        foreach (var box in touchedObjects)
        {
            if (!box.gameObject.TryGetComponent(out DropTables inventoryList) || inventoryList.IsAlreadyOpen) continue;
            gameObject.GetComponent<PlayerInventory>().ReceiveItems(inventoryList.GetObjects());
            break;
        }
    }
}
