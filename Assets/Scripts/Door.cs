using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Door : MonoBehaviour
{

    SpriteRenderer spriteRender;
    SpriteLibrary spriteLibrary;
    BoxCollider2D boxCollider;
    [SerializeField][Min(1)] int _doorID = 1;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    public virtual void OpenDoor(List<int> _pickedUpKeys)
    {
        foreach (int _keyID in _pickedUpKeys)
        {
            if (_keyID == _doorID)
            {
                boxCollider.enabled = false;
                spriteRender.sprite = spriteLibrary.GetSprite("DoorState", "doorOpen");
            }
        }
    }
}
