using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTopDown : MonoBehaviour
{
    Rigidbody2D rigdbody;
    Vector2 moveInput;
    private bool isNearInventory = false;
    [SerializeField] private float _moveSpeed = 2;
    private bool freezeMovement;

    private void Awake()
    {
        rigdbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (!freezeMovement)
        {
        rigdbody.velocity = moveInput * _moveSpeed;
        }
    }

    private void OnWalk(InputValue _value)
    {
        moveInput = _value.Get<Vector2>();

    }
    private void OnInteract()
    {
        if (isNearInventory)
        {
            this.GetComponent<PickUpHandler>().LootInventory();
        }
    }

    private void OnEat()
    {
        if (!this.GetComponent<PlayerInventory>().EatFood())
        {
            Debug.Log("You have no food!");
        }
    }

    public void IncreaseMoveSpeed(float _value)
    {
        _moveSpeed += _value;
    }

    public void FreezePlayerControl()
    {
        freezeMovement = true;
        rigdbody.velocity = Vector2.zero;
    }

    public void SetNearInventory(bool _state)
    {
        isNearInventory = _state;
    }
}
