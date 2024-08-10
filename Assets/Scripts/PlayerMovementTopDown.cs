using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovementTopDown : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Vector2 moveInput;
    bool isNearInventory;

    [FormerlySerializedAs("_moveSpeed")] [SerializeField] float moveSpeed = 2;

    bool freezeMovement;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!freezeMovement)
        {
            playerRigidbody.velocity = moveInput * moveSpeed;
        }
    }

    void OnWalk(InputValue _value)
    {
        moveInput = _value.Get<Vector2>();
    }

    void OnInteract()
    {
        if (isNearInventory)
        {
            GetComponent<PickUpHandler>().LootInventory();
        }
    }

    void OnEat()
    {
        if (!GetComponent<PlayerInventory>().EatFood())
        {
            Debug.Log("You have no food!");
        }
    }

    public void IncreaseMoveSpeed(float _value)
    {
        moveSpeed += _value;
    }

    public void FreezePlayerControl()
    {
        freezeMovement = true;
        playerRigidbody.velocity = Vector2.zero;
    }

    public void SetNearInventory(bool _state)
    {
        isNearInventory = _state;
    }
}