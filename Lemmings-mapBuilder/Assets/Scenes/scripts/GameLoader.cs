using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    List<string> mapList;
    public void setMapList(List <string> newList) { mapList = newList; }
    public GameObject mapScroller;

    public GameObject test;

    void Start()
    {
        
    }

    public void loadGame()
    {
        Instantiate(test).transform.SetParent(mapScroller.transform, false);
        Debug.Log("yep");
    }
}
