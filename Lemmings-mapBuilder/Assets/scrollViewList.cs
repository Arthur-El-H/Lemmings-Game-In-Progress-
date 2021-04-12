using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollViewList : MonoBehaviour
{
    public Unit owner;
    public CanvasRenderer mapButton;
    public CanvasRenderer testMapButton;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        /*
        testMapButton = Instantiate(mapButton);
        testMapButton.transform.SetParent(this.transform);
        testMapButton.GetComponentInChildren<Text>().text = "test";

        foreach (string map in playerSettings.mapList)
        {
            Instantiate(mapButton);
            mapButton.transform.SetParent(this.transform, false);
            mapButton.GetComponentInChildren<Text>().text = map;
            placeButton(id);
        }
        */
    }

    void placeButton (int num)
    {
        id++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
