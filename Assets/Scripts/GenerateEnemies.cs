using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject Enemy, outerLayerNode, player, generateBoard;
    public GameObject[] enemyArray;
    public bool isTurn, movesMade;
    public int numEnemies = 5, moveDelayInFrames = 100, yValue = 1;
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
                Vector3 initPosition = new Vector3(currentNode.transform.position.x, yValue, currentNode.transform.position.z);
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
            List<MoveLogic> allMoves = new List<MoveLogic>(); // Saves best move for each enemy
            //find minScores and associated move for each player, add to list if they don't move into player
            for (int i = 0; i < numEnemies; i++)
            {
                Enemy enemy = enemyArray[i].GetComponent<Enemy>();
                enemy.updateMoveScores();
                int bestScore = enemy.moveScores.Min();
                int index = System.Array.IndexOf(enemy.moveScores, bestScore);
                GameObject bestMoveToNode = enemy.possibleMoves[index];
                if(bestMoveToNode.GetComponent<Point>().isOccupiedBy != player && bestScore < 100000)
                {
                    allMoves.Add(new MoveLogic(bestScore, bestMoveToNode, i));
                }
            }

            //reorder list randomly
            allMoves.Shuffle<MoveLogic>();
            //reorder list by lowest score
            allMoves = allMoves.OrderByDescending(m => m.Score).ToList();

            //select random amount to move
            System.Random rand = new System.Random();
            int numMoved = rand.Next(allMoves.Count); // Its not dtoring it.

            //make those moves
            for (int i = 0; i < numMoved; i++)
            {
                MoveLogic move = allMoves[i];
                Enemy enemy = enemyArray[move.EnemyIndex].GetComponent<Enemy>();
                //update isOccupiedBy
                move.MoveToNode.GetComponent<Point>().isOccupiedBy = enemyArray[move.EnemyIndex];
                enemy.entityNode.GetComponent<Point>().isOccupiedBy = null;
                //Move
                Vector3 destination = move.MoveToNode.transform.position;
                enemy.transform.position = new Vector3(destination.x, yValue, destination.z);
                enemy.entityNode = move.MoveToNode;
            }
            allMoves.Clear();
            movesMade = true;


            // System.Random rand = new System.Random();
            // int numMoved = rand.Next(numEnemies);

            // for (int i = 0; i < numMoved; i++) // This is largely likely to make the last enemy not move many times over
            // {
            //     Enemy enemy = enemyArray[i].GetComponent<Enemy>();
            //     enemy.updateMoveScores();
            //     int index = System.Array.IndexOf(enemy.moveScores, enemy.moveScores.Min());

            //     // enemy.entityNode.GetComponent<Point>().badSibling.GetComponent<Point>().isOccupiedBy != player
            //         GameObject newNode = enemy.possibleMoves[index];
            //         //check if player is there, if so don't move as adjacent
            //         if (newNode.GetComponent<Point>().isOccupiedBy != player)
            //         {
            //             // Update isOccupiedBy
            //             newNode.GetComponent<Point>().isOccupiedBy = enemyArray[i];
            //             enemy.entityNode.GetComponent<Point>().isOccupiedBy = null;

            //             enemy.transform.position = newNode.transform.position;
            //             enemy.entityNode = newNode;
            //         }
                    

                
            // }
        }
        else
        {
            frameCounter++;
        }

    }
}

class MoveLogic
{
    public int Score { get; private set; }
    public GameObject MoveToNode { get; private set; }
    public int EnemyIndex { get; private set; }

    public MoveLogic(int score, GameObject moveToNode, int enemyIndex)
    {
        Score = score;
        MoveToNode = moveToNode;
        EnemyIndex = enemyIndex;
    }

    
}

static class ShuffleLogic
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list) 
    {
        int n = list.Count;
        while (n>1){
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}