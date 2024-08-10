using UnityEngine;

public class Item : MonoBehaviour
{
    protected enum EItemCategories
    {
        Food = 1,
        Ammo = 2,
        Weapon = 3,
        Armor = 4,
    }
    public virtual void OnPickup(GameObject _player)
    {

    }
}
