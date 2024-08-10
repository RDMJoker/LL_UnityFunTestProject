using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Vector2 moveInput;
    Vector2 currentPos;
    Vector2 boxSize;
    [FormerlySerializedAs("_moveSpeed")] [SerializeField] float moveSpeed = 3;
    [FormerlySerializedAs("_distance")] [SerializeField] float distance = 1;
    [FormerlySerializedAs("_obstacles")] [SerializeField] LayerMask obstacles;
    bool freezeMovement;

    public delegate void OnPlayerCollision(GameObject _player);

    public event OnPlayerCollision OnCollideWithPlayer;

    void Awake()
    {
        boxSize = new Vector2(0.5f, 0.5f);
        playerRigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(NewUpdateDirection), 0, 2);
    }

    void FixedUpdate()
    {
        if (!freezeMovement)
        {
            playerRigidbody.velocity = moveInput * moveSpeed;
        }
        else
        {
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D _collidingObject)
    {
        if (_collidingObject.gameObject.CompareTag("Wall") || _collidingObject.gameObject.GetComponent<Door>())
        {
            CancelInvoke(nameof(NewUpdateDirection));
            NewUpdateDirection();
            InvokeRepeating(nameof(NewUpdateDirection), 2, 2);
        }
        else if (_collidingObject.gameObject.CompareTag("Player"))
        {
            CancelInvoke(nameof(NewUpdateDirection));
            FreezeEnemies();
            OnCollideWithPlayer?.Invoke(_collidingObject.gameObject);
        }
    }
    
    // void UpdateDirection()
    // {
    //     var possibleDirections = new List<Vector2>();
    //     var hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(1, 0).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(1, 0));
    //     }
    //
    //     hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(1, 1).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(1, 1));
    //     }
    //
    //     hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(0, 1).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(0, 1));
    //     }
    //
    //     hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(-1, 1).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(-1, 1));
    //     }
    //
    //     hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(-1, 0).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(-1, 0));
    //     }
    //
    //     hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(-1, -1).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(-1, -1));
    //     }
    //
    //     hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(0, -1).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(0, -1));
    //     }
    //
    //     hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(1, -1).normalized, distance,
    //         obstacles);
    //     if (hitInfo.collider == null)
    //     {
    //         possibleDirections.Add(new Vector2(1, -1));
    //     }
    //
    //     moveInput = possibleDirections[Random.Range(0, possibleDirections.Count)];
    //     moveInput.Normalize();
    // }

    void NewUpdateDirection()
    {
        var possibleDirections = new List<Vector2>();
        var checkDirections = new[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            Vector2.up + Vector2.right,
            Vector2.up + Vector2.left,
            Vector2.down + Vector2.right,
            Vector2.down + Vector2.left,
        };
        foreach (var direction in checkDirections)
        {
            var hitInfo = Physics2D.BoxCast(transform.position, boxSize, 0, direction.normalized, distance, obstacles);
            if (hitInfo.collider == null)
            {
                possibleDirections.Add(direction);
            }
        }
        moveInput = possibleDirections[Random.Range(0, possibleDirections.Count)];
        moveInput.Normalize();
    }
    
    public void FreezeEnemies()
    {
        freezeMovement = true;
        playerRigidbody.velocity = Vector2.zero;
    }
}