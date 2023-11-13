using System.Collections.Generic;
using UnityEngine;

public class CreateInventory : MonoBehaviour
{
    [SerializeField] private Sprite _spriteStateOpen;
    public Sprite SpriteStateOpen => _spriteStateOpen;
    [SerializeField] private Sprite _spriteStateClosed;
    public Sprite SpriteStateClosed => _spriteStateClosed;
    List<int> containedObjectIDs = new List<int>();
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _spriteStateClosed;
    }

    public List<int> GiveInventory(GameObject _player)
    {
        return containedObjectIDs;
    }
}
