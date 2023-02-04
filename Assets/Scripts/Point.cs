using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float yUpValue = 0f, yDownValue = -0.2f;
    public int layer;
    public int sidePosition;
    public bool isVisible;
    public bool isClockwise;
    public GameObject child; // higher level
    public GameObject parent; // lower level
    public GameObject sibling; // L if layer 0dd, R is layer even
    public GameObject badSibling;
    public GameObject isOccupiedBy;

    private void Update() {
        transform.position = new Vector3(transform.position.x, (isVisible)?yUpValue:yDownValue,transform.position.z);
    }
}
