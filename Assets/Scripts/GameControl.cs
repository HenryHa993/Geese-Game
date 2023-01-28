using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    // Determines win/lose conditions
    // Determines time
    public GameObject enemies, player;

    public int playerMovesThisTurn = 2;
    public bool isPlayersTurn;

    void Start()
    {
        isPlayersTurn = true;
        player.GetComponent<PlayerController>().movesToMake = playerMovesThisTurn;
        enemies.GetComponent<GenerateEnemies>().movesMade = false;
    }


    void Update()
    {
        
        if (enemies.GetComponent<GenerateEnemies>().movesMade)
        {
            enemies.GetComponent<GenerateEnemies>().movesMade = false;
            player.GetComponent<PlayerController>().movesToMake = playerMovesThisTurn;
        }

        isPlayersTurn = player.GetComponent<PlayerController>().movesToMake >= 1;
        
        player.GetComponent<PlayerController>().isTurn = isPlayersTurn;
        enemies.GetComponent<GenerateEnemies>().isTurn = !isPlayersTurn;
    }
}
