using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonSensor : MonoBehaviour, IPointerEnterHandler
{
    public informationGatherer infoGatherer;
    public string thisButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoGatherer.hoveringOverButton(thisButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
