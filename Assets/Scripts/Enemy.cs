using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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

    // Initialise scores, larger scores are worse
    public void updateMoveScores()
    {
/*        foreach (GameObject move in possibleMoves)
        {

        }*/

        moveScores = new int[3]{100, 100, 100};
        //playerPoint = player.GetComponent<Point>();
        //genScript = generateBoard.GetComponent<GenerateBoard>();
        possibleMoves = new GameObject[3]{entityNode.GetComponent<Point>().parent, entityNode.GetComponent<Point>().child, entityNode.GetComponent<Point>().sibling};

        for (int i= 0; i < moveScores.Length; i++)
        {
            moveScores[i] = getScoreNew(possibleMoves[i], player, generateBoard);
        }
    }

    private static int getScoreNew(GameObject move, GameObject player, GameObject generateBoard)
    {
        // For outer nodes
        if (move == null) //shouldn't make move
        {
            return 100;
        } 

        PlayerController playerController = player.GetComponent<PlayerController>();
        Point playerPoint = playerController.currentNode.GetComponent<Point>();
        Point pointScript = move.GetComponent<Point>();
        GenerateBoard genScript = generateBoard.GetComponent<GenerateBoard>();
        int score = 0;

        // If node occupied, not a good move
        if(pointScript.isOccupiedBy != null)
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
