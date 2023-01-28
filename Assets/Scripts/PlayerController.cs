using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject targetNode;
    public float playerSpeed;
    public int movesToMake;
    public bool isTurn;
    private float timer, yValue = 1;
    private Vector3 currentPosition, targetPosition;
    private int lookingAtNode; //1 parent, 2 sibling, 3 child
    private Point targetNodeData;


    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.localPosition;
        targetPosition = currentPosition;
        timer = 0;
        lookingAtNode = 2;
    }

    // Update is called once per frame
    void Update()
    {
            timer += Time.deltaTime * playerSpeed;
            targetNodeData = targetNode.GetComponent<Point>();
            currentPosition = transform.localPosition;

            if(currentPosition != targetPosition) //not at node
            {
                transform.localPosition = Vector3.Lerp(currentPosition, targetPosition, timer);
            }else if(isTurn) //at a node
            {
                timer = 0;
                if(targetNode != null)
                {
                    targetPosition = new Vector3(targetNode.transform.position.x, yValue,targetNode.transform.position.z);
                }

                if (GetLookedAtNode() != null)
                {
                    var p1 = transform.position;
                    var p2 = GetLookedAtNode().transform.position;
                    var position = new Vector3(p2.x, p1.y, p2.z); // does not bend to target
                    transform.LookAt(position);
                }
                

                bool clockwiseLayer = (targetNodeData.layer) % 2 == 0;
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if(lookingAtNode % 4 != 3 && GetLookedAtNode() != null && movesToMake != 0) //not a bad sibling
                    {
                        targetNode = GetLookedAtNode();
                        movesToMake--;
                    }
                } else if (Input.GetKeyDown(KeyCode.RightArrow))
                { 
                    lookingAtNode = clockwiseLayer ? lookingAtNode + 1: lookingAtNode - 1;
                } else if (Input.GetKeyDown(KeyCode.LeftArrow))
                { 
                    lookingAtNode = clockwiseLayer ? lookingAtNode - 1: lookingAtNode + 1;
                }
            }
        

        //rotate camera
        
    }

    private GameObject GetLookedAtNode()
    {
        switch (lookingAtNode % 4)
        {
            case 1:
                return targetNodeData.sibling;
            case 2:
                return targetNodeData.child;
            case 3:
                return targetNodeData.badSibling;
            default:
                return targetNodeData.parent;
        }

    }
}
