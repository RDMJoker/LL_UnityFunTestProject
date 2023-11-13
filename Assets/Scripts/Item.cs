using UnityEngine;

public class Item : MonoBehaviour
{
    protected enum EItemCategories
    {
        food = 1,
        ammo = 2,
        weapon = 3,
        armor = 4,
    };
    public virtual void OnPickup()
    {

    }
    public virtual void OnPickup(GameObject _player)
    {

    }
}
