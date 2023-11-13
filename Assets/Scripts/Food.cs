using UnityEngine;

public class Food : Item
{
    public override void OnPickup(GameObject _player)
    {
        _player.GetComponent<PlayerMovementTopDown>().IncreaseMoveSpeed(1);
    }
}
