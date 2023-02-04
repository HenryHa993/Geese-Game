using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public GameObject currentNode, targetNode;
    public Text movesLeftText;
    public float playerSpeed;
    public int movesToMake;
    public bool isTurn;
    private float timer, yValue = 1;
    private Vector3 currentPosition, targetPosition;
    private int lookingAtNode; //1 parent, 2 sibling, 3 child
    private Point targetNodeData;


    // Initialise initial player position
    void Start()
    {
        // Position and set camera view of player
        targetNodeData = targetNode.GetComponent<Point>();
        currentPosition = new Vector3(targetNode.transform.position.x, yValue,targetNode.transform.position.z);
        targetPosition = currentPosition;
        transform.localPosition = currentPosition;
        currentNode = targetNode;
        PointVisibility(true);
        currentNode.GetComponent<Point>().isOccupiedBy = transform.gameObject;
        lookingAtNode = 0;
        lookAtNode();

        // Set timer
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
            // Timer increments
            timer += Time.deltaTime * playerSpeed;

            // Refresh target node component and current position each frame
            targetNodeData = targetNode.GetComponent<Point>();
            currentPosition = transform.localPosition;

            // Not at node, movement
            if(currentPosition != targetPosition)
            {
                transform.localPosition = Vector3.Lerp(currentPosition, targetPosition, timer);
            }
            // If players turn, while stationary at node
            else if(isTurn)
            {
                // UI
                movesLeftText.text = String.Concat("Moves: ", Convert.ToString(movesToMake));

                timer = 0;

                // What is this for?
                if(targetNode != null)
                {
                }

                // Update player camera view
                if (GetLookedAtNode() != null)
                {
                    lookAtNode();
                }

                // Player input is below
                playerInput();
            }
        
    }

    // Helper function to differentiate traversable nodes
    private GameObject GetLookedAtNode()
    {
        switch (lookingAtNode % 4)
        {
            case 1:
                lookingAtNode = 1;
                return targetNodeData.sibling;
            case 2:
                lookingAtNode = 2;
                return targetNodeData.child;
            case 3:
                lookingAtNode = 3;
                return targetNodeData.badSibling;
            default:
                lookingAtNode = 0;
                return targetNodeData.parent;
        }

    }

    //called when moving
    private void PointVisibility(bool isShown)
    {
        Point currentPoint = currentNode.GetComponent<Point>();
        //hide current node connected
        //currentNode.SetActive(isShown);
        currentPoint.isVisible = isShown;
        if (currentPoint.sibling != null)
        {
            //currentPoint.sibling.GetComponent<Point>().isVisible = true;
            //currentPoint.sibling.SetActive(isShown);
            currentPoint.sibling.GetComponent<Point>().isVisible = isShown;
        }
        if (currentPoint.parent != null)
        {
            //currentPoint.parent.SetActive(isShown);
            currentPoint.parent.GetComponent<Point>().isVisible = isShown;
        }
        if (currentPoint.child != null)
        {
            //currentPoint.child.SetActive(isShown);
            currentPoint.child.GetComponent<Point>().isVisible = isShown;
        }
    }

    public void lookAtNode()
    {
        var p1 = transform.position;
        var p2 = GetLookedAtNode().transform.position;
        var position = new Vector3(p2.x, p1.y, p2.z); // does not bend to target
        transform.LookAt(position);
    }

    public void playerInput()
    {
        bool clockwiseLayer = (targetNodeData.layer) % 2 == 0;

        // Player input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (lookingAtNode % 4 != 3 && GetLookedAtNode() != null && movesToMake != 0 && GetLookedAtNode().GetComponent<Point>().isOccupiedBy == null) //not a bad sibling
            {
                // I ADDED THE BLOCK DETECTION HERE
                targetNode = GetLookedAtNode();
                targetNode.GetComponent<Point>().isOccupiedBy = transform.gameObject;

                targetPosition = new Vector3(targetNode.transform.position.x, yValue, targetNode.transform.position.z);
                currentNode.GetComponent<Point>().isOccupiedBy = null;
                PointVisibility(false);
                currentNode = targetNode;
                PointVisibility(true);



                movesToMake--;
                movesLeftText.text = String.Concat("Moves: ", Convert.ToString(movesToMake));

                // Update isOccupiedBy

            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Turn made");
            lookingAtNode = clockwiseLayer ? lookingAtNode + 1 : lookingAtNode - 1;
            //if(GetLookedAtNode() == null) lookingAtNode = clockwiseLayer ? lookingAtNode - 1 : lookingAtNode + 1;
            if (lookingAtNode <= -1) lookingAtNode = 3;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Turn made");
            lookingAtNode = clockwiseLayer ? lookingAtNode - 1 : lookingAtNode + 1;
            //if(GetLookedAtNode() == null) lookingAtNode = clockwiseLayer ? lookingAtNode + 1 : lookingAtNode - 1;
            if (lookingAtNode <= -1) lookingAtNode = 3;

        }
    }
}
