using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameControl handles the turn-based system

public class GameControl : MonoBehaviour
{
    public GameObject enemies, player, key, origin, keyIcon, gameWonIcon, gameOverIcon;
    public int playerMovesThisTurn = 2;
    public bool isPlayersTurn, gameWon, gameOver;

    public PlayerController playerController;
    public GenerateEnemies enemyController;

    // Initialise game start state, with players move
    void Start()
    {
        isPlayersTurn = true;
        gameWon = false;
        gameOver = false;

        playerController = player.GetComponent<PlayerController>();
        enemyController = enemies.GetComponent<GenerateEnemies>();

        playerController.movesToMake = 1;
        enemyController.movesMade = false;
        key.GetComponent<Key>().isCollected = false;
        keyIcon.SetActive(false);
    }

    // Swaps turns as turns are made
    void Update()
    {
        gameWonIcon.SetActive(gameWon);
        gameOverIcon.SetActive(gameOver && !gameWon);
        if (!gameOver)
        {
            if (enemyController.movesMade)
            {
                enemyController.movesMade = false;
                System.Random rnd = new System.Random();
                playerController.movesToMake = rnd.Next(playerMovesThisTurn);
            }
            isPlayersTurn = playerController.movesToMake >= 1;

            playerController.isTurn = isPlayersTurn;
            enemyController.isTurn = !isPlayersTurn;

            keyIcon.SetActive(key.GetComponent<Key>().isCollected);



            if (origin.GetComponent<Point>().isOccupiedBy == player && key.GetComponent<Key>().isCollected)
            {
                gameWon = true;
            }

            //game over check
            Point playerNode = player.GetComponent<PlayerController>().currentNode.GetComponent<Point>();
            int gameOverPoints = 0;
            //playerNode adjacent has goose or is null
            if (playerNode.parent == null)
            {
                gameOverPoints++;
            }else if (playerNode.parent.transform.CompareTag("Enemy"))
            {
                gameOverPoints++;
            }

            if (playerNode.child == null)
            {
                gameOverPoints++;
            }else if (playerNode.child.transform.CompareTag("Enemy"))
            {
                gameOverPoints++;
            }

            if (playerNode.sibling == null)
            {
                gameOverPoints++;
            }else if (playerNode.sibling.transform.CompareTag("Enemy"))
            {
                gameOverPoints++;
            }

            if (gameOverPoints >= 3)
            {
                gameOver = true;
            }
        }

    }
}
