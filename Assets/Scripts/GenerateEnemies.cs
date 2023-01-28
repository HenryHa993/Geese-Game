using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject Enemy, outerLayerNode, player, generateBoard;
    public GameObject[] enemyArray;
    public bool isTurn, movesMade;
    public int numEnemies = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (!(outerLayerNode == null))
        {
            enemyArray = new GameObject[numEnemies];
            GameObject currentNode = outerLayerNode;
            for (int i = 0; i < numEnemies; i++)
            {
                Vector3 initPosition = currentNode.transform.position;
                Quaternion initRotation = Quaternion.Euler(0, 0, 0);
                enemyArray[i] = Instantiate(Enemy, initPosition, initRotation);
                Enemy enemy = enemyArray[i].GetComponent<Enemy>();
                enemy.player = player;
                enemy.generateBoard = generateBoard;
                enemy.entityNode = currentNode;

                currentNode = currentNode.GetComponent<Point>().sibling.GetComponent<Point>().sibling;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurn)
        {
            //move enemies
            System.Random rand = new System.Random();
            int numMoved = rand.Next(numEnemies);

            for (int i = 0; i < numMoved; i++)
            {
                Enemy enemy = enemyArray[i].GetComponent<Enemy>();
                enemy.updateMoveScores();
                int index =  System.Array.IndexOf(enemy.moveScores, enemy.moveScores.Min());
                



                GameObject newNode = enemy.possibleMoves[index];
                enemy.transform.position = newNode.transform.position;
                enemy.entityNode = newNode;
            }
            movesMade = true;
        }
            
    }
}
