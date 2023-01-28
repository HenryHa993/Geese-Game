using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject player;
    public Point playerPoint;
    public GameObject entityNode;
    public bool isBlocking;

    public GameObject generateBoard;
    public GenerateBoard genScript;

    public GameObject[] possibleMoves;// = new GameObject[3];
    public int[] moveScores;

    private void Start()
    {
        playerPoint = player.GetComponent<Point>();
        genScript = generateBoard.GetComponent<GenerateBoard>();
    }

    public void updatePossibleMoves()
    {
        possibleMoves = new GameObject[3]{entityNode.GetComponent<Point>().parent, entityNode.GetComponent<Point>().child, entityNode.GetComponent<Point>().sibling};
    }

    public void updateMoveScores()
    {
/*        foreach (GameObject move in possibleMoves)
        {

        }*/

        for (int i= 0; i < moveScores.Length; i++)
        {
            moveScores[i] = getScore(possibleMoves[i]);
        }
    }

    public int getScore(GameObject move)
    {
        Point pointScript = move.GetComponent<Point>();
        //GameObject[,] pointArray = genScript.pointArray;
        int score = 100;

        if(pointScript.isOccupiedBy != null)
        {
            return 100;
        }

        if (isBlocking)
        {
            return 100;
        }

        // Calculate score
        // Count layer difference (take positive)
        if(pointScript.layer < playerPoint.layer)
        {
            score += playerPoint.layer - pointScript.layer;
        }
        else
        {
            score += pointScript.layer - playerPoint.layer;
        }

        // Count number of side turns
        if(pointScript.sidePosition > genScript.numNodes / 2 && playerPoint.sidePosition > genScript.numNodes / 2)
        {
            score += Mathf.Abs(pointScript.sidePosition - playerPoint.sidePosition);
        }
        else if (pointScript.sidePosition < genScript.numNodes / 2 && playerPoint.sidePosition < genScript.numNodes / 2)
        {
            score += Mathf.Abs(pointScript.sidePosition - playerPoint.sidePosition);
        }
        else if (pointScript.sidePosition - playerPoint.sidePosition > genScript.numNodes / 2)
        {
            score += pointScript.sidePosition - (playerPoint.sidePosition + genScript.numNodes / 2);
        }
        else if (pointScript.sidePosition - playerPoint.sidePosition < - genScript.numNodes / 2)
        {
            score += pointScript.sidePosition - (playerPoint.sidePosition - genScript.numNodes / 2);
        }
        else
        {
            score += Mathf.Abs(pointScript.sidePosition - playerPoint.sidePosition);
        }

        return score;
    }
}
