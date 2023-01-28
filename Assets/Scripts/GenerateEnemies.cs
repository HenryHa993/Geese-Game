using System.Collections;
using System.Collections.Generic;
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
                enemy.transform.position = new Vector3(enemy.transform.position.x + 1f, enemy.transform.position.y, enemy.transform.position.z);
            }
            movesMade = true;
        }
            
    }
}
