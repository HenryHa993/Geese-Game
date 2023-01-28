using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Player tracking
    public GameObject player;
    public Point playerPoint;

    // Track entityNode
    public GameObject entityNode;

    // Checks is blocking player TODO
    public bool isBlocking;

    // To get number of nodes
    public GameObject generateBoard;
    public GenerateBoard genScript;

    // Possible moves and their corresponding move scores
    public GameObject[] possibleMoves;
    public int[] moveScores;

    private void Start()
    {
        playerPoint = player.GetComponent<Point>();
        genScript = generateBoard.GetComponent<GenerateBoard>();

        moveScores = new int[3];
        possibleMoves = new GameObject[3];

        isBlocking = false;
    }

    public void updatePossibleMoves()
    {
        playerPoint = player.GetComponent<Point>();
        possibleMoves = new GameObject[3]{entityNode.GetComponent<Point>().parent, entityNode.GetComponent<Point>().child, entityNode.GetComponent<Point>().sibling};
    }

    public void updateMoveScores()
    {
        // Updates score array
        for (int i= 0; i < moveScores.Length; i++)
        {
            int dasscore = getScore(possibleMoves[i]);
            moveScores[i] = dasscore;
            Debug.Log(moveScores[i] );
        }
    }

    // Get score for each move
    // Lower score means better for getting closer to player
    public int getScore(GameObject move)
        {
        Point pointScript = move.GetComponent<Point>();
        //GameObject[,] pointArray = genScript.pointArray;
        int score = 0;

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
            score += Mathf.Abs(pointScript.sidePosition - (playerPoint.sidePosition + genScript.numNodes));
        }
        else if (pointScript.sidePosition - playerPoint.sidePosition < - genScript.numNodes / 2)
        {
            score += Mathf.Abs(pointScript.sidePosition - (playerPoint.sidePosition - genScript.numNodes));
        }
        else
        {
            score += Mathf.Abs(pointScript.sidePosition - playerPoint.sidePosition);
        }

        return score;
    }
}
