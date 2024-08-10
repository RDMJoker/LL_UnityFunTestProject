using UnityEngine;
using UnityEngine.Serialization;

public class Key : Item
{
    [FormerlySerializedAs("_keyID")] [SerializeField][Min(1)] int keyID;
    public override void OnPickup(GameObject _player)
    {
        _player.gameObject.GetComponent<PickUpHandler>().GetKey(keyID);
    }
}
