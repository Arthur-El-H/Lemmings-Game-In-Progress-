using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Save 
{
    public string mapName;
    public List<List<string>> boardTags;   //Anzahl der boards, liste der boardTags
    public List<int> lemmingPos;
}
