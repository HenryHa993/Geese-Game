using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject Enemy, outerLayerNode;
    public GameObject[] enemyArray;
    public int numEnemies = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (!(outerLayerNode == null))
        {
            enemyArray = new GameObject[numEnemies];
            Point currentNode = outerLayerNode.GetComponent<Point>();
            for (int i = 0; i < numEnemies; i++)
            {
                Vector3 initPosition = currentNode.transform.position;
                Quaternion initRotation = Quaternion.Euler(0, 0, 0);
                enemyArray[i] = Instantiate(Enemy, initPosition, initRotation);
                Enemy.GetComponent<Enemy>().number = i;
                currentNode = currentNode.sibling.GetComponent<Point>().sibling.GetComponent<Point>();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
