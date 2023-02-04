using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swirl : MonoBehaviour
{
    public bool isClockwise;
    public float speed;
    private Vector3 rotation;
    public PlayerController playerPoint;

    private void Start()
    {
        speed = 1f;
        rotation = new Vector3(0f, speed, 0f) * Time.deltaTime;
    }

    private void Update()
    {
        isClockwise = playerPoint.currentNode.GetComponent<Point>().isClockwise;
        if(isClockwise)
        {
            transform.rotation *= Quaternion.Euler(rotation);
        }
        else
        {
            transform.rotation *= Quaternion.Euler(-rotation);
        }
    }
}
