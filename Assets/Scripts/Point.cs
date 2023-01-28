using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int layer;
    public int sidePosition;
    public GameObject child; // higher level
    public GameObject parent; // lower level
    public GameObject sibling; // L if layer 0dd, R is layer even
    public GameObject badSibling;
    public GameObject isOccupiedBy;
}
