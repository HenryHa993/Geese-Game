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
            foreach (GameObject enemyObject in enemyArray)
            {
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                enemy.updateMoveScores();
                int index =  System.Array.IndexOf(enemy.moveScores, enemy.moveScores.Min());
                Debug.Log("Scores parent:" + enemy.moveScores[0] );
                Debug.Log("Scores child:" + enemy.moveScores[1] );
                Debug.Log("Scores sibling:" + enemy.moveScores[2] );
                Debug.Log("!!!!Index:" + index );



                GameObject newNode = enemy.possibleMoves[index];
                enemy.transform.position = newNode.transform.position;
                enemy.entityNode = newNode;
                
            }
            movesMade = true;
        }
            
    }
}
