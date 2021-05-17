using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class listOfMapsSave
{
    public List<string> listOfMaps;   //Anzahl der boards, liste der boardTags
    public listOfMapsSave(List<string> maps) { listOfMaps = maps; }
}
