using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;

public class Door : MonoBehaviour
{

    SpriteRenderer spriteRender;
    SpriteLibrary spriteLibrary;
    BoxCollider2D boxCollider;
    [FormerlySerializedAs("_doorID")] [SerializeField][Min(1)] int doorID = 1;

    void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D _other)
    {
        if (_other.gameObject.TryGetComponent(out PickUpHandler pickUpHandler))
        {
            OpenDoor(pickUpHandler.PickedUpKeys);
        }
    }

    public void OpenDoor(List<int> _pickedUpKeys)
    {
        foreach (int keyID in _pickedUpKeys.Where(_keyID => _keyID == doorID))
        {
            boxCollider.enabled = false;
            spriteRender.sprite = spriteLibrary.GetSprite("DoorState", "doorOpen");
        }
    }
}
