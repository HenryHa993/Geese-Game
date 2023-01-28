using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public GameObject origin;
    public GameObject point, player, enemies;

    public int layers = 1;
    public int numNodes = 4;
    public float radius = 5f;

    public GameObject[,] pointArray;

    private void Awake()
    {
        // Save points in an array
        pointArray = new GameObject[layers, numNodes];

        // Forms circle based on layers, radius and numNodes
        for (int j = 1; j <= layers; j++)
        {
            for (int i = 0; i < numNodes; i++)
            {
                float angle = i * Mathf.PI * 2 / numNodes;
                float x = Mathf.Cos(angle) * (radius * j);
                float z = Mathf.Sin(angle) * (radius * j);
                Vector3 pos = transform.position + new Vector3(x, 0, z);
                float angleDegrees = -angle * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
                //Instantiate(point, pos, rot);

                // Adding instantiated object to multidimensional array
                pointArray[j - 1, i] = Instantiate(point, pos, rot);
                pointArray[j - 1, i].GetComponent<Point>().layer = j;
            }
        }

        //Debug.Log(pointArray);
        //Print2DArray(pointArray);

        // Traverse through 2D array to assign relationships
        for (int j = 1; j <= layers; j++)
        {
            // FIRST LAYER
            // Assign parent to origin, its child
            // ANTICLOCKWISE R->L
            if (j == 1)
            {
                for (int i = 0; i < numNodes; i++)
                {
                    pointArray[j - 1, i].GetComponent<Point>().parent = origin;
                    pointArray[j - 1, i].GetComponent<Point>().sidePosition = i;
                    pointArray[j - 1, i].GetComponent<Point>().child = pointArray[j, i]; // Next layer, same position in second dimension
                    pointArray[j, i].GetComponent<Point>().parent = pointArray[j - 1, i]; // This node is parent of its child

                    if (i == 0)
                    {
                        pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, numNodes - 1]; // Same layer, end of array
                        pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i + 1]; // Point on left
                    }
                    else if(i == numNodes - 1)
                    {
                        pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i - 1]; // Same layer, end of array
                        pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, 0];
                    }
                    else
                    {
                        pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i - 1]; // Point on left
                        pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i + 1]; // Point on left
                    }
                }
            }

            // Odd
            // ANTICLOCKWISE in array
            if (j % 2 != 0 && j > 1)
            {
                // Mid layer
                if (j < layers)
                {
                    // Sibling should be on end of array if i = 0
                    for (int i = 0; i < numNodes; i++)
                    {
                        pointArray[j - 1, i].GetComponent<Point>().sidePosition = i;
                        pointArray[j - 1, i].GetComponent<Point>().child = pointArray[j, i]; // Next layer, same position in second dimension
                        pointArray[j, i].GetComponent<Point>().parent = pointArray[j - 1, i]; // This node is parent of its child

                        if (i == 0)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, numNodes - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i + 1]; // Point on left
                        }
                        else if (i == numNodes - 1)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, 0];
                        }
                        else
                        {
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i - 1]; // Point on left
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i + 1]; // Point on left
                        }
                    }
                }

                // End layer
                if (j == layers)
                {
                    // Sibling should be on end of array if i = 0
                    for (int i = 0; i < numNodes; i++)
                    {
                        pointArray[j - 1, i].GetComponent<Point>().sidePosition = i;
                        if (i == 0)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, numNodes - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i + 1]; // Point on left
                        }
                        else if (i == numNodes - 1)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, 0];
                        }
                        else
                        {
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i - 1]; // Point on left
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i + 1]; // Point on left
                        }
                    }
                }

            }


            // EVEN L -> R CLOCKWISE
            if (j % 2 == 0 && j > 1)
            {
                for (int i = 0; i < numNodes; i++)
                {
                    // Last layer
                    // Only assign siblings
                    pointArray[j - 1, i].GetComponent<Point>().sidePosition = i;
                    if (j == layers)
                    {
                        if (i == 0)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, numNodes - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i + 1];
                        }
                        else if (i == numNodes - 1)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, 0]; // Point on left
                        }
                        else
                        {
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i - 1]; // Point on left
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i + 1]; // Point on left
                        }
                    }
                    else // Mid layer
                    {
                        pointArray[j - 1, i].GetComponent<Point>().child = pointArray[j, i]; // Next layer, same position in second dimension
                        pointArray[j, i].GetComponent<Point>().parent = pointArray[j - 1, i]; // This node is parent of its child

                        if (i == 0)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, numNodes - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i + 1];
                        }
                        else if (i == numNodes - 1)
                        {
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i - 1]; // Same layer, end of array
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, 0]; // Point on left
                        }
                        else
                        {
                            pointArray[j - 1, i].GetComponent<Point>().badSibling = pointArray[j - 1, i - 1]; // Point on left
                            pointArray[j - 1, i].GetComponent<Point>().sibling = pointArray[j - 1, i + 1]; // Point on left
                        }

                    }
                }
            }

        }



        //assign starting node to player
        player.GetComponent<PlayerController>().targetNode = pointArray[0,0];
        enemies.GetComponent<GenerateEnemies>().outerLayerNode = pointArray[layers-1,0];
    }

    



    /*    public static void Print2DArray<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Debug.Log(matrix[i, j] + "\t");
                }
                Debug.Log("");
            }
        }
    */
}
