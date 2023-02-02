using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameControl handles the turn-based system

public class GameControl : MonoBehaviour
{
    public GameObject enemies, player;
    public int playerMovesThisTurn = 2;
    public bool isPlayersTurn;

    public PlayerController playerController;
    public GenerateEnemies enemyController;

    // Initialise game start state, with players move
    void Start()
    {
        isPlayersTurn = true;

        playerController = player.GetComponent<PlayerController>();
        enemyController = enemies.GetComponent<GenerateEnemies>();

        playerController.movesToMake = playerMovesThisTurn;
        enemyController.movesMade = false;
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
    }
}
