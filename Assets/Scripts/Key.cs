using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public bool isCollected;
    public GameObject Point, player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Point.GetComponent<Point>().isOccupiedBy == player)
        {
            isCollected = true;
            transform.gameObject.SetActive(false);
        }
    }
}
