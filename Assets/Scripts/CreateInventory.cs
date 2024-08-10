using UnityEngine;
using UnityEngine.Serialization;

public class CreateInventory : MonoBehaviour
{
    [FormerlySerializedAs("_spriteStateOpen")] [SerializeField] Sprite spriteStateOpen;
    public Sprite SpriteStateOpen => spriteStateOpen;
    [FormerlySerializedAs("_spriteStateClosed")] [SerializeField] Sprite spriteStateClosed;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteStateClosed;
    }
}
