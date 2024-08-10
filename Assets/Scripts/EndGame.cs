using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class EndGame : MonoBehaviour
{
    public Action OnEndGame;
    public Action OnKillPlayer;
    [FormerlySerializedAs("_enemyClass")] [SerializeField] EnemyMovement enemyClass;
    [FormerlySerializedAs("_enemyClass2")] [SerializeField] EnemyMovement enemyClass2;

    void Awake()
    {
        enemyClass.OnCollideWithPlayer += KillPlayer;
        enemyClass2.OnCollideWithPlayer += KillPlayer;
    }

    void KillPlayer(GameObject _player)
    {
        FreezePlayerMovement(_player);
        enemyClass.FreezeEnemies();
        enemyClass2.FreezeEnemies();
        OnKillPlayer.Invoke();
        StartCoroutine(StartEndTimer());
    }

    static void FreezePlayerMovement(GameObject _player)
    {
        var playerMovementController = _player.GetComponent<PlayerMovementTopDown>();
        playerMovementController.FreezePlayerControl();
    }

    void OnTriggerEnter2D(Collider2D _player)
    {
        FreezePlayerMovement(_player.gameObject);
        enemyClass.FreezeEnemies();
        enemyClass2.FreezeEnemies();
        OnEndGame.Invoke();
        StartCoroutine(StartEndTimer());
    }

    IEnumerator StartEndTimer()
    {
        yield return new WaitForSeconds(2);
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
    }
}