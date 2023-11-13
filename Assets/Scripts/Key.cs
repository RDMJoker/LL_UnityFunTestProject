using UnityEngine;

public class Key : Item
{
    [SerializeField][Min(1)] int _keyID;
    public override void OnPickup(GameObject _player)
    {
        _player.gameObject.GetComponent<PickUpHandler>().GetKey(_keyID);
    }
}
