using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rigdbody;
    Vector2 moveInput;
    Vector2 currentPos;
    Vector2 boxSize;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _distance = 1;
    [SerializeField] private LayerMask _obstacles;
    private bool freezeMovement;

    public delegate void OnPlayerCollision(GameObject _player);
    public event OnPlayerCollision OnCollideWithPlayer;

    private void Awake()
    {
        boxSize = new Vector2(0.5f, 0.5f);
        rigdbody = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(UpdateDirection), 0, 2);
    }
    private void FixedUpdate()
    {
        if (!freezeMovement)
        {
            rigdbody.velocity = moveInput * _moveSpeed;
        }
        else
        {
            rigdbody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D _collidingObject)
    {
        if (_collidingObject.gameObject.CompareTag("Wall") || _collidingObject.gameObject.GetComponent<Door>())
        {
            CancelInvoke(nameof(UpdateDirection));
            UpdateDirection();
            InvokeRepeating(nameof(UpdateDirection), 2, 2);
        }
        else if (_collidingObject.gameObject.CompareTag("Player"))
        {
            CancelInvoke(nameof(UpdateDirection));
            FreezeEnemies();
            OnCollideWithPlayer.Invoke(_collidingObject.gameObject);
        }
    }

    private void UpdateDirection()
    {
        List<Vector2> possibleDirections = new List<Vector2>();
        var hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(1, 0).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(1, 0));
        }
        hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(1, 1).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(1, 1));
        }
        hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(0, 1).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(0, 1));
        }
        hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(-1, 1).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(-1, 1));
        }
        hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(-1, 0).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(-1, 0));
        }
        hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(-1, -1).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(-1, -1));
        }
        hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(0, -1).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(0, -1));
        }
        hitInfo = Physics2D.BoxCast(this.transform.position, boxSize, 0, new Vector2(1, -1).normalized, _distance, _obstacles);
        if (hitInfo.collider == null)
        {
            possibleDirections.Add(new Vector2(1, -1));
        }

        moveInput = possibleDirections[Random.Range(0, possibleDirections.Count)];
        moveInput.Normalize();
    }

    public void FreezeEnemies()
    {
        freezeMovement = true;
        rigdbody.velocity = Vector2.zero;
    }
}
