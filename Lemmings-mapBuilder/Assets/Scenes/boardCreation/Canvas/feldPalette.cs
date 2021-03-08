using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feldPalette : MonoBehaviour
{
    public GameObject currentDragged;
    public bool isDragging = false;
    public string currentTag;
    Vector2 helper;

    void Update()
    {
        if(isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                helper = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentDragged.transform.position = new Vector3(helper.x, helper.y, 19);
            }
        }
        else
        {
        }
    }
}
