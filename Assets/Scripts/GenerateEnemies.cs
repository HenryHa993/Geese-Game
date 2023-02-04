using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject Enemy, outerLayerNode, player, generateBoard;
    public GameObject[] enemyArray;
    public bool isTurn, movesMade;
    public int numEnemies = 5, moveDelayInFrames = 100;
    private int frameCounter;

    // Spawn enemies on runtime
    void Start()
    {
        if (!(outerLayerNode == null))
        {
            enemyArray = new GameObject[numEnemies];
            GameObject currentNode = outerLayerNode;

            // An enemy on every other outernode
            for (int i = 0; i < numEnemies; i++)
            {
                Vector3 initPosition = currentNode.transform.position;
                Quaternion initRotation = Quaternion.Euler(0, 0, 0);

                enemyArray[i] = Instantiate(Enemy, initPosition, initRotation);

                Enemy enemy = enemyArray[i].GetComponent<Enemy>();
                enemy.player = player;
                enemy.generateBoard = generateBoard;
                enemy.entityNode = currentNode;
                enemy.entityNode.GetComponent<Point>().isOccupiedBy = enemyArray[i];

                // Every other outernode
                currentNode = currentNode.GetComponent<Point>().sibling.GetComponent<Point>().sibling;
            }
        }
        frameCounter = 0;
    }

    void Update()
    {
        // Monsters turn
        if (isTurn)
        {
            EnemyMove();
        }
            
    }

    public void EnemyMove()
    {
        // Delay hopefully prevents intersection moves
        if (frameCounter >= moveDelayInFrames)
        {
            frameCounter = 0;

            //find minScores for all valid moves
            //  Have BestMove class that considers minScore, corresponding move, and enemy
            //select random number of enemies to move
            //move enemies
            System.Random rand = new System.Random();
            int numMoved = rand.Next(numEnemies);

            for (int i = 0; i < numMoved; i++) // This is largely likely to make the last enemy not move many times over
            {
                Enemy enemy = enemyArray[i].GetComponent<Enemy>();
                enemy.updateMoveScores();
                int index = System.Array.IndexOf(enemy.moveScores, enemy.moveScores.Min());

                // enemy.entityNode.GetComponent<Point>().badSibling.GetComponent<Point>().isOccupiedBy != player
                    GameObject newNode = enemy.possibleMoves[index];
                    //check if player is there, if so don't move as adjacent
                    if (newNode.GetComponent<Point>().isOccupiedBy != player)
                    {
                        // Update isOccupiedBy
                        newNode.GetComponent<Point>().isOccupiedBy = enemyArray[i];
                        enemy.entityNode.GetComponent<Point>().isOccupiedBy = null;

                        enemy.transform.position = newNode.transform.position;
                        enemy.entityNode = newNode;
                    }
                    

                
            }
            movesMade = true;
        }
        else
        {
            frameCounter++;
        }

    }
}
