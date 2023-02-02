using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OriginPoint Class
public class OriginPoint : MonoBehaviour
{
    public GameObject[,] pointArray;
    public GameObject generator;

    private void Start()
    {
        pointArray = generator.GetComponent<GenerateBoard>().pointArray;
    }
}
