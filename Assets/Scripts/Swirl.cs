using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swirl : MonoBehaviour
{
    public bool isClockwise;
    public float speed;
    private Vector3 rotation;
    public PlayerController playerPoint;
    public Vector3 height;
    public Vector3 origin;
    public float riseSpeed;
    private bool flowUp;

    private void Start()
    {
        speed = 1f;
        rotation = new Vector3(0f, speed, 0f);
    }

    private void Update()
    {
        isClockwise = playerPoint.currentNode.GetComponent<Point>().isClockwise;
        if(isClockwise)
        {
            transform.rotation *= Quaternion.Euler(rotation * Time.deltaTime);
        }
        else
        {
            transform.rotation *= Quaternion.Euler(-rotation * Time.deltaTime);
        }

        if(transform.position.y >= height.y)
        {
            flowUp = false;
        }
        else if(transform.position.y <= origin.y)
        {
            flowUp = true;
        }

        if (flowUp)
        {
            transform.Translate(new Vector3(0f, 1f, 0f) * riseSpeed * Time.deltaTime);
        }
        else if (!flowUp)
        {
            transform.Translate(new Vector3(0f, -1f, 0f) * riseSpeed * Time.deltaTime);
        }
    }
}
