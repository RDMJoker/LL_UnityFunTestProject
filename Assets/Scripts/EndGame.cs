using System;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public Action OnEndGame;
    public Action OnKillPlayer;
    [SerializeField] EnemyMovement _enemyClass;
    [SerializeField] EnemyMovement _enemyClass2;

    private void Awake()
    {
        _enemyClass.OnCollideWithPlayer += KillPlayer;
        _enemyClass2.OnCollideWithPlayer += KillPlayer;
    }

    private void KillPlayer(GameObject _player)
    {
        FreezePlayerMovement(_player);
        OnKillPlayer.Invoke();
    }

    public void EndGameTrigger(GameObject _player)
    {
        FreezePlayerMovement(_player);
        _enemyClass.FreezeEnemies();
        _enemyClass2.FreezeEnemies();
        OnEndGame.Invoke();
    }

    private static void FreezePlayerMovement(GameObject _player)
    {
        var playerMovementController = _player.GetComponent<PlayerMovementTopDown>();
        playerMovementController.FreezePlayerControl();
    }
}
