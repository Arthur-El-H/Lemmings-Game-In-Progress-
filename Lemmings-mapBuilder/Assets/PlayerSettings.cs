using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public List<string> mapList = new List<string>();
    int numberOfMaps;

    public void Awake()
    {
        numberOfMaps = PlayerPrefs.GetInt("numberOfMaps");
        for (int i = 0; i < numberOfMaps; i++)
        {
            mapList.Add(PlayerPrefs.GetString("map" + i.ToString()));
        }
        gameObject.GetComponent<Unit>().loadMap.GetComponent<GameLoader>().setMapList(mapList);
        Debug.Log(mapList.Count);
        Debug.Log(mapList[0]);

    }

    private void addToNumber()
    {
        numberOfMaps++;
        PlayerPrefs.SetInt("numberOfMaps", numberOfMaps);
    }

    public void addMap(string name)
    {
        addToNumber();
        PlayerPrefs.SetString("map" + numberOfMaps.ToString(), name);
    }

}
