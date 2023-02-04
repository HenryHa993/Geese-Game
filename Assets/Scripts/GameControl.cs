using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameControl handles the turn-based system

public class GameControl : MonoBehaviour
{
    public GameObject enemies, player, key, origin;
    public int playerMovesThisTurn = 2;
    public bool isPlayersTurn, gameWon;

    public PlayerController playerController;
    public GenerateEnemies enemyController;

    // Initialise game start state, with players move
    void Start()
    {
        isPlayersTurn = true;
        gameWon = false;

        playerController = player.GetComponent<PlayerController>();
        enemyController = enemies.GetComponent<GenerateEnemies>();

        playerController.movesToMake = playerMovesThisTurn;
        enemyController.movesMade = false;
        key.GetComponent<Key>().isCollected = false;
    }

    // Swaps turns as turns are made
    void Update()
    {
        
        if (enemyController.movesMade)
        {
            enemyController.movesMade = false;
            playerController.movesToMake = playerMovesThisTurn;
        }
        isPlayersTurn = playerController.movesToMake >= 1;

        playerController.isTurn = isPlayersTurn;
        enemyController.isTurn = !isPlayersTurn;

        if (origin.GetComponent<Point>().isOccupiedBy == player && key.GetComponent<Key>().isCollected)
        {
            gameWon = true;
        }
    }
}
