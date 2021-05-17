using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feldPalette : MonoBehaviour
{
    public GameObject currentDragged;
    public bool isDragging = false;
    public string currentTag;
    Vector2 helper;
    informationGatherer infoGatherer;

    public void increaseComplexity() { infoGatherer.increaseComplexity(); } //informed by dragable
    public void decreaseComplexity() { infoGatherer.decreaseComplexity(); } //informed by dragable


    public void informGatherer()        //informed by dragable if its fire
    {
        if (currentTag == "Flamme") { infoGatherer.fireGrabbed(); }
        if (currentTag == "Ziel")   { infoGatherer.targetGrabbed(); }
    }

    public void palettenUpdate()
    {
        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                helper = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentDragged.transform.position = new Vector3(helper.x, helper.y, 19);
            }
        }
    }

    public void Start()
    {
        infoGatherer = GameObject.Find("Manager").GetComponent<informationGatherer>();
    }
}
