using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
    public readonly List<int> PickedUpKeys = new();
    DropTables touchedObject;
    readonly List<DropTables> touchedObjects = new();
    public delegate void PickedUpUIChange(int _keyID);
    public event PickedUpUIChange OnKeyPickup;

    void OnCollisionEnter2D(Collision2D _collidingObject)
    {
        if (_collidingObject.gameObject.TryGetComponent(out Item item))
        {
            item.OnPickup(gameObject);
            Destroy(_collidingObject.gameObject);
        }
        else if (_collidingObject.gameObject.TryGetComponent(out DropTables table))
        {
            touchedObject = table;
            touchedObjects.Add(touchedObject);
            GetComponent<PlayerMovementTopDown>().SetNearInventory(true);
        }
    }
    void OnCollisionExit2D(Collision2D _collidingObject)
    {
        if (!_collidingObject.gameObject.TryGetComponent(out DropTables table)) return;
        GetComponent<PlayerMovementTopDown>().SetNearInventory(false);
        touchedObjects.Remove(table);
    }

    public void GetKey(int _keyID)
    {
        OnKeyPickup?.Invoke(_keyID);
        PickedUpKeys.Add(_keyID);

    }

    public void LootInventory()
    {
        foreach (var box in touchedObjects.Where(_box => !_box.IsAlreadyOpen))
        {
            gameObject.GetComponent<PlayerInventory>().ReceiveItems(box.GetObjects());
            break;
        }
    }
}
